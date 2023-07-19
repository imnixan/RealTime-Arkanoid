using System.Collections;
using UnityEngine;

public class SoundManager : Manager
{
    [SerializeReference]
    private AudioClip[] explosions;

    private AudioSource soundPlayer;

    private void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
    }

    protected override void Subscribe()
    {
        gm.MeteoritDestroyed += OnExplosion;
        gm.MeteoritFallen += OnMeteoritFallen;
    }

    protected override void Unsubscribe()
    {
        gm.MeteoritDestroyed -= OnExplosion;
        gm.MeteoritFallen -= OnMeteoritFallen;
    }

    private void OnMeteoritFallen()
    {
        OnExplosion();
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
