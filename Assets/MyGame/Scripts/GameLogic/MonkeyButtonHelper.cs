using UnityEngine;

public class MonkeyButtonHelper : MonoBehaviour
{
    public GameSceneManager.MonkeyColor monkycolor;
    public Sprite monkeySprite;
    public bool isFilled;
    public GameSceneManager gm;

    public void AddToMonkeyCount()
    {
        switch (monkycolor)
        {
            case GameSceneManager.MonkeyColor.red:
                gm.redCount++;
                break;
            case GameSceneManager.MonkeyColor.blue:
                gm.blueCount++;
                break;
            case GameSceneManager.MonkeyColor.green:
                gm.greenCount++;
                break;
            case GameSceneManager.MonkeyColor.yellow:
                gm.yellowCount++;
                break;
        }
    }
}
