using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Scene1()
    {
        SceneManager.LoadScene("1-Menu");
    }
    public void Scene2()
    {
        SceneManager.LoadScene("3-GroundPlane");
    }
    public void Scene3()
    {
        SceneManager.LoadScene("4-GroundPlane");
    }
}
