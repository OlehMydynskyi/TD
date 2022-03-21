﻿using UnityEngine;

public abstract class DamageDealer : MonoBehaviour
{
    [HideInInspector] public GameObject target;
    [SerializeField] private float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected LayerMask enemyLayer;
    private Vector3 lastTargetPoint;

    void Start()
    {
        lastTargetPoint = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            lastTargetPoint = target.transform.position;
            transform.LookAt(target.transform);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, lastTargetPoint, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, lastTargetPoint) < 0.5f)
                Destroy(gameObject);
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(other);     
            Destroy(gameObject);
        }
    }

    protected virtual void TakeDamage(Collider other) {}
}
