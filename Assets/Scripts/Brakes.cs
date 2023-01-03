using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakes : MonoBehaviour
{
    public GameObject brakeLight_L, brakeLight_R;
    public float waitTime = 4f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(brakes());
    }

    IEnumerator brakes()
    {
        // yield return new WaitForSeconds(waitTime);

        // brakeLight_L.SetActive(false);
        // brakeLight_R.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        brakeLight_L.SetActive(true);
        brakeLight_R.SetActive(true);

        StartCoroutine(brakes());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
