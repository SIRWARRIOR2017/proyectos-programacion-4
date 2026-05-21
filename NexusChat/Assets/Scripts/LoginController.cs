using UnityEngine;
using TMPro;

public class LoginController : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TextMeshProUGUI errorText;

    void OnEnable()
    {
        errorText.gameObject.SetActive(false);
        emailInputField.text = string.Empty;
        passwordInputField.text = string.Empty;
    }

    public void OnLoginButtonClicked()
    {
        if (!AreFieldsValid())
            return;

        UIManager.Instance.ShowPanel("ServerListPanel");
    }

    public void OnGoToRegisterButtonClicked()
    {
        UIManager.Instance.ShowPanel("RegisterPanel");
    }

    private bool AreFieldsValid()
    {
        if (string.IsNullOrEmpty(emailInputField.text) || string.IsNullOrEmpty(passwordInputField.text))
        {
            ShowError("Por favor completá todos los campos.");
            return false;
        }

        if (!emailInputField.text.Contains("@"))
        {
            ShowError("Ingresá un correo electrónico válido.");
            return false;
        }

        if (passwordInputField.text.Length < 6)
        {
            ShowError("La contraseña debe tener al menos 6 caracteres.");
            return false;
        }

        return true;
    }

    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
