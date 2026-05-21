using UnityEngine;
using TMPro;

public class MessageItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI senderText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private TextMeshProUGUI timestampText;

    public void SetData(string sender, string content)
    {
        senderText.text = sender;
        contentText.text = content;
        timestampText.text = System.DateTime.Now.ToString("HH:mm");
    }
}
