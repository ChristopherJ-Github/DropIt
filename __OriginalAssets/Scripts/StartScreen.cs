using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreen : DestructiveSingleton<StartScreen>
{
    private enum State { Enabled, Disabled };
    private State state;
    private Canvas canvas;
    private bool started;

    void Start ()
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

    void UpdateState (GameState lastState, GameState newState)
    {
        if (newState == GameState.StartScreen)
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

    void Update()
    {
        if (state == State.Enabled)
        {
            GetStartInput();
        } 
    }

    void GetStartInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ChangeButtonImage(true);
        } 
        else
        {
            ChangeButtonImage(false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameManager.instance.SwitchState(GameState.RandomizationScreen);
        }
    }

    public Image button;
    public Sprite buttonState1;
    public Sprite buttonState2;

    void ChangeButtonImage(bool pressed)
    {
        if (pressed)
        {
            button.sprite = buttonState2;
        }
        else
        {
            button.sprite = buttonState1;
        }
    }
}
