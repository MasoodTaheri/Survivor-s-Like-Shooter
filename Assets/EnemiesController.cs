using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float SpawnDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemy.transform.position = Random.insideUnitCircle.normalized * SpawnDistance;
        }
    }
}
