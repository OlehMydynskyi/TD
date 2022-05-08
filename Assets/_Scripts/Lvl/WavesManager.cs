using System.Collections;
using UnityEngine;
using Object_Pooling;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Wave[] waves;
    private bool working = false;
    private int numberOfWave = 0;
    [HideInInspector] public bool started;
    [HideInInspector] public bool endWork = false;
    private ObjectPool objectPool;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
    }

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
        objectPool.GetObject(waves[numberOfWave].enemyPrefab).GetComponent<EnemyManager>().OnSpawn(transform, waves[numberOfWave].enemyHP);
        waves[numberOfWave].countOfEnemies--;
        yield return new WaitForSeconds(waves[numberOfWave].interval);
        working = false;
    }
}