using UnityEngine;
using System.Collections;

public class LevelRandomizer : MonoBehaviour
{
    public static LevelRandomizer instance;
    public enum State {menu, gameplay};
    private State state; 

	void Start ()
    {
        InitializeInstance();
        state = State.menu;
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

	void Update ()
    {
        if (state == State.menu)
        {
            GetInput();
        }
	}

    void GetInput ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RandomizeLevel();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ApplyLevel();
        }
    }

    public Character[] characters;
    public Dropable[] dropables;
    public Planet[] planets;
    private Character character;
    private Dropable dropable;
    private Planet planet;

    void RandomizeLevel ()
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

    void UpdateUISlots ()
    {

    }

    void ApplyLevel ()
    {
        Player.instance.SetCharacter(character);
        Player.instance.dropable = dropable;
        Application.LoadLevel(planet.sceneName);
        state = State.gameplay;
        Debug.Log("level applied");
    }

    public void SwitchToMenuState ()
    {
        state = State.menu;
    }
}
