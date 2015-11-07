using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;

    void Start()
    {
        InitializeInstance();
    }

    void InitializeInstance()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void SetCharacter (Character character)
    {

    }

    [HideInInspector] public Dropable dropable;
}
