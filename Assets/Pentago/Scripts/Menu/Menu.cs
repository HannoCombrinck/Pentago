using UnityEngine;

public class Menu : MonoBehaviour
{
    public MenuPageGroup MainMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            MainMenu.gameObject.SetActive(!MainMenu.gameObject.activeSelf);
    }

    public void ShowMenu()
    {
        MainMenu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        MainMenu.gameObject.SetActive(false);
    }
}
