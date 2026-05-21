using UnityEngine;
using TMPro;

public class RegisterController : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField emailInputField;
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private TMP_InputField confirmPasswordInputField;
    [SerializeField] private TextMeshProUGUI errorText;

    void OnEnable()
    {
        errorText.gameObject.SetActive(false);
    }

    public void OnRegisterButtonClicked()
    {
        if (!AreFieldsValid())
            return;

        PlayerPrefs.SetString("username", usernameInputField.text);
        PlayerPrefs.SetString("email", emailInputField.text);
        UIManager.Instance.ShowPanel("ServerListPanel");
    }

    public void OnGoToLoginButtonClicked()
    {
        UIManager.Instance.ShowPanel("LoginPanel");
    }

    private bool AreFieldsValid()
    {
        if (string.IsNullOrEmpty(usernameInputField.text) ||
            string.IsNullOrEmpty(emailInputField.text) ||
            string.IsNullOrEmpty(passwordInputField.text) ||
            string.IsNullOrEmpty(confirmPasswordInputField.text))
        {
            ShowError("Por favor completá todos los campos.");
            return false;
        }

        if (!emailInputField.text.Contains("@"))
        {
            ShowError("Ingresá un correo electrónico válido.");
            return false;
        }

        if (passwordInputField.text != confirmPasswordInputField.text)
        {
            ShowError("Las contraseñas no coinciden.");
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
