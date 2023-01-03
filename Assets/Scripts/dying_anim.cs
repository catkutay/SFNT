using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dying_anim : MonoBehaviour
{

    //public GameObject Male;
    public GameObject Broom;
    public GameObject Hit_Text;
    // public static CFX_AutoDestructShuriken text;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Animator>().Play("Brooming");
        GetComponent<Animator>().Play("Brooming");
    }

    // Update is called once per frame
    void Update()
    {
        // GetComponent<Animator>().Play("Brooming");
        // if (Animator.IsPlaying("Brooming"))
        //     Broom.SetActive(true);
        // else
        //     Broom.SetActive(false);
    }

    // public void getUp() {
    //     GetComponent<Animator>().Play("stand up");
    // }

    public void whenButtonClicked() {
        
        StartCoroutine(anim_delay());
        
    }

    public IEnumerator anim_delay() {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(0.1f);
        Debug.Log(Time.time);

        GetComponent<Animator>().Play("dying");
        Broom.SetActive(false);
        if (Hit_Text.activeInHierarchy == true)
            Hit_Text.SetActive(false);
        else
            Hit_Text.SetActive(true);
        
    }

    // public void onPlay() {
    //     if (Hit_Text.activeInHierarchy == true)
    //         Hit_Text.SetActive(false);
    //     else
    //         Hit_Text.SetActive(true);
    // } 

}
