/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 3
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderAudioController : MonoBehaviour
{
    [SerializeField] private EAudioType soundType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        ChangeAudioSliderVolume(slider.value);
    }

    //Delegates
    private void OnEnable()
    {
        slider.onValueChanged.AddListener(ChangeAudioSliderVolume);
    }

    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(ChangeAudioSliderVolume);
    }

    private void ChangeAudioSliderVolume(float newVolume)
    {
        switch (soundType)
        {
            case EAudioType.None:
                Debug.Log("Sound Type Not Set On: " + gameObject.name);
                break;

            case EAudioType.Music:
                GameManager.Instance.SoundManager.ChangeMusicVolume(newVolume);
                break;

            case EAudioType.Sfx:
                GameManager.Instance.SoundManager.ChangeSfxVolume(newVolume);
                break;

            default:
                break;
        }
    }
}
