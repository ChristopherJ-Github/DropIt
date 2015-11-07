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

    private GameObject currentCharacter;
    public void SetCharacter (Character character)
    {
        if (currentCharacter!= null)
        {
            Destroy(currentCharacter);
        }
        GameObject charObject = Instantiate(character.prefab) as GameObject;
        currentCharacter = charObject;
        currentCharacter.transform.parent = transform;
        currentCharacter.transform.localPosition = Vector3.zero;
    }

    public Dropable dropable;
}
