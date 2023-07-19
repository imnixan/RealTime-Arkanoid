using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(DefaultSceneSettings))]
public class MenuManager : MonoBehaviour
{
    public enum SettingsType
    {
        SoundSettings,
        VibroSettings
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CitySaver");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
