using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brooming : MonoBehaviour
{
    public GameObject Broom;
    // [SerializeField] private Animator myAnimatorController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void whenButtonClicked()
    {
        // myAnimatorController.SetBool("playBrooming", true);
        GetComponent<Animator>().Play("Brooming");
        Broom.SetActive(true);
        
    } 
}
