using UnityEngine;
using System.Collections;

public class PlayerManager : GameObjectSingleton<PlayerManager>
{

    [HideInInspector] public GameObject currentCharacter;
    private bool spawnCharacter;
    public void SetCharacter (Character character)
    {
        currentCharacter = Instantiate(character.prefab) as GameObject;
    }

    public Dropable dropable;
}
