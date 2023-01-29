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
    Canvas canvas;

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

    public static int redCount;
    public static int blueCount;
    public static int greenCount;
    public static int yellowCount;

    bool monkeyFollowsCursor;

    MonkeyColor currentColor = MonkeyColor.red;

    [SerializeField]
    Image cursorFollowImage;

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        cursorFollowMonkey.transform.position = canvas.transform.TransformPoint(pos);
    }

    void checkForWinner()
    {
        if (redCount > 5 || blueCount > 5 || greenCount > 5 || yellowCount > 5)
        {
            redCount = blueCount = greenCount = yellowCount = 0;
            SceneManager.LoadScene("StartScene");
        }
    }
    
    public void PressMonkey(MonkeyButtonHelper helper)
    {
        if (currentColor == helper.monkycolor && !helper.isFilled && monkeyFollowsCursor)
        {
            cursorFollowImage.color = new Color(255, 255, 255, 0);
            helper.gameObject.GetComponent<Image>().sprite = helper.monkeySprite;
            helper.isFilled = true;
            monkeyFollowsCursor = false;
            helper.AddToMonkeyCount();
            checkForWinner();
        }

    }

    public void PressBag()
    {
        if (!monkeyFollowsCursor)
        {
            currentColor = (MonkeyColor)Random.Range(0, 4);

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

            cursorFollowImage.color = new Color(255, 255, 255, 1);
            monkeyFollowsCursor = true;
        }
    }
}