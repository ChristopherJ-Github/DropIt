using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            GameManager gameManager = GameManager.instance;
            if (value >= gameManager.goalScore)
            {
                score = gameManager.goalScore;
                gameManager.EndLevel(this);
            } 
            else
            {
                score = value;
            }
        }
    }
}
