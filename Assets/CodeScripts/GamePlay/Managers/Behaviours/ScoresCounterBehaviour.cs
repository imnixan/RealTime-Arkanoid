using System.Collections;
using UnityEngine;
using TMPro;

public class ScoresCounterBehaviour : MonoBehaviour
{
    private TextMeshProUGUI scoresCounter;
    private int currentScores;

    private void Start()
    {
        scoresCounter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnMeteoritDestroyed()
    {
        currentScores++;
        scoresCounter.text = currentScores.ToString();
    }
}
