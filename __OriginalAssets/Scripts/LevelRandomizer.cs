using UnityEngine;
using System.Collections;

public class LevelRandomizer : DestructiveSingleton<LevelRandomizer>
{
    public void SwitchToWaitingToRandomize()
    {
        //show blank slots
        GameManager.instance.SwitchState(GameState.WaitingToRandomize);
        Debug.Log("press R to randomize");
    }

    void Update ()
    {
        if (GameManager.instance.state == GameState.WaitingToRandomize)
        {
            GetRandomizationInput();
        }
        else if (GameManager.instance.state == GameState.WaitingToApply)
        {
            GetRandomizationInput();
            GetConfirmationInput();
        }
	}

    void GetRandomizationInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeLevel();
            SwitchToWaitingToApply();
        }
    }

    public Character[] characters;
    public Dropable[] dropables;
    public Planet[] planets;
    private Character character;
    private Dropable dropable;
    private Planet planet;

    void RandomizeLevel()
    {
        int characterIndex = Random.Range(0, characters.Length);
        character = characters[characterIndex];
        int dropableIndex = Random.Range(0, dropables.Length);
        if (dropables[dropableIndex].name == character.name)
        { //make sure the droppable isn't the same as the character
            dropableIndex = (dropableIndex + 1) % dropables.Length;
        }
        dropable = dropables[dropableIndex];
        int planetIndex = Random.Range(0, planets.Length);
        planet = planets[planetIndex];
        UpdateUISlots();
        Debug.Log("level randomized: " + character.name + ", " + dropable.name + ", " + planet.name);
    }

    void UpdateUISlots()
    {

    }

    void SwitchToWaitingToApply ()
    {
        GameManager.instance.state = GameState.WaitingToApply;
        Debug.Log("press A to apply or R to randomize again");
        //show ui button
    }

    void GetConfirmationInput ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(ApplyLevel());
        }
    }

    IEnumerator ApplyLevel ()
    {
        AsyncOperation async = Application.LoadLevelAsync(planet.sceneName);
        yield return async;
        PlayerManager.instance.SetCharacter(character);
        PlayerManager.instance.dropable = dropable;
        SwitchToGameplay ();
        Debug.Log("level applied");
    }

    void SwitchToGameplay ()
    {
        GameManager.instance.ResetTimer();
        GameManager.instance.state = GameState.Gameplay;
    }
}
