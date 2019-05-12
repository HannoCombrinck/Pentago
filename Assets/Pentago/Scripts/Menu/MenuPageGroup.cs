using UnityEngine;
using System.Collections.Generic;

public class MenuPageGroup : MonoBehaviour
{
    public MenuPage InitialPage;
    public MenuPage ActivePage;

    private List<MenuPage> pages = new List<MenuPage>();

    private void Awake()
    {
        Debug.Assert(InitialPage != null, "InitialPage must be set.");

        foreach (Transform t in transform)
        {
            var page = t.gameObject.GetComponent<MenuPage>();
            if (page != null)
            {
                pages.Add(page);
                page.gameObject.SetActive(false);
            }
        }

        if (!pages.Contains(InitialPage))
        {
            Debug.LogError("InitialPage is not a child of this " + gameObject.name + " page group.");
            return;
        }

        ActivePage = InitialPage;
        ActivePage.gameObject.SetActive(true);
    }
    
    public void SwitchToPage(MenuPage page)
    {
        if (page == null)
            return;

        if (!pages.Contains(page))
            return;

        ActivePage.gameObject.SetActive(false);
        ActivePage = page;
        ActivePage.gameObject.SetActive(true);
    }
}
