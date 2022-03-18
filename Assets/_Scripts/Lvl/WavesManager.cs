using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy; 
    [SerializeField] private int[] countOfEnemies;
    [SerializeField] private float[] interval;
    private bool working = false;
    private int numberOfWaves = 0;
    private bool permit = true;
    [HideInInspector] public bool started;
    [HideInInspector] public bool endWork = false;

    void Start()
    {
        if (enemy.Length != interval.Length && enemy.Length != countOfEnemies.Length)
        {
            Debug.Log("Lengths of arrays must be the same.");
            permit = false;
        }
    }

    private void FixedUpdate()
    {
        if (!permit || !started)
            return;

        if (numberOfWaves >= enemy.Length)
        {
            endWork = true;
            return;
        }            
        else if (!working && countOfEnemies[numberOfWaves] > 0)
            StartCoroutine("Spawn");
        else if (countOfEnemies[numberOfWaves] <= 0)
            numberOfWaves++;
    }

    IEnumerator Spawn()
    {
        working = true;
        Instantiate(enemy[numberOfWaves], gameObject.transform.position, Quaternion.identity); 
        countOfEnemies[numberOfWaves]--;
        yield return new WaitForSeconds(interval[numberOfWaves]);
        working = false;
    }
}