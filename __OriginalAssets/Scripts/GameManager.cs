using UnityEngine;
using System.Collections;

public enum GameState
{
    StartScreen,
    RandomizationScreen,
    Gameplay
}

public class GameManager : DestructiveSingleton<GameManager>
{
    void Start ()
    {
        base.Start();
        SwitchState(GameState.StartScreen);
    }

    private GameState state;

    public void SwitchState(GameState newState)
    {
        NotifyStateChange(state, newState);
    }

    public delegate void StateHandler(GameState lastState, GameState newState);
    public event StateHandler OnStateChange;

    public void NotifyStateChange(GameState lastState, GameState newState)
    {
        state = newState;
        if (OnStateChange != null)
        {
            OnStateChange(lastState, newState);
        }
    }

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
        if (state == GameState.Gameplay)
        {
            timer += Time.deltaTime;
            //Debug.Log(timer);
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

    public void EndLevel ()
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
