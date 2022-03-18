using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LVLManager : MonoBehaviour
{
    public static LVLManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [SerializeField] private float startTime;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private float radiusOfVisibility;
    [SerializeField] private WavesManager[] wavesManager;
    private bool lvlEnded = false;
    [SerializeField] public int coins;
    [SerializeField] private GameObject coinsText;
    [SerializeField] private GameObject completeUI;

    void Start()
    {
        StartCoroutine("StartWavesManager");
        coinsText.GetComponent<TMP_Text>().text = coins.ToString();
    }


    void FixedUpdate()
    {
        if (lvlEnded)
            return;

        if (!EnemyCheck())
        {
            for (int i = 0; i < wavesManager.Length; i++)
            {
                if (!wavesManager[i].endWork)
                    return;
            }
            
            EndLvl();
        } 
    }

    IEnumerator StartWavesManager()
    {
        yield return new WaitForSeconds(startTime);

        for (int i = 0; i < wavesManager.Length; i++)
        {
            wavesManager[i].started = true;
        }
            
    }

    private void EndLvl()
    {
        Debug.Log("End");
        completeUI.SetActive(true);
        lvlEnded = true;
    }

    private bool EnemyCheck()
    {
        return Physics.CheckSphere(transform.position, radiusOfVisibility, enemies);
    }

    public void ChangeCoins(int value)
    {
        coins += value;
        coinsText.GetComponent<TMP_Text>().text = coins.ToString();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radiusOfVisibility);
    }
}
