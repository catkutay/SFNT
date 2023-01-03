using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goggles_HideShow : MonoBehaviour
{

    public GameObject Goggles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if (Goggles.activeInHierarchy == true)
            Goggles.SetActive(false);
        else
            Goggles.SetActive(true);
    }
}
