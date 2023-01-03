using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloves_HideShow : MonoBehaviour
{

    public GameObject Gloves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(Gloves.activeInHierarchy == true)
            Gloves.SetActive(false);
        else
            Gloves.SetActive(true);
    }
}
