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

    private void LoadProfile()
    {
        usernameText.text = PlayerPrefs.GetString("username", "Usuario");
        emailText.text = PlayerPrefs.GetString("email", "usuario@email.com");
    }

    private void LoadSavedVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("notificationVolume", 0.7f);
        volumeSlider.value = savedVolume;
        UpdateVolumeText(savedVolume);
    }

    public void OnVolumeChanged(float value)
    {
        PlayerPrefs.SetFloat("notificationVolume", value);
        UpdateVolumeText(value);
    }

    private void UpdateVolumeText(float value)
    {
        volumePercentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    public void OnLogoutButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        UIManager.Instance.ShowPanel("LoginPanel");
    }

    public void OnBackButtonClicked()
    {
        UIManager.Instance.ShowPanel("ChatPanel");
    }
}
