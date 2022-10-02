using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiVis_HideShow : MonoBehaviour
{

    public GameObject HiVis;

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
    }
}
