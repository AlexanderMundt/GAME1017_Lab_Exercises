/* Completed By:    Alexander Mundt - 101632886
 * Assignment:      Lab Exercise 5
 * Class:           GAME-1017
 * Professor:       Ernie Burrows
 */
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderAudioController : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private EAudioType audioType;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.value = GameManager.Instance.SoundManager.GetVolume(audioType);
    }

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
        switch (audioType)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.Instance.SoundManager.VolumeChangeFinished(slider.value, audioType);
    }
}
