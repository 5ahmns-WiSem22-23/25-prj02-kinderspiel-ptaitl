using UnityEngine;

public class MonkeyButtonHelper : MonoBehaviour
{
    // Since all these variables are accessed by the GameManagerScript, they must be public
    public GameSceneManager.MonkeyColor monkycolor;
    public Sprite monkeySprite;
    public bool isFilled;
    public GameSceneManager gm;

    // Depending on the monkey color, another counter is counted up
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
