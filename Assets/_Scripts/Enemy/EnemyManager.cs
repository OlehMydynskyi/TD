using System.Collections;
using UnityEngine;
using Object_Pooling;
using System;

public class EnemyManager : MonoBehaviour, IPoolable
{
    private float currentHP;
    [SerializeField] private float physicalDamageMultiplayer = 1;
    [SerializeField] private float fireDamageMultiplayer = 1;
    [SerializeField] private int revard;
    private LVLManager lvlManager;

    public float CurrentHP { get => currentHP; }

    public Transform Transform => transform;
    public GameObject GameObject => gameObject;
    public event Action<IPoolable> OnReturnToPool;
    public void ReturnToPool()
    {
        OnReturnToPool?.Invoke(this);
    }


    void Start()
    {
        lvlManager = LVLManager.Instance;
    }

    public void OnSpawn (Transform spownPoint, float HP)
    {
        transform.position = spownPoint.position;
        ChangeHP(HP);
    }

    public void GetDamage (float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                ChangeHP(currentHP - damage * physicalDamageMultiplayer);
                break;
            case DamageType.Fire:
                ChangeHP(currentHP - damage * fireDamageMultiplayer);
                break;
        }

         
    }

    private void ChangeHP(float HP)
    {
        currentHP = HP;
        if (currentHP <= 0)
        {
            OnDeath();
            return;
        }  
        float scale = HP / 100;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void OnDeath ()
    {
        StopCoroutine("Burning");
        //lvlManager.ChangeCoins(revard);
        ReturnToPool();
    }

    IEnumerator Burning(float damage)
    {
        for (int i = 0; i < 5; i++)
        {
            GetDamage(damage, DamageType.Fire);
            yield return new WaitForSeconds(1f);
        }   
    }

}

public enum DamageType
{
    Physical = 1,
    Fire = 2,
}

