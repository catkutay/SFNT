using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesText_HideShow : MonoBehaviour
{

    public GameObject GlovesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(GlovesText.activeInHierarchy == true)
            GlovesText.SetActive(false);
        else
            GlovesText.SetActive(true);
    }
}
