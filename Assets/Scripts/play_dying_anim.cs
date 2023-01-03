using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_dying_anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Broom;
    public GameObject Hit_Text;
    [SerializeField] private Animator myAnimatorController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Untagged"))
        
        {
            myAnimatorController.SetBool("playDying", true);
            Broom.SetActive(false);
            Hit_Text.SetActive(true);
            
        }
        
    }   

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Untagged"))
        {
            myAnimatorController.SetBool("playDying", false);
        }
    }   

}
