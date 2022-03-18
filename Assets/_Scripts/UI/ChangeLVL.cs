using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeLVL : MonoBehaviour
{
    private Button button;
    [SerializeField] private bool thisLvl;
    [SerializeField] private bool nextLvl;
    [SerializeField] private Scene scene;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeLvl);

        if (thisLvl) scene = (Scene)SceneManager.GetActiveScene().buildIndex;
        else if (nextLvl)
        {
            scene = (Scene)SceneManager.GetActiveScene().buildIndex + 1;

            if ((int)scene >= SceneManager.sceneCountInBuildSettings)
                scene = Scene.MainMenu;
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void ChangeLvl()
    {
        SceneManager.LoadScene((int)scene);
    }

}

public enum Scene
{
    MainMenu,
    First,
    Second,
    Third,
    Fourth,
}