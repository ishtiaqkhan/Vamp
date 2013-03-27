using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject CloseRangedEnemy;
    public GameObject FarRangedEnemy;
    public Transform SpawnPoint;   
    private int EnemyCount;
    private int EnemyMax;
    private bool EnemyType;
    private bool StartSpawning;
    RaycastHit hit;

    // Use this for initialization
    void Start()
    {
        EnemyMax = 7;
        StartSpawning = true;
    }

    // Update is called once per frame
    void Update()
    {        
        EnemyCount = Close_Ranged_Enemy.allCloseRangedEnemy.Count + Far_Ranged_Enemy.allFarRangedEnemy.Count;

        if (EnemyCount < EnemyMax && StartSpawning == true)
        {
            if (Random.Range(0, 10) < 5)
            {
                EnemyType = true;
            }
            else
            {
                EnemyType = false;
            }

            for (int i = 0; i < EnemyMax; i++) 
            {
                Vector3 TempSpawnPoint = new Vector3(SpawnPoint.transform.position.x, SpawnPoint.transform.position.y, SpawnPoint.transform.position.z + i);
                if (EnemyType == true)
                {
                    Instantiate(CloseRangedEnemy, TempSpawnPoint, Quaternion.identity);
                }

                if (EnemyType == false)
                {
                    Instantiate(FarRangedEnemy, TempSpawnPoint, Quaternion.identity);
                }          
            }
        } 

        else
        {
            StartSpawning = false;
        }     
    }
}