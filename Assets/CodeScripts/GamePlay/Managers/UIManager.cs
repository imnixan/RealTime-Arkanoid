using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(DefaultSceneSettings))]
public class UIManager : Manager
{
    public event UnityAction OnGameEnd;

    private void OnHpEnd()
    {
        OnGameEnd?.Invoke();
        BroadcastMessage("OnGameEnd");
    }

    private void MeteoritFallBroadCast()
    {
        BroadcastMessage("OnMeteoritFall");
    }

    private void MeteoritDestroyedBroadCast()
    {
        BroadcastMessage("OnMeteoritDestroyed");
    }

    protected override void Subscribe()
    {
        GetComponentInChildren<HPBehaviour>().NullHP += OnHpEnd;
        gm.MeteoritFallen += MeteoritFallBroadCast;
        gm.MeteoritDestroyed += MeteoritDestroyedBroadCast;
    }

    protected override void Unsubscribe()
    {
        GetComponentInChildren<HPBehaviour>().NullHP -= OnHpEnd;
        gm.MeteoritFallen -= MeteoritFallBroadCast;
        gm.MeteoritDestroyed -= MeteoritDestroyedBroadCast;
    }
}
