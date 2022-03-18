using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private Vector3 d;
    private LVLManager lvlManager;
    private GameObject currentTower;
    private bool isPlaced = false;

    private void Start()
    {
        lvlManager = LVLManager.Instance;
    }
    private void OnMouseDown()
    {
        towerUI.SetActive(true);
        towerUI.GetComponent<TowerUIControllser>().platform = this;
    }

    public void ChooseTower (Towers tower)
    {
        if (towers[(int)tower].GetComponent<TowerManager>().cost > lvlManager.coins || isPlaced)
        {
            Debug.Log("Cost: " + (towers[(int)tower].GetComponent<TowerManager>().cost > lvlManager.coins));
            Debug.Log("Cost: " + (towers[(int)tower].GetComponent<TowerManager>().cost));
            Debug.Log("Plased: " + isPlaced);
            return;
        }

        currentTower = GameObject.Instantiate(towers[(int)tower], gameObject.transform.position + d, Quaternion.identity);
        lvlManager.ChangeCoins(-towers[(int)tower].GetComponent<TowerManager>().cost);
        towerUI.SetActive(false);
        isPlaced = true;
    }

    public void RemoveTower()
    {
        if (!isPlaced)
            return;

        lvlManager.ChangeCoins(currentTower.GetComponent<TowerManager>().cost / 2);
        Destroy(currentTower);
        isPlaced = false;
    }
}

public enum Towers
{
    SimpleTower,
    RocketLauncher,
    Flamer,
    DethLaser,
}