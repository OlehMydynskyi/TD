using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private float maxGateHP;
    private float currentGateHP;
    private float gateHeight;

    private void Start()
    {
        gateHeight = transform.lossyScale.y;
        currentGateHP = maxGateHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GetDamage(other.GetComponent<EnemyManager>().CurrentHP);
            other.gameObject.GetComponent<EnemyManager>().OnDeath();
        }
    }

    private void GetDamage (float damage)
    {
        currentGateHP -= damage;
        if (currentGateHP < 0)
            Destroy(gameObject);
        float scaleDamage = damage / maxGateHP * gateHeight;
        transform.localScale -= new Vector3(0, scaleDamage, 0);
        transform.position -= new Vector3(0, scaleDamage / 2, 0);
    }
        
}
