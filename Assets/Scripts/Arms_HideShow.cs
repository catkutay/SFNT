using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms_HideShow : MonoBehaviour
{

    public GameObject Arms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(Arms.activeInHierarchy == true)
            Arms.SetActive(false);
        else
            Arms.SetActive(true);
    }
}
