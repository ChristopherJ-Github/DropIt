using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public float deadPos, alivePos;
    private HealthManager healthManager;
    private float currentAmount = 0;
    private float speed = 0.5f;
    /*
    void Start()
    {
        healthManager = PlayerManager.instance.currentCharacter.GetComponent<HealthManager>();
    }

    void Update()
    {
        currentAmount = Mathf.MoveTowards(currentAmount, healthManager.healthNormalized, speed * Time.deltaTime);
        Vector3 newPos = transform.localPosition;
        newPos.x = Mathf.Lerp(deadPos, alivePos, currentAmount);
        transform.localPosition = newPos;
    }
    */
}
