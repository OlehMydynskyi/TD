using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Object_Pooling;
using System;

public class EnemyManager : MonoBehaviour, IPoolable
{
    [SerializeField] private float maxHP;
    private float currentHP;
    [SerializeField] private float physicalDamageMultiplayer = 1;
    [SerializeField] private float fireDamageMultiplayer = 1;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private int revard;
    private LVLManager lvlManager;

    public Transform Transform => transform;
    public GameObject GameObject => gameObject;
    public event Action<IPoolable> OnReturnToPool;
    public void ReturnToPool()
    {
        OnReturnToPool?.Invoke(this);
    }


    void Start()
    {
        currentHP = maxHP;
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
        lvlManager = LVLManager.Instance;
    }

    void FixedUpdate()
    {
        canvas.transform.rotation = new Quaternion(0, transform.rotation.y * -1, 0, 0);
    }

    public void GetDamage (float damage, DamageType damageType)
    {
        switch (damageType)
        {
            case DamageType.Physical:
                currentHP -= damage * physicalDamageMultiplayer;
                break;
            case DamageType.Fire:
                currentHP -= damage * fireDamageMultiplayer;
                break;
        }

        hpSlider.value = currentHP;

        if (currentHP <= 0)
            OnDeath(); 
    }

    public void OnDeath ()
    {
        StopCoroutine("Burning");
        lvlManager.ChangeCoins(revard);
        ReturnToPool();
        currentHP = maxHP;
        hpSlider.value = maxHP;
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