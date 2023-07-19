using System.Collections;
using UnityEngine;

public class BgManager : Manager
{
    [SerializeField]
    private ParticleSystem[] ps;
    private int particleQueue;

    protected override void Subscribe()
    {
        gm.MeteoritFallen += OnMeteoritFall;
    }

    protected override void Unsubscribe()
    {
        gm.MeteoritFallen -= OnMeteoritFall;
    }

    private void OnMeteoritFall()
    {
        if (particleQueue < ps.Length)
        {
            ps[particleQueue].Play();
            particleQueue++;
        }
    }
}
