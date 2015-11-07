using UnityEngine;
using System.Collections;

public class GameManager : DestructiveSingleton<GameManager>
{
    [HideInInspector] public int goalScore;

    public void EndLevel (ScoreManager winner)
    {
        Destroy(PlayerManager.instance.currentCharacter);
        Application.LoadLevel("Menu");
        if (LevelRandomizer.instance != null)
        { //used when the game is properly started from the menu. Otherwise there won't be a LevelRandomizer.
            LevelRandomizer.instance.SwitchToWaitingToRandomize();
        }
    }
}
