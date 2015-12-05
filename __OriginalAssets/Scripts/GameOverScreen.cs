using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : DestructiveSingleton<GameOverScreen>
{
    private enum State { Disabled, ShowingResults, WaitingToReplay };
    private State state;
    private Canvas canvas;

    void Start()
    {
        base.Start();
        if (instance == this)
        {
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
        if (newState == GameState.GameOver)
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
        state = State.ShowingResults;
        //animation
        state = State.WaitingToReplay;
    }

    void Update ()
    {
        UpdateButtonImage();
        if (state == State.WaitingToReplay)
        {
            GetReplayInput();
        }
	}

    public Image button;
    public Sprite buttonState1;
    public Sprite buttonState2;

    void UpdateButtonImage()
    {
        if (Input.GetKey(KeyCode.A))
        {
            button.sprite = buttonState2;
        }
        else
        {
            button.sprite = buttonState1;
        }
    }

    void GetReplayInput ()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameManager.instance.SwitchState(GameState.RandomizationScreen);
        }
    }
}
