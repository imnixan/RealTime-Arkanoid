using UnityEngine;

public class DefaultSceneSettings : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
