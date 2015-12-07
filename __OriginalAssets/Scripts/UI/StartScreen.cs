using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreen : DestructiveSingleton<StartScreen>
{
    private enum State { Enabled, Disabled };
    private State state;
    private Canvas canvas;

    void Start ()
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

    public AudioClip startSound;

    void GetStartInput()
    {
        UpdateButtonImage();
        if (Input.GetKeyUp(KeyCode.A) || Input.GetButtonUp("Fire1"))
        {
            AudioManager.instance.Play(startSound, Vector3.zero, 1, 1, 1, false);
            GameManager.instance.SwitchState(GameState.RandomizationScreen);
        }
    }

    public Image button;
    public Sprite buttonState1;
    public Sprite buttonState2;

    void UpdateButtonImage ()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetButton("Fire1"))
        {
            button.sprite = buttonState2;
        }
        else
        {
            button.sprite = buttonState1;
        }
    }
}
