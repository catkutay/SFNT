/*============================================================================== 
Copyright (c) 2021 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.   
==============================================================================*/

using UnityEngine;
using Vuforia;
using Vuforia.UnityRuntimeCompiled;

public class ProductPlacement : MonoBehaviour
{

    public GameObject Start_Button;
    public GameObject OnScreenPrompt_01;

    public bool GroundPlaneHitReceived { get; private set; }
    Vector3 ProductScale
    {
        get
        {
            var augmentationScale = VuforiaRuntimeUtilities.IsPlayMode() ? 0.1f : ProductSize;
            return new Vector3(augmentationScale, augmentationScale, augmentationScale);
        }
    }

    [Header("Augmentation Object")]
    [SerializeField] GameObject Character = null;
    //[SerializeField] GameObject ChairShadow = null;

    [Header("Control Indicators")]
    [SerializeField] GameObject TranslationIndicator = null;
    [SerializeField] GameObject RotationIndicator = null;

    [Header("Augmentation Size")]
    [Range(0.1f, 2.0f)]
    [SerializeField] float ProductSize = 0.65f;

    const string RESOURCES_CHAIR_BODY = "ChairBody";//const string RESOURCES_CHAIR_BODY = "ChairBody";
    //const string RESOURCES_Chair_FRAME = "ChairFrame";
    //const string RESOURCES_Chair_SHADOW = "ChairShadow";
    const string RESOURCES_CHAIR_BODY_TRANSPARENT = "ChairBodyTransparent";
    //const string RESOURCES_Chair_FRAME_TRANSPARENT = "ChairFrameTransparent";
    //const string RESOURCES_Chair_SHADOW_TRANSPARENT = "ChairShadowTransparent";
    const string GROUND_PLANE_NAME = "Emulator Ground Plane";
    const string FLOOR_NAME = "Floor";

    MeshRenderer mChairRenderer;
    //MeshRenderer mChairShadowRenderer;
    
    Material[] mChairMaterials, mChairMaterialsTransparent;
    //Material mChairShadowMaterial, mChairShadowMaterialTransparent;
    Camera mMainCamera;
    string mFloorName;
    Vector3 mOriginalChairScale;
    bool mIsPlaced;
    int mAutomaticHitTestFrameCount;

    void Start()
    {
        mMainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        mChairRenderer = Character.GetComponent<MeshRenderer>();
        //mChairShadowRenderer = ChairShadow.GetComponent<MeshRenderer>();

        SetupMaterials();
        SetupFloor();
        
        mOriginalChairScale = Character.transform.localScale;
        Reset();
    }

    void Update()
    {
        EnablePreviewModeTransparency(!mIsPlaced);
        if (!mIsPlaced)
            RotateTowardsCamera(Character);

        if (mIsPlaced)
        {
            RotationIndicator.SetActive(Input.touchCount == 2);

            TranslationIndicator.SetActive((TouchHandler.sIsSingleFingerDragging || TouchHandler.sIsSingleFingerStationary)
                                            && !UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected());

            SnapProductToMousePosition();

            Start_Button.SetActive(true);
            OnScreenPrompt_01.SetActive(true);

        }
        else
        {
            RotationIndicator.SetActive(false);
            TranslationIndicator.SetActive(false);
            Start_Button.SetActive(false);
            OnScreenPrompt_01.SetActive(false);
        }
    }

    void LateUpdate()
    {
        // The AutomaticHitTestFrameCount is assigned the Time.frameCount in the
        // OnAutomaticHitTest() callback method. When the LateUpdate() method
        // is then called later in the same frame, it sets GroundPlaneHitReceived
        // to true if the frame number matches. For any code that needs to check
        // the current frame value of GroundPlaneHitReceived, it should do so
        // in a LateUpdate() method.
        GroundPlaneHitReceived = mAutomaticHitTestFrameCount == Time.frameCount;

        if (!mIsPlaced)
        {
            // The Chair should only be visible if the following conditions are met:
            // 1. Target Status is Tracked, Extended Tracked or Limited
            // 2. Ground Plane Hit was received for this frame
            var isVisible = VuforiaBehaviour.Instance.DevicePoseBehaviour.TargetStatus.IsTrackedOrLimited() && GroundPlaneHitReceived;
            //mChairRenderer.enabled = mChairShadowRenderer.enabled = isVisible;
            mChairRenderer.enabled = isVisible;
            
            
        }
    }

    void SnapProductToMousePosition()
    {
        if (TouchHandler.sIsSingleFingerDragging || VuforiaRuntimeUtilities.IsPlayMode() && Input.GetMouseButton(0))
        {
            if (!UnityRuntimeCompiledFacade.Instance.IsUnityUICurrentlySelected())
            {
                var cameraToPlaneRay = mMainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(cameraToPlaneRay, out var cameraToPlaneHit) &&
                    cameraToPlaneHit.collider.gameObject.name == mFloorName)
                    Character.transform.position = cameraToPlaneHit.point;
            }
        }
    }

    /// <summary>
    /// Resets the augmentation.
    /// It is called by the UI Reset Button and also by DevicePoseManager.DevicePoseReset callback.
    /// </summary>
    public void Reset()
    {
        Character.transform.localPosition = Vector3.zero;
        Character.transform.localEulerAngles = Vector3.zero;
        Character.transform.localScale = Vector3.Scale(mOriginalChairScale, ProductScale);

        mIsPlaced = false;
    }

    /// <summary>
    /// Adjusts augmentation in a desired way.
    /// Anchor is already placed by ContentPositioningBehaviour.
    /// So any augmentation on the anchor is also placed.
    /// </summary>
    public void OnContentPlaced()
    {
        Debug.Log("OnContentPlaced() called.");

        // Align content to the anchor
        Character.transform.localPosition = Vector3.zero;
        RotateTowardsCamera(Character);

        mIsPlaced = true;
    }

    /// <summary>
    /// Displays a preview of the Chair at the location pointed by the device.
    /// It is registered to PlaneFinderBehaviour.OnAutomaticHitTest.
    /// </summary>
    public void OnAutomaticHitTest(HitTestResult result)
    {
        mAutomaticHitTestFrameCount = Time.frameCount;

        if (!mIsPlaced)
        {
            // Content is not placed yet. So we place the augmentation at HitTestResult
            // position to provide a visual feedback about where the augmentation will be placed.
            Character.transform.position = result.Position;
        }
    }

    void SetupMaterials()
    {
        mChairMaterials = new[]
                          {
                              Resources.Load<Material>(RESOURCES_CHAIR_BODY),
                              //Resources.Load<Material>(RESOURCES_Chair_FRAME)
                          };

        mChairMaterialsTransparent = new[]
                                     {
                                         Resources.Load<Material>(RESOURCES_CHAIR_BODY_TRANSPARENT),
                                         //Resources.Load<Material>(RESOURCES_Chair_FRAME_TRANSPARENT)
                                     };

        //mChairShadowMaterial = Resources.Load<Material>(RESOURCES_Chair_SHADOW);
        //mChairShadowMaterialTransparent = Resources.Load<Material>(RESOURCES_Chair_SHADOW_TRANSPARENT);
    }

    void SetupFloor()
    {
        if (VuforiaRuntimeUtilities.IsPlayMode())
            mFloorName = GROUND_PLANE_NAME;
        else
        {
            mFloorName = FLOOR_NAME;
            var floor = new GameObject(mFloorName, typeof(BoxCollider));
            floor.transform.SetParent(Character.transform.parent);
            floor.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            floor.transform.localScale = Vector3.one;
            floor.GetComponent<BoxCollider>().size = new Vector3(100f, 0, 100f);
        }
    }

    void EnablePreviewModeTransparency(bool previewEnabled)
    {
        mChairRenderer.materials = previewEnabled ? mChairMaterialsTransparent : mChairMaterials;
        //mChairShadowRenderer.material = previewEnabled ? mChairShadowMaterialTransparent : mChairShadowMaterial;
    }

    void RotateTowardsCamera(GameObject augmentation)
    {
        var lookAtPosition =  mMainCamera.transform.position - augmentation.transform.position;
        lookAtPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookAtPosition);
        augmentation.transform.rotation = rotation;
    }

}

