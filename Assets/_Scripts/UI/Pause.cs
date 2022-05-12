using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private Button button;
    private static bool isPause = false;
    [SerializeField] private GameObject menu;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeTimeScale);
    }

    private void ChangeTimeScale()
    {
        Time.timeScale = isPause ? 1 : 0;
        isPause = !isPause;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}