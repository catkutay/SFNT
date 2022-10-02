using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowObject_01 : MonoBehaviour
{
    public GameObject SM_Chr_Builder_HighVis_01;

    void Start() {

    }

    void Update() {
        
    }

    public void whenButtonClicked() {
        if (SM_Chr_Builder_HighVis_01.activeInHierarchy == false)
            SM_Chr_Builder_HighVis_01.SetActive(true);
        else
            SM_Chr_Builder_HighVis_01.SetActive(false);
    }
}
