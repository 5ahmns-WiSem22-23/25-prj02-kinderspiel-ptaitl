using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameSceneManager;

public class GameSceneManager : MonoBehaviour
{
    public enum MonkeyColor
    {
        red,
        blue,
        green,
        yellow
    }

    [SerializeField]
    Canvas gameCanvas;

    [SerializeField]
    Canvas escCanvas;

    [SerializeField]
    Canvas gameOverCanvas;

    [SerializeField]
    GameObject cursorFollowMonkey;

    [SerializeField]
    Sprite redMonkeySprite;

    [SerializeField]
    Sprite blueMonkeySprite;

    [SerializeField]
    Sprite greenMonkeySprite;

    [SerializeField]
    Sprite yellowMonkeySprite;

    public int redCount;
    public int blueCount;
    public int greenCount;
    public int yellowCount;

    bool monkeyFollowsCursor;

    int currentPlayer = 1;

    [SerializeField]
    Image cursorFollowImage;

    [SerializeField]
    GameObject twoPlayers;

    [SerializeField]
    GameObject threePlayers;

    [SerializeField]
    GameObject fourPlayers;

    [SerializeField]
    GameObject yellowMonkeyCross;

    MonkeyColor currentColor;

    public Text anzeige;


    void Start()
    {
        switch (StartSceneManager.currentPlayerCount)
        {
            case 2:
                twoPlayers.SetActive(true);
                break;
            case 3:
                threePlayers.SetActive(true);
                yellowMonkeyCross.SetActive(true);
                break;
            case 4:
                fourPlayers.SetActive(true);
                break;
        }
    }


    void Update()
    {
        anzeige.text = currentPlayer.ToString();

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameCanvas.transform as RectTransform, Input.mousePosition, gameCanvas.worldCamera, out pos);
        cursorFollowMonkey.transform.position = gameCanvas.transform.TransformPoint(pos);

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverCanvas.gameObject.active)
        {
            escCanvas.gameObject.SetActive(!escCanvas.gameObject.active);
        }
    }

    void CheckForWinner()
    {
        if (redCount > 5 || blueCount > 5 || greenCount > 5 || yellowCount > 5)
        {
            string winnerName = "";

            switch (currentColor)
            {
                case MonkeyColor.red:
                    winnerName = "Spieler Eins";
                    break;
                case MonkeyColor.blue:
                    winnerName = StartSceneManager.currentPlayerCount == 2 ? "Spieler Eins" : "Spieler Zwei";
                    break;
                case MonkeyColor.green:
                    winnerName = StartSceneManager.currentPlayerCount == 2 ? "Spieler Zwei" : "Spieler Drei";
                    break;
                case MonkeyColor.yellow:
                    winnerName = StartSceneManager.currentPlayerCount == 2 ? "Spieler Zwei" : "Spieler Vier";
                    break;
            }

            gameOverCanvas.gameObject.transform.FindChild("Winner").GetComponent<Text>().text = $"{winnerName} gewinnt";
            gameOverCanvas.gameObject.SetActive(true);
        }
    }

    void CheckTurn()
    {
        if (currentColor == MonkeyColor.red)
        {
            currentPlayer = (currentPlayer == 1) ? 2 : currentPlayer;
        }
        else if (currentColor == MonkeyColor.blue)
        {
            if (currentPlayer == 1 && StartSceneManager.currentPlayerCount == 2)
            {
                currentPlayer = 2;
            }
            else if (currentPlayer == 2 && StartSceneManager.currentPlayerCount != 2)
            {
                currentPlayer = 3;
            }
        }
        else if (currentColor == MonkeyColor.green)
        {
            if (currentPlayer == 2 && StartSceneManager.currentPlayerCount == 2)
            {
                currentPlayer = 1;
            }
            else if (currentPlayer == 3 && StartSceneManager.currentPlayerCount == 4)
            {
                currentPlayer = 4;
            }
            else if (currentPlayer == 3 && StartSceneManager.currentPlayerCount == 3)
            {
                currentPlayer = 1;
            }
        }
        else if (currentColor == MonkeyColor.yellow)
        {
            if (currentPlayer == 2 && StartSceneManager.currentPlayerCount == 2)
            {
                currentPlayer = 1;
            }
            else if (currentPlayer == 4 && StartSceneManager.currentPlayerCount != 2)
            {
                currentPlayer = 1;
            }
        }
    }


    public void PressMonkey(MonkeyButtonHelper helper)
    {
        if (currentColor == helper.monkycolor && !helper.isFilled && monkeyFollowsCursor)
        {
            cursorFollowImage.enabled = false;
            helper.gameObject.GetComponent<Image>().sprite = helper.monkeySprite;
            helper.isFilled = true;
            monkeyFollowsCursor = false;
            helper.AddToMonkeyCount();
            CheckForWinner();
            CheckTurn();
        }

    }

    public void PressBag()
    {
        if (!monkeyFollowsCursor)
        {
            currentColor = (MonkeyColor)Random.Range(0, StartSceneManager.currentPlayerCount == 3 ? 3 : 4);

            switch (currentColor)
            {
                case MonkeyColor.red:
                    cursorFollowImage.sprite = redMonkeySprite;
                    break;
                case MonkeyColor.blue:
                    cursorFollowImage.sprite = blueMonkeySprite;
                    break;
                case MonkeyColor.green:
                    cursorFollowImage.sprite = greenMonkeySprite;
                    break;
                case MonkeyColor.yellow:
                    cursorFollowImage.sprite = yellowMonkeySprite;
                    break;
            }

            cursorFollowImage.enabled = true;
            monkeyFollowsCursor = true;
        }
    }

    public void PressQuit()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void PressCancel(GameObject escCanvas)
    {
        escCanvas.SetActive(false);
    }
}