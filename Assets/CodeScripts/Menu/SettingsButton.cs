using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsButton : MonoBehaviour
{
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
        InitialPrefString();
        currentStatus = PlayerPrefs.GetString(
            PlayerPrefsString,
            StaticConstants.TurnedOnSettingsValue
        );
        InitialSprites();
        SetSprite();
    }

    private void InitialPrefString()
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
    }

    private void InitialSprites()
    {
        onSprite = Resources.Load<Sprite>(
            $"SettingsButtons/{PlayerPrefsString}{StaticConstants.TurnedOnSettingsValue}"
        );
        offSprite = Resources.Load<Sprite>(
            $"SettingsButtons/{PlayerPrefsString}{StaticConstants.TurnedOffSettingsVaule}"
        );
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
