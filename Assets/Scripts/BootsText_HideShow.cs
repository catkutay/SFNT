using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsText_HideShow : MonoBehaviour
{

    public GameObject BootsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(BootsText.activeInHierarchy == true)
            BootsText.SetActive(false);
        else
            BootsText.SetActive(true);
    }
}
