using System.Collections;
using UnityEngine;

public class SoundManager : Manager
{
    [SerializeReference]
    private AudioClip[] explosions;

    [SerializeReference]
    private AudioClip shot,
        bulletBounce,
        endGame;

    private AudioSource soundPlayer;

    private void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
        soundPlayer.volume = 0.6f;
    }

    protected override void Subscribe()
    {
        gm.MeteoritDestroyed += OnExplosion;
        gm.MeteoritFallen += OnMeteoritFallen;
        gm.PlayerHadShot += OnPlayerShot;
        gm.BulletHadBounce += OnBulletBounce;
        gm.GameEnd += OnGameEnd;
    }

    protected override void Unsubscribe()
    {
        gm.MeteoritDestroyed -= OnExplosion;
        gm.MeteoritFallen -= OnMeteoritFallen;
        gm.PlayerHadShot -= OnPlayerShot;
        gm.BulletHadBounce -= OnBulletBounce;
        gm.GameEnd -= OnGameEnd;
    }

    private void OnBulletBounce()
    {
        PlaySound(bulletBounce);
    }

    private void OnPlayerShot()
    {
        PlaySound(shot);
    }

    private void OnMeteoritFallen()
    {
        OnExplosion();
        PlayVibration();
    }

    private void OnGameEnd()
    {
        PlaySound(endGame);
        PlayVibration();
    }

    private void OnExplosion()
    {
        PlaySound(explosions[Random.Range(0, explosions.Length)]);
    }

    private void PlaySound(AudioClip clip)
    {
        if (
            PlayerPrefs.GetString(
                StaticConstants.SoundSettingsPrefs,
                StaticConstants.TurnedOnSettingsValue
            ) == StaticConstants.TurnedOnSettingsValue
        )
        {
            soundPlayer.PlayOneShot(clip);
        }
    }

    private void PlayVibration()
    {
        if (
            PlayerPrefs.GetString(
                StaticConstants.VibroSettingsPrefs,
                StaticConstants.TurnedOnSettingsValue
            ) == StaticConstants.TurnedOnSettingsValue
        )
        {
            Handheld.Vibrate();
        }
    }
}
