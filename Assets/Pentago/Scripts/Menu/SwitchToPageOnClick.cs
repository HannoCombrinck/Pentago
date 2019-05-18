using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SwitchToPageOnClick : MonoBehaviour
{
    public IMenuPage switchToPage;

    private MenuPageGroup pageGroup;
    private Button button;

    private void Awake()
    {
        Debug.Assert(switchToPage != null, "PageToSwitchTo is required.");
        pageGroup = GetComponentInParent<MenuPageGroup>();
        Debug.Assert(pageGroup != null, "Couldn't find MenuPageGroup in parent.");
        button = GetComponent<Button>();
        button.onClick.AddListener(SwitchToPage);
    }

    private void SwitchToPage()
    {
        pageGroup.SwitchToPage(switchToPage);
    }
}
