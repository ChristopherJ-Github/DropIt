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
                _score = gameManager.goalScore;
                gameManager.EndLevelGoalReached(this);
            } 
            else
            {
                _score = value;
            }
        }
    }
}
