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
        channelNameText.text = "#general";
    }

    public void OnSendButtonClicked()
    {
        if (string.IsNullOrWhiteSpace(messageInputField.text))
            return;

        SpawnMessageItem(currentUsername, messageInputField.text);
        messageInputField.text = string.Empty;
        messageInputField.ActivateInputField();
        ScrollToBottom();
    }

    public void OnGoToServersButtonClicked()
    {
        UIManager.Instance.ShowPanel("ServerListPanel");
    }

    public void OnGoToProfileButtonClicked()
    {
        UIManager.Instance.ShowPanel("ProfilePanel");
    }

    private void SpawnMessageItem(string sender, string content)
    {
        GameObject item = Instantiate(messageItemPrefab, messagesContainer);
        MessageItem messageItem = item.GetComponent<MessageItem>();
        messageItem.SetData(sender, content);
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        messagesScrollRect.verticalNormalizedPosition = 0f;
    }
}
