using UnityEngine;
using System.Collections;

public class LevelRandomizer : DestructiveSingleton<LevelRandomizer>
{

	void Start ()
    {
        base.Start();
        SwitchToWaitingToRandomize();
	}

    public void SwitchToWaitingToRandomize()
    {
        //show blank slots
        GameManager.instance.state = State.waitingToRandomize;
    }

    void Update ()
    {
        if (GameManager.instance.state == State.waitingToRandomize)
        {
            GetRandomizationInput();
        }
        else if (GameManager.instance.state == State.waitingToApply)
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
        GameManager.instance.state = State.waitingToApply;
        //show ui button
    }

    void GetConfirmationInput ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyLevel();
        }
    }

    void ApplyLevel ()
    {
        Application.LoadLevel(planet.sceneName);
        PlayerManager.instance.SetCharacter(character);
        PlayerManager.instance.dropable = dropable;
        SwitchToGameplay ();
        Debug.Log("level applied");
    }

    void SwitchToGameplay ()
    {
        GameManager.instance.ResetTimer();
        GameManager.instance.state = State.gameplay;
    }
}
