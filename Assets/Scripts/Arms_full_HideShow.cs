using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms_full_HideShow : MonoBehaviour
{

    public GameObject Arms_full;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(Arms_full.activeInHierarchy == true)
            Arms_full.SetActive(false);
        else
            Arms_full.SetActive(true);
    }
}
