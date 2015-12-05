using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropCounter : MonoBehaviour
{
    private enum State { Enabled, Disabled }
    private State state;
    private Text text;
    
    void Start()
    {
        text = GetComponent<Text>();
        state = State.Disabled;
        GameManager.instance.OnStateChange += UpdateState;
    }

    private ScoreManager scoreManager;

    void UpdateState (GameState lastState, GameState newState)
    {
        if (newState == GameState.Gameplay)
        {
            scoreManager = PlayerManager.instance.currentCharacter.GetComponent<ScoreManager>();
            state = State.Enabled;
        }
        else
        {
            state = State.Disabled;
        }
    }

    void Update()
    {
        if (state == State.Enabled)
        {
            text.text = "X " + scoreManager.score.ToString();
        }
    }
}
