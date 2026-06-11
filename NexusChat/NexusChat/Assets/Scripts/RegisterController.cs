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

        string username = usernameInputField?.text?.Trim() ?? string.Empty;
        string email = emailInputField?.text?.Trim().ToLowerInvariant() ?? string.Empty;
        string password = passwordInputField?.text ?? string.Empty;

 
        string existingEmail = PlayerPrefs.GetString("email", string.Empty);
        if (!string.IsNullOrEmpty(existingEmail) && existingEmail == email)
        {
            ShowError("Ya existe una cuenta registrada con ese correo.");
            return;
        }

        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("email", email);
        
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();
        DebugLogger.Log("RegisterController: datos guardados en PlayerPrefs email='" + email + "' username='" + username + "'");
        DebugLogger.Log("RegisterController: registro exitoso username='" + usernameInputField.text + "' email='" + emailInputField.text + "'");
        UIManager.Instance.ShowPanel("ServerListPanel");
    }

    public void OnGoToLoginButtonClicked()
    {
        UIManager.Instance.ShowPanel("LoginPanel");
    }

    private bool AreFieldsValid()
    {
        string username = usernameInputField?.text?.Trim() ?? string.Empty;
        string email = emailInputField?.text?.Trim() ?? string.Empty;
        string password = passwordInputField?.text ?? string.Empty; 
        string confirm = confirmPasswordInputField?.text ?? string.Empty;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
        {
            ShowError("Por favor completá todos los campos.");
            return false;
        }

        if (!email.Contains("@"))
        {
            ShowError("Ingresá un correo electrónico válido.");
            return false;
        }

        
        if (!string.Equals(password, confirm, System.StringComparison.Ordinal))
        {
            ShowError("Las contraseñas no coinciden.");
            return false;
        }

        if (password.Length < 6)
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
