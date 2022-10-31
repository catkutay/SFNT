using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator myAnimationController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            myAnimationController.SetBool("Death", true);
        }
    }
}
