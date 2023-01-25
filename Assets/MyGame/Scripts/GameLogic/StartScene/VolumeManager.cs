using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    bool musicOn;

    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite speakerIcon;

    [SerializeField]
    Sprite noSpeakerIcon;

    void Start()
    {
        // Player Prefs auslesen und bool setzten

        SetIcon();
    }

    public void ChangeVolume()
    {
        musicOn = !musicOn;
        SetIcon();
    }

    void SetIcon()
    {
        icon.sprite = musicOn ? speakerIcon : noSpeakerIcon;
    }
}
