using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    bool musicOn = true;
    static bool firstGame = true;

    [SerializeField]
    Animator startSceneAnimator;
    
    [SerializeField]
    Image speakerIcon;

    [SerializeField]
    Sprite speakerSprite;

    [SerializeField]
    Sprite noSpeakerSprite;

    AudioSource gameMusic;
    
    void Start()
    {

        gameMusic = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();
        // evtl. PlayerPrefs auslesen und musicOn bool setzten

        SetIcon();
        if (firstGame)
        {
            gameMusic.Play();
            Object.DontDestroyOnLoad(gameMusic.gameObject);
            firstGame = false;
        }

    }

    void SetIcon()
    {
        speakerIcon.sprite = musicOn ? speakerSprite : noSpeakerSprite;
    }

    void ChangeVolume()
    {
        gameMusic.volume = musicOn ? 1 : 0;
    }

    public void PressPlayerCountButton(int playerCount)
    {
        // Player Count als statische Variable setzten und in der GameScene auslesen
        SceneManager.LoadScene("GameScene");
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
}
