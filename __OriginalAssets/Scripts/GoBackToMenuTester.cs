using UnityEngine;
using System.Collections;

public class GoBackToMenuTester : MonoBehaviour
{
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.M))
        {
            Destroy(PlayerManager.instance.currentCharacter);
            Application.LoadLevel("Menu");
            LevelRandomizer.instance.SwitchToWaitingToRandomize();
        }
	}
}
