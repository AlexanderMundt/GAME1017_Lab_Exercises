/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 4
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicSource, sfxSource;

    public event Action<float, EAudioType> OnVolumeChangeFinished;
    private Action<float, EAudioType> OnVolumeChangeFinishedStorage;

    private void OnDisable()
    {
        OnVolumeChangeFinished -= OnVolumeChangeFinishedStorage;
    }

    private void Start()
    {
        OnVolumeChangeFinishedStorage = GameManager.Instance.SaveSystem.SaveVolume;
        OnVolumeChangeFinished += OnVolumeChangeFinishedStorage;

        if (musicSource)
        {
            musicSource.loop = true;
            musicSource.Play();
        }

        if (sfxSource)
        {
            sfxSource.loop = true;
            sfxSource.Play();
        }
    }

    public void ChangeMusicVolume(float newVolume)
    {
        musicSource.volume = newVolume;
    }

    public void ChangeSfxVolume(float newVolume)
    {
        sfxSource.volume = newVolume;
    }

    public void VolumeChangeFinished(float newVolume, EAudioType audioType)
    {
        OnVolumeChangeFinished?.Invoke(newVolume, audioType);
    }

    public float GetVolume(EAudioType audioType)
    {
        return audioType switch
        {
            EAudioType.None => 0.0f,
            EAudioType.Music => musicSource.volume,
            EAudioType.Sfx => sfxSource.volume,
            _ => 0.0f,
        };
    }
}
