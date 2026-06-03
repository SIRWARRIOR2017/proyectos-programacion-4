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

    // Sobrecarga que acepta el nombre del servidor (se puede pasar desde el Button -> OnClick con un string)
    public void OnServerSelected(string serverName)
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance es null. Asegurate de que el objeto UIManager exista en la escena.");
            return;
        }

        // Guardamos el nombre del canal/servidor para que ChatController lo lea al activarse
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
