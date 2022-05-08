using UnityEngine;
using UnityEngine.UI;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject loseUI;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void GetDamage(float damage)
    {
        currentHP -= damage;
        slider.value = currentHP;
        if (currentHP <= 0)
        {
            loseUI.SetActive(true);
            Destroy(gameObject);
        } 
    }
}
