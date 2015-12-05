using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    private enum State { Enabled, Disabled }
    private State state;
    
    void Start()
    {
        state = State.Disabled;
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

    public float deadPos, alivePos;
    private float currentAmount = 0;
    private float speed = 0.5f;
    public Text text;

    void Update()
    {
        if (state == State.Enabled)
        {
            currentAmount = Mathf.MoveTowards(currentAmount, healthManager.healthNormalized, speed * Time.deltaTime);
            Vector3 newPos = transform.localPosition;
            newPos.x = Mathf.Lerp(deadPos, alivePos, currentAmount);
            transform.localPosition = newPos;
            text.text = healthManager.health.ToString();
        }  
    } 
}
