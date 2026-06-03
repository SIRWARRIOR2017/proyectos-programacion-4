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

        // Validar credenciales contra lo registrado
        string savedEmail = PlayerPrefs.GetString("email", string.Empty).ToLowerInvariant().Trim();
        string savedPassword = PlayerPrefs.GetString("password", string.Empty);
        string enteredEmail = emailInputField.text?.Trim().ToLowerInvariant() ?? string.Empty;
        string enteredPassword = passwordInputField.text ?? string.Empty;

        DebugLogger.Log("LoginController: intentando login enteredEmail='" + enteredEmail + "'");
        DebugLogger.Log("LoginController: savedEmail='" + savedEmail + "' savedPasswordLength=" + (string.IsNullOrEmpty(savedPassword) ? 0 : savedPassword.Length));

        if (enteredEmail != savedEmail || enteredPassword != savedPassword)
        {
            ShowError("Credenciales inválidas. Usá la cuenta registrada.");
            return;
        }

        DebugLogger.Log("LoginController: login exitoso email='" + emailInputField.text + "'");
        UIManager.Instance.ShowPanel("ServerListPanel");
    }

    public void OnGoToRegisterButtonClicked()
    {
        UIManager.Instance.ShowPanel("RegisterPanel");
    }

    private bool AreFieldsValid()
    {
        // Trim inputs to avoid accidental leading/trailing spaces
        emailInputField.text = emailInputField.text?.Trim() ?? string.Empty;
        passwordInputField.text = passwordInputField.text?.Trim() ?? string.Empty;

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

        // Minimum password length (coincidir con el valor de registro)
        if (passwordInputField.text.Length < 6)
        {
            ShowError("La contraseña debe tener al menos 8 caracteres.");
            return false;
        }

        // Clear any previous error when validation passes
        errorText.gameObject.SetActive(false);
        return true;
    }

    private void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
