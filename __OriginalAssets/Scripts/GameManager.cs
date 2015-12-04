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
    IEnumerator Start ()
    {
        base.Start();
        yield return null; //wait for all screen scripts to subscribe to OnStateChange and then call it
        SwitchState(GameState.StartScreen);
        OnStateChange += CheckForTimerReset;
    }
    private GameState state;

    public void SwitchState(GameState newState)
    {
        NotifyStateChange(state, newState);
    }

    public delegate void StateHandler(GameState lastState, GameState newState);
    public event StateHandler OnStateChange;

    void NotifyStateChange(GameState lastState, GameState newState)
    {
        state = newState;
        if (OnStateChange != null)
        {
            OnStateChange(lastState, newState);
        }
    }

    void CheckForTimerReset(GameState lastState, GameState newState)
    {
        if (newState == GameState.Gameplay)
        {
            timer = 0;
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
        Application.LoadLevel("Menu");
        Destroy(PlayerManager.instance.currentCharacter);
        if (LevelRandomizer.instance != null)
        { //used when the game is properly started from the menu. Otherwise there won't be a LevelRandomizer.
            SwitchState(GameState.RandomizationScreen);
        }
    }
}
