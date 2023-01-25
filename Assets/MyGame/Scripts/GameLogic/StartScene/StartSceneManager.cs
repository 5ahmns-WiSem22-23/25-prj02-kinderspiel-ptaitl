using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    bool musicOn = true;

    [SerializeField]
    Animator startSceneAnimator;

    [SerializeField]
    Image icon;

    [SerializeField]
    Sprite speakerIcon;

    [SerializeField]
    Sprite noSpeakerIcon;

    [SerializeField]
    AudioSource clip;

    void Start()
    {
        // evtl. PlayerPrefs auslesen und musicOn bool setzten

        SetIcon();
        clip.Play();
        Object.DontDestroyOnLoad(clip.gameObject);
    }

    void SetIcon()
    {
        icon.sprite = musicOn ? speakerIcon : noSpeakerIcon;
    }

    void LoadStartScene(int playerCount)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    void ChangeVolume()
    {
        clip.volume = musicOn ? 1 : 0;
    }

    public void PressVolumeButton()
    {
        musicOn = !musicOn;
        SetIcon();
        ChangeVolume();
    }

    public void PressQuitButton()
    {
        Application.Quit();
    }

    public void PressStartButton()
    {
        startSceneAnimator.SetTrigger("ToPlayerCount");
    }

    public void PressBackButton()
    {
        startSceneAnimator.SetTrigger("ToStart");
    }

    public void PressOnePlayer()
    {
        LoadStartScene(1);
    }

    public void PressTwoPlayer()
    {
        LoadStartScene(2);
    }

    public void PressThreePlayer()
    {
        LoadStartScene(4);
    }

    public void PressFourPlayer()
    {
        LoadStartScene(4);
    }
}
