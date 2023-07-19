using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public event UnityAction PlayerShoot;

    [SerializeReference]
    private GameObject bulletPrefab;

    private GunLaserSight gunLaserSight;
    private GunSight gunSight;
    private float shotsPerSeconds;
    private float lastShootTime;
    private float Timer
    {
        get { return 1f / shotsPerSeconds; }
    }

    public bool CanShootByTimer
    {
        get { return Time.time - lastShootTime > Timer; }
    }

    private void Start()
    {
        gunSight = Instantiate(new GameObject("GunSight"), transform).AddComponent<GunSight>();
        gunSight.Init(transform.parent.GetComponent<RectTransform>(), transform);

        gunLaserSight = Instantiate(new GameObject("GunLaserSight"), transform)
            .AddComponent<GunLaserSight>();
        gunLaserSight.Init(GetComponent<RectTransform>());
        lastShootTime = Time.time;
    }

    public void UpdateSightStatus(bool sightActive)
    {
        gunLaserSight.SetLaserStatus(sightActive);

        if (!sightActive && CanShootByTimer)
        {
            lastShootTime = Time.time;
            Shoot();
        }
    }

    private void Update()
    {
        shotsPerSeconds = GlobalStat.GameSpeed * 0.8f;
    }

    private void Shoot()
    {
        PlayerShoot?.Invoke();
        Bullet bullet = Instantiate(
                bulletPrefab,
                gunLaserSight.GetStartPosition(),
                new Quaternion(),
                transform.parent
            )
            .GetComponent<Bullet>();
        bullet.BulletShot(gunLaserSight.GetEndPoint());
    }

    private void OnGameEnd()
    {
        gunLaserSight.enabled = false;
        gunSight.enabled = false;
        this.enabled = false;
    }
}
