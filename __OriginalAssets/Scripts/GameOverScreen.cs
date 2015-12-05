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
        SetScore();
        //animation
        state = State.WaitingToReplay;
    }

    public Text objectiveText;
    public Text objectiveScore;
    public Text timeText;
    public Text timeScore;
    public Text healthText;
    public Text healthScore;
    public Text totalScore;

    void SetScore ()
    {
        int score = (int)GameManager.instance.score;
        objectiveText.text = "Objectives X " + score.ToString();
        int _objectiveScore = score * 20;
        objectiveScore.text = _objectiveScore.ToString("D3");
        int totalSeconds = (int)GameManager.instance.timeLeft;
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        timeText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        int _timeScore = totalSeconds;
        timeScore.text = totalSeconds.ToString("D3");
        int _healthScore = (int)GameManager.instance.health;
        healthScore.text = _healthScore.ToString("D3");
        int _totalScore = _objectiveScore + _timeScore + _healthScore;
        totalScore.text = _totalScore.ToString("D3");
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
