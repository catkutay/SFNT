using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowObject : MonoBehaviour
{
    public GameObject Characters;

    void Start() {

    }

    void Update() {
        
    }

    public void whenButtonClicked() {
        if (Characters.activeInHierarchy == true)
            Characters.SetActive(false);
        else
            Characters.SetActive(true);
    }
}
