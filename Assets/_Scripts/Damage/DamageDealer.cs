using UnityEngine;
using Object_Pooling;
using System;

public abstract class DamageDealer : MonoBehaviour, IPoolable
{
    [HideInInspector] public GameObject target;
    [SerializeField] private float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected DamageType damageType;
    [SerializeField] protected LayerMask enemyLayer;
    private Vector3 lastTargetPoint;

    public event Action<IPoolable> OnReturnToPool;
    public Transform Transform => transform;
    public GameObject GameObject => gameObject;
    public void ReturnToPool()
    {
        OnReturnToPool?.Invoke(this);
    }


    void Start()
    {
        lastTargetPoint = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        if (target != null && target.activeInHierarchy == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
            lastTargetPoint = target.transform.position;
            transform.LookAt(target.transform);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, lastTargetPoint, Time.deltaTime * speed);
            transform.LookAt(lastTargetPoint);
            if (Vector3.Distance(transform.position, lastTargetPoint) < 0.5f)
                ReturnToPool();
        }
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(other);
            ReturnToPool();
        }
    }

    protected virtual void TakeDamage(Collider other) {}
}
