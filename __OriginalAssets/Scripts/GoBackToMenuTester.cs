using UnityEngine;
using System.Collections;

public class GoBackToMenuTester : MonoBehaviour
{
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.M))
        {
            Application.LoadLevel("Menu");
            LevelRandomizer.instance.SwitchToMenuState();
        }
	}
}
