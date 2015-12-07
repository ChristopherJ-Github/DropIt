using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private enum State { Enabled, Disabled }
    private State state;
    private Image image;
    
    void Start()
    {
        state = State.Disabled;
        image = GetComponent<Image>();
        GameManager.instance.OnStateChange += UpdateState;
    }

    private HealthManager healthManager;

    void UpdateState(GameState lastState, GameState newState)
    {
        if (newState == GameState.Gameplay)
        {
            healthManager = PlayerManager.instance.currentCharacter.GetComponent<HealthManager>();
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
            UpdateHealthBar();
            UpdateTimer();
        }  
    }

    public float deadPos, alivePos;
    private float currentAmount = 0;
    private float speed = 0.5f;
    public Gradient healthColor;

    void UpdateHealthBar()
    {
        currentAmount = Mathf.MoveTowards(currentAmount, healthManager.healthNormalized, speed * Time.deltaTime);
        Vector3 newPos = transform.localPosition;
        newPos.x = Mathf.Lerp(deadPos, alivePos, currentAmount);
        transform.localPosition = newPos;
        Color color = healthColor.Evaluate(healthManager.healthNormalized);
        image.color = color;
    }

    public Text text;

    void UpdateTimer()
    {
        int totalSeconds = (int)GameManager.instance.timeLeft;
        int seconds = totalSeconds % 60;
        int minutes = totalSeconds / 60;
        text.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
