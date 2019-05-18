using UnityEngine;
using System.Collections.Generic;

public class MenuPageGroup : IMenuPage
{
    public IMenuPage InitialPage;
    public IMenuPage ActivePage;

    private List<IMenuPage> pages = new List<IMenuPage>();

    private void Awake()
    {
        Debug.Assert(InitialPage != null, "InitialPage must be set.");

        foreach (Transform t in transform)
        {
            var page = t.gameObject.GetComponent<IMenuPage>();
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
    
    public void SwitchToPage(IMenuPage page)
    {
        if (page == null)
            return;

        if (!pages.Contains(page))
        {
            var parentPageGroup = transform.parent.GetComponentInParent<MenuPageGroup>();
            if (parentPageGroup != null)
                parentPageGroup.SwitchToPage(page);

            return;
        }

        ActivePage.gameObject.SetActive(false);
        ActivePage = page;
        ActivePage.gameObject.SetActive(true);
    }
}
