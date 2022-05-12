using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Quit);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}