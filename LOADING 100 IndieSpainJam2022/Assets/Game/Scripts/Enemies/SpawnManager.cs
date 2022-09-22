using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] enemiesPrefabs;

    public int round = 0;
    public int roundSize = 3;
    float nextRoundTime = 10f;



    private void Start()
    {
        StartCoroutine(RoundCycle());
    }


    IEnumerator RoundCycle()
    {
        yield return new WaitForSeconds(nextRoundTime);
        NewRound(roundSize);
        UpgradeRound();
        StartCoroutine(RoundCycle());
    }

    void UpgradeRound()
    {
        if(round%3 == 0)
        {
            roundSize++;
            nextRoundTime -= 0.5f;
        }
    }

    GameObject GetRandomEnemy()
    {
        return enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
    }

    Transform GetRandomSpawn()
    {
        GameObject spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        if(spawn.GetComponent<Spawner>().ready)
        {
            spawn.GetComponent<Spawner>().ready = false;
            return spawn.transform;
        }
        else
        {
           return GetRandomSpawn();
        }
    }



    void RandomSpawn()
    {

        Instantiate(GetRandomEnemy(), GetRandomSpawn().position, Quaternion.identity);
    }



    void NewRound(int size)
    {
        if(spawnPoints.Length > size)
        {
            for (int i = 0; i < size; i++)
            {
                RandomSpawn();
            }
            ResetSpawns();
        }
        else
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                RandomSpawn();
            }

            ResetSpawns();
            NewRound(size - spawnPoints.Length);
        }
        round++;
    }

     void ResetSpawns()
    {
        foreach(GameObject spawn in spawnPoints)
        {
            spawn.GetComponent<Spawner>().ready = true;
        }
    }






}
