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
        StartCoroutine(SetScore());
        //animation
    }

    public Text objectiveText;
    public Text objectiveScore;
    public Text timeText;
    public Text timeScore;
    public Text healthText;
    public Text healthScore;
    public Text totalScore;
    public float animationSpeed;
    public AudioClip increaseSound;

    IEnumerator SetScore()
    {
        DisableAllText();

        objectiveText.enabled = true;
        float score = GameManager.instance.score;     
        objectiveText.text = "Objectives X " + ((int)score).ToString();
        objectiveScore.enabled = true;
        float _objectiveScore = score * 20;
        float _currentObjectiveScore = 0;
        do
        {
            _currentObjectiveScore = Mathf.MoveTowards(_currentObjectiveScore, _objectiveScore, animationSpeed * Time.deltaTime);
            objectiveScore.text = ((int)_currentObjectiveScore).ToString("D3");
            AudioManager.instance.Play(increaseSound, Vector3.zero, 1, 1, 1, false);
            yield return null;
        }
        while (_currentObjectiveScore != _objectiveScore);

        timeText.enabled = true;
        int totalSeconds = (int)GameManager.instance.timeLeft;
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        timeText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        timeScore.enabled = true;
        float _timeScore = totalSeconds;
        float _currentTimeScore = 0;
        do
        {
            _currentTimeScore = Mathf.MoveTowards(_currentTimeScore, _timeScore, animationSpeed * Time.deltaTime);
            timeScore.text = ((int)_currentTimeScore).ToString("D3");
            AudioManager.instance.Play(increaseSound, Vector3.zero, 1, 1, 1, false);
            yield return null;
        }
        while (_currentTimeScore != _timeScore);

        healthText.enabled = true;
        healthScore.enabled = true;
        float _healthScore = GameManager.instance.health;
        float _currentHealthScore = 0;
        do
        {
            _currentHealthScore = Mathf.MoveTowards(_currentHealthScore, _healthScore, animationSpeed * Time.deltaTime);
            healthScore.text = ((int)_currentHealthScore).ToString("D3");
            AudioManager.instance.Play(increaseSound, Vector3.zero, 1, 1, 1, false);
            yield return null;
        }
        while (_currentHealthScore != _healthScore);

        totalScore.enabled = true;
        float _totalScore = _objectiveScore + _timeScore + _healthScore;
        float _currentTotalScore = 0;
        do
        {
            _currentTotalScore = Mathf.MoveTowards(_currentTotalScore, _totalScore, animationSpeed * Time.deltaTime);
            totalScore.text = ((int)_currentTotalScore).ToString("D3");
            AudioManager.instance.Play(increaseSound, Vector3.zero, 1, 1, 1, false);
            yield return null;
        }
        while (_currentTotalScore != _totalScore);
        state = State.WaitingToReplay;
    }

    void DisableAllText ()
    {
        objectiveText.enabled = false;
        objectiveScore.enabled = false;
        timeText.enabled = false;
        timeScore.enabled = false;
        healthText.enabled = false;
        healthScore.enabled = false;
        totalScore.enabled = false;
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
        if (Input.GetKey(KeyCode.A) || Input.GetButton("Fire1"))
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
        if (Input.GetKeyUp(KeyCode.A) || Input.GetButtonUp("Fire1"))
        {
            GameManager.instance.SwitchState(GameState.RandomizationScreen);
        }
    }
}
