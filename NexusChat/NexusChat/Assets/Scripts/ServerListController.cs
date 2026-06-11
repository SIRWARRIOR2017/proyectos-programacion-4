using UnityEngine;

public class ServerListController : MonoBehaviour
{
    public void OnServerSelected()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance es null. Asegurate de que el objeto UIManager exista en la escena.");
            return;
        }
        UIManager.Instance.ShowPanel("ChatPanel");
    }

    public void OnServerSelected(string serverName)
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance es null. Asegurate de que el objeto UIManager exista en la escena.");
            return;
        }

        PlayerPrefs.SetString("currentChannel", serverName);
        DebugLogger.Log("ServerListController: servidor seleccionado='" + serverName + "'");
        UIManager.Instance.ShowPanel("ChatPanel");
    }

    public void OnGoToProfileButtonClicked()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance es null. Asegurate de que el objeto UIManager exista en la escena.");
            return;
        }
        UIManager.Instance.ShowPanel("ProfilePanel");
    }
}
