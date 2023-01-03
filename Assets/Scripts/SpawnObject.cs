using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject SpawnObjectPrefab;
    public void whenButtonClicked()
    {
        // Instantiate(SpawnObjectPrefab, transform.position, Quaternion.identity);
        // Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        Instantiate(SpawnObjectPrefab, transform);        
    }
}
