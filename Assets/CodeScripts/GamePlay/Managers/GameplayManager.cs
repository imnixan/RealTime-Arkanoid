using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : Manager
{
    public event UnityAction MeteoritFallen;
    public event UnityAction MeteoritDestroyed;

    [SerializeReference]
    private GameObject explosionPrefab;

    private const float GameSpeedAddition = 0.03f;

    private void OnMeteoritFall()
    {
        MeteoritFallen?.Invoke();
    }

    protected override void Subscribe()
    {
        GetComponentInChildren<Walls>().MeteoritEnter += OnMeteoritFall;
        Meteorit.MeteoritDestroyed += OnMeteoritDestroy;
        uiManager.OnGameEnd += GameEndBroadCast;
    }

    protected override void Unsubscribe()
    {
        GetComponentInChildren<Walls>().MeteoritEnter -= OnMeteoritFall;
        Meteorit.MeteoritDestroyed -= OnMeteoritDestroy;
        uiManager.OnGameEnd -= GameEndBroadCast;
    }

    private void OnMeteoritDestroy(bool byPlayer, Vector2 position)
    {
        Instantiate(explosionPrefab, position, new Quaternion(), transform);
        if (byPlayer)
        {
            MeteoritDestroyed?.Invoke();
            GlobalStat.GameSpeed += GameSpeedAddition;
        }
    }

    private void GameEndBroadCast()
    {
        BroadcastMessage("OnGameEnd");
    }
}
