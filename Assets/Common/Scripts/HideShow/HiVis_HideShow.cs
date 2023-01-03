using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiVis_HideShow : MonoBehaviour
{

    public GameObject HiVis;
    public GameObject HiVis_Inside;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if (HiVis.activeInHierarchy == true)
            HiVis.SetActive(false);
        else
            HiVis.SetActive(true);

        if (HiVis_Inside.activeInHierarchy == true)
            HiVis_Inside.SetActive(false);
        else
            HiVis_Inside.SetActive(true);
    }
}
