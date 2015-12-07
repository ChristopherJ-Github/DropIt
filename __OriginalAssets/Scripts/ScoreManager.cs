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
            if (value > _score)
            {
                PlayIncreaseSound();
            }
            if (value >= gameManager.goalScore)
            {
                _score = gameManager.goalScore;
                gameManager.score = _score; //save for game over screen
                gameManager.EndLevelGoalReached(this);
            } 
            else
            {
                _score = value;
                gameManager.score = _score;
            }
        }
    }

    public AudioClip increaseSound;

    void PlayIncreaseSound ()
    {
        AudioManager.instance.Play(increaseSound, Vector3.zero, 1, 1, 1, false);
    }
}
