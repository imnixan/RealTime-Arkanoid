using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private RectTransform windowRt,
        textRt;

    public void Restart()
    {
        SceneManager.LoadScene("CitySaver");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnGameEnd()
    {
        StartCoroutine(ShowEndWindow());
    }

    private IEnumerator ShowEndWindow()
    {
        float Speed = 10;
        while (windowRt.anchoredPosition.y < 0)
        {
            windowRt.anchoredPosition = Vector2.MoveTowards(
                windowRt.anchoredPosition,
                Vector2.zero,
                Speed
            );
            textRt.anchoredPosition = Vector2.MoveTowards(
                textRt.anchoredPosition,
                Vector2.zero,
                Speed
            );
            yield return new WaitForFixedUpdate();
        }
    }
}
