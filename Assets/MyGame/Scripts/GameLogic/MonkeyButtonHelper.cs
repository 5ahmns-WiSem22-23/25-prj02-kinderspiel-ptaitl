using UnityEngine;

public class MonkeyButtonHelper : MonoBehaviour
{
    public GameSceneManager.MonkeyColor monkycolor;
    public Sprite monkeySprite;
    public bool isFilled;

    public void AddToMonkeyCount()
    {
        switch (monkycolor)
        {
            case GameSceneManager.MonkeyColor.red:
                GameSceneManager.redCount++;
                break;
            case GameSceneManager.MonkeyColor.blue:
                GameSceneManager.blueCount++;
                break;
            case GameSceneManager.MonkeyColor.green:
                GameSceneManager.greenCount++;
                break;
            case GameSceneManager.MonkeyColor.yellow:
                GameSceneManager.yellowCount++;
                break;
        }
    }
}
