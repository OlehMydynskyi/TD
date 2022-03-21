using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float maxHP;
    private float currentHP;
    [SerializeField] private float physicalDamageMultiplayer = 1;
    [SerializeField] private float fireDamageMultiplayer = 1;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private int revard;
    private LVLManager lvlManager;

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
        //Debug.Log("HP: " + currentHP);
        if (currentHP <= 0)
        {
            StopCoroutine("Burning");
            lvlManager.ChangeCoins(revard);
            Destroy(gameObject);
        }
            
            
    }

    //temp
    public float GetHP()
    {
        return currentHP;
    }
    //temp

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

