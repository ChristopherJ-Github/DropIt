using UnityEngine;
using System.Collections;

public enum State
{
    waitingToRandomize,
    waitingToApply,
    gameplay
}

public class GameManager : DestructiveSingleton<GameManager>
{
    public float timeLimit;
    private float _timer;
    private float timer
    {
        get { return _timer; }
        set
        {
            _timer = value;
            if (_timer >= timeLimit)
            {
                EndLevelTimeOut();
            }
        }
    }

    public void ResetTimer ()
    {
        timer = 0;
    }

    void Update ()
    {
        if (state == State.gameplay)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
    }

    [HideInInspector] public int goalScore;

    public void EndLevelGoalReached (ScoreManager winner)
    {
        Debug.Log(winner.gameObject + " wins");
        EndLevel();
    }

    void EndLevelTimeOut ()
    {
        Debug.Log("player with the most points wins");
        EndLevel();
    }

    [HideInInspector] public State state;

    void EndLevel ()
    {
        goalScore = 0;
        Destroy(PlayerManager.instance.currentCharacter);
        Application.LoadLevel("Menu");
        if (LevelRandomizer.instance != null)
        { //used when the game is properly started from the menu. Otherwise there won't be a LevelRandomizer.
            LevelRandomizer.instance.SwitchToWaitingToRandomize();
        }
    }
}
