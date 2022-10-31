using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dying_anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
