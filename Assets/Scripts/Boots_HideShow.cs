using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots_HideShow : MonoBehaviour
{

    public GameObject Boots;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(Boots.activeInHierarchy == false)
            Boots.SetActive(true);
        else
            Boots.SetActive(false);
    }
}
