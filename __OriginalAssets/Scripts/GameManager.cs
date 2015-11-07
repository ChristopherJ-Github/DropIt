using UnityEngine;
using System.Collections;

public class GameManager : GameObjectSingleton<GameManager>
{
    [HideInInspector] public int goalScore;

    public void EndLevel (ScoreManager winner)
    {
        Destroy(PlayerManager.instance.currentCharacter);
        Application.LoadLevel("Menu");
        LevelRandomizer.instance.SwitchToWaitingToRandomize();
    }
}
