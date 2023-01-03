using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardCapText_HideShow : MonoBehaviour
{

    public GameObject HardCapText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(HardCapText.activeInHierarchy == true)
            HardCapText.SetActive(false);
        else
            HardCapText.SetActive(true);
    }
}
