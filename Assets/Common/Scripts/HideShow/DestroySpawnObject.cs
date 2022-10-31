using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawnObject : MonoBehaviour
{
    public GameObject SpawnObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(SpawnObjectPrefab, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
