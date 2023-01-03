using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiVisVestText_HideShow : MonoBehaviour
{

    public GameObject HiVisVestText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(HiVisVestText.activeInHierarchy == true)
            HiVisVestText.SetActive(false);
        else
            HiVisVestText.SetActive(true);
    }
}
