using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots_HideShow : MonoBehaviour
{

    public GameObject Boots;
    public GameObject Thongs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(Boots.activeInHierarchy == true)
            Boots.SetActive(false);
        else
            Boots.SetActive(true);

        if(Thongs.activeInHierarchy == false)
            Thongs.SetActive(true);
        else
            Thongs.SetActive(false);
    }
}
