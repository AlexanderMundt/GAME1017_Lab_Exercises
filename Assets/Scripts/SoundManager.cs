/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 2
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, sfxSource;

    private void Start()
    {
        if (musicSource)
        {
            sfxSource.loop = true;
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
}
