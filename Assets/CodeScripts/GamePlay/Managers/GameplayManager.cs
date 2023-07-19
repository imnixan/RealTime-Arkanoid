using UnityEngine;
using UnityEngine.Events;

public class GameplayManager : Manager
{
    public event UnityAction MeteoritFallen;
    public event UnityAction MeteoritDestroyed;
    public event UnityAction PlayerHadShot;
    public event UnityAction BulletHadBounce;
    public event UnityAction GameEnd;

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
        GetComponentInChildren<Walls>().BulletBounced += OnBulletBounced;
        GetComponentInChildren<Gun>().PlayerShoot += OnPlayerShoot;
        Meteorit.MeteoritDestroyed += OnMeteoritDestroy;
        uiManager.OnGameEnd += GameEndBroadCast;
    }

    protected override void Unsubscribe()
    {
        GetComponentInChildren<Walls>().MeteoritEnter -= OnMeteoritFall;
        GetComponentInChildren<Walls>().BulletBounced -= OnBulletBounced;
        GetComponentInChildren<Gun>().PlayerShoot -= OnPlayerShoot;
        Meteorit.MeteoritDestroyed -= OnMeteoritDestroy;
        uiManager.OnGameEnd -= GameEndBroadCast;
    }

    private void OnBulletBounced()
    {
        BulletHadBounce?.Invoke();
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
        GameEnd?.Invoke();
        BroadcastMessage("OnGameEnd");
    }

    private void OnPlayerShoot()
    {
        PlayerHadShot?.Invoke();
    }
}
