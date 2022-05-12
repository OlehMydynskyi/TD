using UnityEngine;
using UnityEngine.UI;

public class ChangeMenuStatus : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject menu;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UseLvlMenu);
    }

    private void UseLvlMenu()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}