using UnityEngine;

public class TowerUIControllser : MonoBehaviour
{
    [HideInInspector] public PlatformManager platform;

    public void ChooseTower(Towers tower)
    {
        platform.ChooseTower(tower);
    }

    public void RemoveTower()
    {
        platform.RemoveTower();
    }
}