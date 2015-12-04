using UnityEngine;
using System.Collections;

public class GameplayScreen : DestructiveSingleton<GameplayScreen>
{

    private enum State { Enabled, Disabled };
    private State state;
    private Canvas canvas;
    private bool started;

    void Start()
    {
        base.Start();
        if (instance == this)
        {
            started = true;
            GameManager.instance.OnStateChange += UpdateState;
            canvas = GetComponent<Canvas>();
        }
    }

    void HideScreen()
    {
        canvas.enabled = false;
        state = State.Disabled;
    }

    void UpdateState(GameState lastState, GameState newState)
    {
        if (newState == GameState.Gameplay)
        {
            ShowScreen();
        }
        else
        {
            HideScreen();
        }
    }

    void ShowScreen()
    {
        canvas.enabled = true;
        state = State.Enabled;
    }
}
