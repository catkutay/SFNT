using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GogglesText_HideShow : MonoBehaviour
{

    public GameObject GogglesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked() {
        if(GogglesText.activeInHierarchy == true)
            GogglesText.SetActive(false);
        else
            GogglesText.SetActive(true);
    }
}
