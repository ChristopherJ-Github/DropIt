using UnityEngine;
using System.Collections;

public class LevelRandomizer : MonoBehaviour
{
    public static LevelRandomizer instance;
    public delegate void StateHandler();
    private StateHandler State;

	void Start ()
    {
        InitializeInstance();
        SwitchToWaitingToRandomize();
	}

    void InitializeInstance ()
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

    public void SwitchToWaitingToRandomize()
    {
        //show blank slots
        State = WaitingToRandomize;
    }

    void Update ()
    {
        State();
	}

    void WaitingToRandomize()
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
        State = WaitingToApply;
        //show ui button
    }

    void WaitingToApply ()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyLevel();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeLevel();
        }
    }

    void ApplyLevel ()
    {
        Player.instance.SetCharacter(character);
        Player.instance.dropable = dropable;
        Application.LoadLevel(planet.sceneName);
        SwitchToGameplay ();
        Debug.Log("level applied");
    }

    void SwitchToGameplay ()
    {
        State = GamePlay;
    }

    void GamePlay () { }
}
