using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    void Start()
    {
        InitializeInstance();
    }

    public static PlayerManager instance;

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

    [HideInInspector] public GameObject currentCharacter;
    private bool spawnCharacter;
    public void SetCharacter (Character character)
    {
        currentCharacter = Instantiate(character.prefab) as GameObject;
    }

    public Dropable dropable;
}
