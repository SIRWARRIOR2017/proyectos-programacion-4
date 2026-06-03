using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatController : MonoBehaviour
{
    [SerializeField] private TMP_InputField messageInputField;
    [SerializeField] private Transform messagesContainer;
    [SerializeField] private GameObject messageItemPrefab;
    [SerializeField] private ScrollRect messagesScrollRect;
    [SerializeField] private TextMeshProUGUI channelNameText;

    private string currentUsername;

    void OnEnable()
    {
        currentUsername = PlayerPrefs.GetString("username", "Usuario");

        // Leer el canal/servidor seleccionado (guardado por ServerListController)
        string currentChannel = PlayerPrefs.GetString("currentChannel", "#general");
        if (string.IsNullOrEmpty(currentChannel))
            currentChannel = "#general";

        if (channelNameText != null)
            channelNameText.text = currentChannel.StartsWith("#") ? currentChannel : "#" + currentChannel;
        else
            DebugLogger.LogWarning("ChatController: channelNameText no está asignado en el Inspector.");
        DebugLogger.Log("ChatController: OnEnable currentChannel='" + currentChannel + "' username='" + currentUsername + "'");
    }

    public void OnSendButtonClicked()
    {
        if (messageInputField == null)
        {
            Debug.LogError("ChatController: messageInputField no está asignado en el Inspector.");
            return;
        }

        if (string.IsNullOrWhiteSpace(messageInputField.text))
            return;

        DebugLogger.Log("ChatController: OnSendButtonClicked text='" + messageInputField.text + "'");
        SpawnMessageItem(currentUsername, messageInputField.text);
        messageInputField.text = string.Empty;
        messageInputField.ActivateInputField();
        ScrollToBottom();
    }

    public void OnGoToServersButtonClicked()
    {
        if (UIManager.Instance == null)
        {
            Debug.LogError("UIManager.Instance es null. Asegurate de que el objeto UIManager exista en la escena.");
            return;
        }
        UIManager.Instance.ShowPanel("ServerListPanel");
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

    private void SpawnMessageItem(string sender, string content)
    {
        if (messageItemPrefab == null)
        {
            Debug.LogError("ChatController: messageItemPrefab no está asignado en el Inspector.");
            return;
        }

        if (messagesContainer == null)
        {
            Debug.LogError("ChatController: messagesContainer (Content del ScrollView) no está asignado en el Inspector.");
            return;
        }

        GameObject item = Instantiate(messageItemPrefab, messagesContainer);
        if (item == null)
        {
            Debug.LogError("ChatController: Instantiate devolvió null para messageItemPrefab.");
            return;
        }

        // Asegurar que el item tiene MessageItem y configurarlo
        MessageItem messageItem = item.GetComponent<MessageItem>();
        if (messageItem == null)
        {
            Debug.LogError("ChatController: el prefab messageItemPrefab no contiene el componente MessageItem.");
            return;
        }

        messageItem.SetData(sender, content);
        DebugLogger.Log("ChatController: SpawnMessageItem sender='" + sender + "' content='" + (content.Length > 50 ? content.Substring(0, 50) + "..." : content) + "'");

        // Forzar que el layout se actualice y el nuevo item quede al final
        item.transform.SetAsLastSibling();
        var rt = messagesContainer as RectTransform;
        if (rt != null)
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
        else
            DebugLogger.LogWarning("ChatController: messagesContainer no es RectTransform");
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        if (messagesScrollRect != null)
            messagesScrollRect.verticalNormalizedPosition = 0f;
        else
            Debug.LogWarning("ChatController: messagesScrollRect no está asignado en el Inspector.");
    }
}
