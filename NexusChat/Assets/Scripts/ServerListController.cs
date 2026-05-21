using UnityEngine;

public class ServerListController : MonoBehaviour
{
    public void OnServerSelected()
    {
        UIManager.Instance.ShowPanel("ChatPanel");
    }

    public void OnGoToProfileButtonClicked()
    {
        UIManager.Instance.ShowPanel("ProfilePanel");
    }
}
