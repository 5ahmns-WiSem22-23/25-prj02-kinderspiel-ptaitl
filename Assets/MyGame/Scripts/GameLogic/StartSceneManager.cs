using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    // By making the variables static they will not be reseted when the scene is reloaded
    static bool musicOn = true;
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
        // Since the gameMusic object is not destroyed when the scene changes, it cannot be included as a reference in the inspector
        gameMusic = GameObject.FindGameObjectWithTag("MusicPlayer").GetComponent<AudioSource>();

        SetIcon();

        // Music should not be restarted when coming back from the GameScene
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

    // The number of players is given as a parameter in the inspector
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

    // The name of the animation trigger is given as a parameter in the inspector
    public void PressAnimationUIButton(string triggerName)
    {
        startSceneAnimator.SetTrigger(triggerName);
    }

}
