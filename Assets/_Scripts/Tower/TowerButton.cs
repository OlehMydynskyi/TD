using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private Towers tower;
    [SerializeField] private GameObject towerUI;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChooseTower);
    }

    public void ChooseTower()
    {
        towerUI.GetComponent<TowerUIControllser>().ChooseTower(tower);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
