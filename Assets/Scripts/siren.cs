using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class siren : MonoBehaviour
{
    public GameObject pointLight_1, pointLight_2;
    public float waitTime = .2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Siren());
    }

    IEnumerator Siren()
    {
        yield return new WaitForSeconds(waitTime);

        pointLight_1.SetActive(false);
        pointLight_2.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        pointLight_1.SetActive(true);
        pointLight_2.SetActive(false);

        StartCoroutine(Siren());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
