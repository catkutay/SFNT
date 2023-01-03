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

    //public GameObject Start_Button_Hide;
    //public GameObject OnScreenPrompt_01;
    //public AudioSource audioSource;

    public bool GroundPlaneHitReceived { get; private set; }
    Vector3 ProductScale
    {
        get
        {
            var augmentationScale = VuforiaRuntimeUtilities.IsPlayMode() ? 0.1f : ProductSize;
            return new Vector3(augmentationScale, augmentationScale, augmentationScale);
        }
    }

    // Augmented object
    [Header("Augmentation Object")]
    
    // character
    [SerializeField] GameObject Character = null;
    // [SerializeField] GameObject CharacterBody = null;

    // character body parts
    [SerializeField] GameObject Arms = null;
    [SerializeField] GameObject Beard = null;
    [SerializeField] GameObject Chest = null;
    [SerializeField] GameObject Foot = null;
    [SerializeField] GameObject Hair = null;
    [SerializeField] GameObject Hands = null;
    [SerializeField] GameObject Head = null;
    [SerializeField] GameObject Hip = null;
    [SerializeField] GameObject Legs = null;
    [SerializeField] GameObject ShinLower = null;
    [SerializeField] GameObject ShinUpper = null;
    [SerializeField] GameObject Shoulders = null;
    [SerializeField] GameObject Stomach = null;

    // character clothes
    [SerializeField] GameObject Shirt = null;
    [SerializeField] GameObject Pants = null;
    [SerializeField] GameObject Shoes = null;

    
    
    // augmented object controller
    [Header("Control Indicators")]
    [SerializeField] GameObject TranslationIndicator = null;
    [SerializeField] GameObject RotationIndicator = null;

    [Header("Augmentation Size")]
    [Range(0.1f, 2.0f)]
    [SerializeField] float ProductSize = 0.65f;

    const string RESOURCES_MALE_SHIRT = "shirt";
    const string RESOURCES_MALE_PANTS = "pants";
    const string RESOURCES_MALE_SHOES = "shoes";
    const string RESOURCES_CHARACTER_BODY = "Body";
    
    const string RESOURCES_TRANSPARENT = "EXAMPLE HAND4";
    // const string RESOURCES_MALE_PANTS_TRANSPARENT = "MalePantsTransparent";
    // const string RESOURCES_MALE_SHOES_TRANSPARENT = "MaleShoesTransparent";
    // const string RESOURCES_MALE_BODY_TRANSPARENT = "MaleBodyTransparent";
    
    const string GROUND_PLANE_NAME = "Emulator Ground Plane";
    const string FLOOR_NAME = "Floor";

    
    //SkinnedMeshRenderer mCharacterBodyRenderer;
    SkinnedMeshRenderer mArmsRenderer;
    SkinnedMeshRenderer mBeardRenderer;
    SkinnedMeshRenderer mChestRenderer;
    SkinnedMeshRenderer mFootRenderer;
    SkinnedMeshRenderer mHairRenderer;
    SkinnedMeshRenderer mHandsRenderer;
    SkinnedMeshRenderer mHeadRenderer;
    SkinnedMeshRenderer mHipRenderer;
    SkinnedMeshRenderer mLegsRenderer;
    SkinnedMeshRenderer mShinLowerRenderer;
    SkinnedMeshRenderer mShinUpperRenderer;
    SkinnedMeshRenderer mShouldersRenderer;
    SkinnedMeshRenderer mStomachRenderer;

    SkinnedMeshRenderer mShirtRenderer;
    SkinnedMeshRenderer mPantsRenderer;
    SkinnedMeshRenderer mShoesRenderer;

    
    //MeshRenderer mSetRenderer;
    
    //AudioSource audioSource;
    
    Material[] mMaleShirtMaterials, mMaleShirtMaterialsTransparent;
    Material[] mMalePantsMaterials, mMalePantsMaterialsTransparent;
    Material[] mMaleShoesMaterials, mMaleShoesMaterialsTransparent;
    Material[] mMaleBodyMaterials, mMaleBodyMaterialsTransparent;
    
    
    Camera mMainCamera;
    string mFloorName;
    Vector3 mOriginalChairScale;
    bool mIsPlaced;
    int mAutomaticHitTestFrameCount;

    void Start()
    {
        mMainCamera = VuforiaBehaviour.Instance.GetComponent<Camera>();
        //mCharacterBodyRenderer = CharacterBody.GetComponent<SkinnedMeshRenderer>();
        mArmsRenderer = Arms.GetComponent<SkinnedMeshRenderer>();
        mBeardRenderer = Beard.GetComponent<SkinnedMeshRenderer>();
        mChestRenderer = Chest.GetComponent<SkinnedMeshRenderer>();
        mFootRenderer = Foot.GetComponent<SkinnedMeshRenderer>();
        mHairRenderer = Hair.GetComponent<SkinnedMeshRenderer>();
        mHandsRenderer = Hands.GetComponent<SkinnedMeshRenderer>();
        mHeadRenderer = Head.GetComponent<SkinnedMeshRenderer>();
        mHipRenderer = Hip.GetComponent<SkinnedMeshRenderer>();
        mLegsRenderer = Legs.GetComponent<SkinnedMeshRenderer>();
        mShinLowerRenderer = ShinLower.GetComponent<SkinnedMeshRenderer>();
        mShinUpperRenderer = ShinUpper.GetComponent<SkinnedMeshRenderer>();
        mShouldersRenderer = Shoulders.GetComponent<SkinnedMeshRenderer>();
        mStomachRenderer = Stomach.GetComponent<SkinnedMeshRenderer>();

        mShirtRenderer = Shirt.GetComponent<SkinnedMeshRenderer>();
        mPantsRenderer = Pants.GetComponent<SkinnedMeshRenderer>();
        mShoesRenderer = Shoes.GetComponent<SkinnedMeshRenderer>();

        
        //audioSource = Character.GetComponent<AudioSource>();

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
            
            
            
            //Start_Button_Hide.SetActive(true);
            //OnScreenPrompt_01.SetActive(true);

        }
        else
        {
            RotationIndicator.SetActive(false);
            TranslationIndicator.SetActive(false);
            
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
            //mCharacterBodyRenderer.enabled = isVisible;
            mArmsRenderer.enabled = mBeardRenderer.enabled = mChestRenderer.enabled = mFootRenderer.enabled = mHairRenderer.enabled = mHandsRenderer.enabled = mHeadRenderer.enabled = mHipRenderer.enabled = mLegsRenderer.enabled = mPantsRenderer.enabled = mShinLowerRenderer.enabled = mShinUpperRenderer.enabled = mShirtRenderer.enabled = mShoesRenderer.enabled = mShouldersRenderer.enabled = mStomachRenderer.enabled = isVisible;
            
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
        // mCharacterBodyMaterials = new[]
        //                   {
        //                       Resources.Load<Material>(RESOURCES_CHARACTER_BODY),
        //                   };

        // mCharacterBodyMaterialsTransparent = new[]
        //                              {
        //                                  Resources.Load<Material>(RESOURCES_CHARACTER_BODY_TRANSPARENT),
        //                              };

        mMaleShirtMaterials = new[]
                          {
                              Resources.Load<Material>(RESOURCES_MALE_SHIRT),
                          };

        mMaleShirtMaterialsTransparent = new[]
                                     {
                                         Resources.Load<Material>(RESOURCES_TRANSPARENT),
                                     };

        mMalePantsMaterials = new[]
                          {
                              Resources.Load<Material>(RESOURCES_MALE_PANTS),
                          };

        mMalePantsMaterialsTransparent = new[]
                                     {
                                         Resources.Load<Material>(RESOURCES_TRANSPARENT),
                                     };

        mMaleShoesMaterials = new[]
                          {
                              Resources.Load<Material>(RESOURCES_MALE_SHOES),
                          };

        mMaleShoesMaterialsTransparent = new[]
                                     {
                                         Resources.Load<Material>(RESOURCES_TRANSPARENT),
                                     };

        mMaleBodyMaterials = new[]
                          {
                              Resources.Load<Material>(RESOURCES_CHARACTER_BODY),
                          };

        mMaleBodyMaterialsTransparent = new[]
                                     {
                                         Resources.Load<Material>(RESOURCES_TRANSPARENT),
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
        //mCharacterBodyRenderer.materials = previewEnabled ? mCharacterBodyMaterialsTransparent : mCharacterBodyMaterials;
        mArmsRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mBeardRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mChestRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mFootRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mHairRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mHandsRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mHeadRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mHipRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mLegsRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mShinLowerRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mShinUpperRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mShouldersRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;
        mStomachRenderer.materials = previewEnabled ? mMaleBodyMaterialsTransparent : mMaleBodyMaterials;

        mShirtRenderer.materials = previewEnabled ? mMaleShirtMaterialsTransparent : mMaleShirtMaterials;
        mPantsRenderer.materials = previewEnabled ? mMalePantsMaterialsTransparent : mMalePantsMaterials;
        mShoesRenderer.materials = previewEnabled ? mMaleShoesMaterialsTransparent : mMaleShoesMaterials;
        
    }

    void RotateTowardsCamera(GameObject augmentation)
    {
        var lookAtPosition =  mMainCamera.transform.position - augmentation.transform.position;
        lookAtPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookAtPosition);
        augmentation.transform.rotation = rotation;
    }

}
