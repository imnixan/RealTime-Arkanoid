using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsButton : MonoBehaviour
{
    [SerializeReference]
    private Sprite onSprite,
        offSprite;

    [SerializeField]
    private MenuManager.SettingsType buttonType;

    private string PlayerPrefsString;

    private Image buttonImage;
    private string currentStatus;
    private TextMeshProUGUI settingsStatus;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
        settingsStatus = GetComponentInChildren<TextMeshProUGUI>();
        Init();
        GetComponent<Button>().onClick.AddListener(UpdateSettings);
    }

    private void Init()
    {
        switch (buttonType)
        {
            case MenuManager.SettingsType.SoundSettings:
                PlayerPrefsString = StaticConstants.SoundSettingsPrefs;
                break;
            case MenuManager.SettingsType.VibroSettings:
                PlayerPrefsString = StaticConstants.VibroSettingsPrefs;
                break;
        }

        currentStatus = PlayerPrefs.GetString(
            PlayerPrefsString,
            StaticConstants.TurnedOnSettingsValue
        );
        SetSprite();
    }

    private void SetSprite()
    {
        if (currentStatus == StaticConstants.TurnedOnSettingsValue)
        {
            buttonImage.sprite = onSprite;
        }
        else
        {
            buttonImage.sprite = offSprite;
        }
        settingsStatus.text = $"{PlayerPrefsString} {currentStatus}";
    }

    private void UpdateSettings()
    {
        currentStatus = TurnedOn()
            ? StaticConstants.TurnedOffSettingsVaule
            : StaticConstants.TurnedOnSettingsValue;
        PlayerPrefs.SetString(PlayerPrefsString, currentStatus);
        PlayerPrefs.Save();
        SetSprite();
    }

    private bool TurnedOn()
    {
        return currentStatus == StaticConstants.TurnedOnSettingsValue;
    }
}
