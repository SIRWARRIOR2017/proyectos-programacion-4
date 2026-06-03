using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI emailText;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumePercentageText;

    void OnEnable()
    {
        LoadProfile();
        LoadSavedVolume();
    }

    void OnDisable()
    {
        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
    }

    private void LoadProfile()
    {
        usernameText.text = PlayerPrefs.GetString("username", "Usuario");
        emailText.text = PlayerPrefs.GetString("email", "usuario@email.com");
    }

    private void LoadSavedVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("notificationVolume", 0.7f);
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            // Añadimos listener para que el texto se actualice cuando el usuario mueva el slider
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
        UpdateVolumeText(savedVolume);
    }

    public void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("notificationVolume", value);
        UpdateVolumeText(value);
        DebugLogger.Log("ProfileController: OnVolumeChanged value=" + value);
    }

    private void UpdateVolumeText(float value)
    {
        if (volumePercentageText != null)
            volumePercentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnLogoutButtonClicked()
    {
        // No borramos todas las PlayerPrefs aquí porque contiene las credenciales guardadas
        // (PlayerPrefs.DeleteAll() elimina email/password y provoca que no se pueda volver a iniciar sesión).
        // En su lugar, reseteamos solo el estado de sesión si es necesario.
        PlayerPrefs.SetInt("isLoggedIn", 0);
        PlayerPrefs.Save();
        DebugLogger.Log("ProfileController: OnLogoutButtonClicked - cleared session flag, returning to LoginPanel");
        UIManager.Instance.ShowPanel("LoginPanel");
    }

    public void OnBackButtonClicked()
    {
        UIManager.Instance.ShowPanel("ChatPanel");
    }
}
