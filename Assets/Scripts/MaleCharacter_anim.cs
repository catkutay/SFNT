using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleCharacter_anim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dyingAnim() {
        
        StartCoroutine(dyingAnim_delay());
    }

    public IEnumerator dyingAnim_delay() {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(0.1f);
        Debug.Log(Time.time);

        GetComponent<Animator>().Play("dying");
    }

    public void notDyingAnim() {
        
        StartCoroutine(notDyingAnim_delay());
    }

    public IEnumerator notDyingAnim_delay() {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(0f);
        Debug.Log(Time.time);

        GetComponent<Animator>().Play("Getting hit");
    }

    public void truckHitAnim() {
        
        StartCoroutine(truckHitAnim_delay());
    }

    public IEnumerator truckHitAnim_delay() {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(0.5f);
        Debug.Log(Time.time);

        GetComponent<Animator>().Play("waving");
    }

}
