using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private bool working = false;
    private int numberOfWave = 0;
    [HideInInspector] public bool started;
    [HideInInspector] public bool endWork = false;

    private void FixedUpdate()
    {

        if (numberOfWave >= waves.Length)
        {
            endWork = true;
            return;
        }            
        else if (!working && waves[numberOfWave].countOfEnemies > 0)
            StartCoroutine("Spawn");
        else if (waves[numberOfWave].countOfEnemies <= 0)
            numberOfWave++;
    }

    IEnumerator Spawn()
    {
        working = true;
        Instantiate(waves[numberOfWave].enemyPrefab, gameObject.transform.position, Quaternion.identity);
        waves[numberOfWave].countOfEnemies--;
        yield return new WaitForSeconds(waves[numberOfWave].interval);
        working = false;
    }
}