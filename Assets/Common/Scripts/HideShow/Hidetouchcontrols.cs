using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidetouchcontrols : MonoBehaviour
{
    public GameObject ProductPlacement;

    void Start() {

    }

    void Update() {
        
    }

    public void whenButtonClicked() {
        if (ProductPlacement.activeInHierarchy == false)
            ProductPlacement.SetActive(true);
        else
            ProductPlacement.SetActive(false);
    }
}
