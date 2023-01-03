using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowObject : MonoBehaviour
{
    public GameObject HardCap;

    void Start() {

    }

    void Update() {
        
    }

    public void whenButtonClicked() {
        if (HardCap.activeInHierarchy == true)
            HardCap.SetActive(false);
        else
            HardCap.SetActive(true);
    }
}
