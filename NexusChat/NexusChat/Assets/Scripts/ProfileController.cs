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
