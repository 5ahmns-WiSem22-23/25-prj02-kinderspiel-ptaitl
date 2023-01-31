using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    static bool musicOn = true;

    // By making the variable static it will not be reset when the scene is reloaded
    static bool firstGame = true;

    public static int currentPlayerCount;

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
        currentPlayerCount = playerCount;
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

    public void PressAnimationUIButton(string triggerName)
    {
        startSceneAnimator.SetTrigger(triggerName);
    }

}
