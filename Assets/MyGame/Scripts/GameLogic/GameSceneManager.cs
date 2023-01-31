using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    // By using an enum over a string, typing errors can be avoided
    public enum MonkeyColor
    {
        red,
        blue,
        green,
        yellow
    }

    MonkeyColor currentColor;

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

    // Since the counters are also used in the helper script, they are public
    public int redCount;
    public int blueCount;
    public int greenCount;
    public int yellowCount;

    bool monkeyFollowsCursor;

    int currentPlayer = 1;
    int prevPlayer;

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


    void Start()
    {
        // Since the UI looks different depending on the number of players, the respective game object is activated at the beginning
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
        // When the bag is pressed, the monkey should follow the cursor every frame
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameCanvas.transform as RectTransform, Input.mousePosition, gameCanvas.worldCamera, out pos);
        cursorFollowMonkey.transform.position = gameCanvas.transform.TransformPoint(pos);

        // The pause canvas should only be called by pressing the ESC key when the winner canvas is not active
        if (Input.GetKeyDown(KeyCode.Escape) && !gameOverCanvas.gameObject.active)
        {
            escCanvas.gameObject.SetActive(!escCanvas.gameObject.active);
        }
    }

    // Once a player has more than 5 monkeys on his board, his name is determined as the winner
    void CheckForWinner()
    {
        if (redCount > 5 || blueCount > 5 || greenCount > 5 || yellowCount > 5)
        {
            string winnerName = "";

            // Depending on the number of players, a color is assigned to a different player
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

    // This method checks which player's turn it is (this depends on the number of players, which is why a few edge cases need to be covered)
    void CheckTurn()
    {
        prevPlayer = currentPlayer;

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

    // Depending on the number of players, the respective UI element is activated via string interpolation
    void SetTurnHighlighting()
    {
        switch (StartSceneManager.currentPlayerCount)
        {
            case 2:
                twoPlayers.transform.FindChild($"Underline_{prevPlayer}").gameObject.SetActive(false);
                twoPlayers.transform.FindChild($"Underline_{currentPlayer}").gameObject.SetActive(true);
                break;
            case 3:
                threePlayers.transform.FindChild($"Underline_{prevPlayer}").gameObject.SetActive(false);
                threePlayers.transform.FindChild($"Underline_{currentPlayer}").gameObject.SetActive(true);
                break;
            case 4:
                fourPlayers.transform.FindChild($"Underline_{prevPlayer}").gameObject.SetActive(false);
                fourPlayers.transform.FindChild($"Underline_{currentPlayer}").gameObject.SetActive(true);
                break;
        }
    }

    // Since multiple parameters cannot be assigned in the Inspector, a helper script is used
    public void PressMonkey(MonkeyButtonHelper helper)
    {
        // If the random color matches the monkey color, the monkey is placed and it is checked for a winner or the next player
        if (currentColor == helper.monkycolor && !helper.isFilled && monkeyFollowsCursor)
        {
            cursorFollowImage.enabled = false;
            helper.gameObject.GetComponent<Image>().sprite = helper.monkeySprite;
            helper.isFilled = true;
            monkeyFollowsCursor = false;
            helper.AddToMonkeyCount();
            CheckForWinner();
            CheckTurn();
            SetTurnHighlighting();
        }

    }

    // If no monkey is following the cursor, a random color is chosen (depending on the number of players, yellow may or may not be present)
    public void PressBag()
    {
        if (!monkeyFollowsCursor)
        {
            // With three players, yellow may not be drawn
            currentColor = (MonkeyColor)Random.Range(0, StartSceneManager.currentPlayerCount == 3 ? 3 : 4);

            // The color from the enum is assigned to a sprite
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

    // This method is called from the UI
    public void PressQuit()
    {
        SceneManager.LoadScene("StartScene");
    }

    // This method is called from the UI
    public void PressCancel(GameObject escCanvas)
    {
        escCanvas.SetActive(false);
    }
}