using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelRandomizer : DestructiveSingleton<LevelRandomizer>
{
    private enum State { Disabled, WaitingToRandomize, WaitingToApply };
    private State state;
    private Canvas canvas;

    void Start()
    {
        base.Start();
        if (instance == this)
        {
            GameManager.instance.OnStateChange += UpdateState;
            canvas = GetComponent<Canvas>();
        }     
    }

    void HideScreen()
    {
        canvas.enabled = false;
        state = State.Disabled;
    }

    void UpdateState(GameState lastState, GameState newState)
    {
        if (newState == GameState.RandomizationScreen)
        {
            StartCoroutine(SwitchToWaitingToRandomize());
        }
        else
        {
            HideScreen();
        }
    }

    public GameObject randomizedSentence;

    IEnumerator SwitchToWaitingToRandomize()
    {
        canvas.enabled = true;
        randomizedSentence.SetActive(false);
        yield return null; //wait until a frame has passed since the last button press
        state = State.WaitingToRandomize;
    }

    void Update ()
    {
        UpdateButtonImage();
        if (state == State.WaitingToRandomize)
        {
            GetRandomizationInput();
        }
        else if (state == State.WaitingToApply)
        {
            GetConfirmationInput();
        }
	}

    public Image button;
    public Sprite buttonState1;
    public Sprite buttonState2;

    void UpdateButtonImage()
    {
        if (Input.GetKey(KeyCode.A))
        {
            button.sprite = buttonState2;
        }
        else
        {
            button.sprite = buttonState1;
        }
    }

    void GetRandomizationInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
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
        UpdateUISlots(character.name, dropable.name, planet.name);
    }

    public Text characterText;
    public Text dropableText;
    public Text planetText;

    void UpdateUISlots(string characterName, string dropableName, string planetName)
    {
        characterText.text = characterName;
        dropableText.text = dropableName;
        planetText.text = planetName;
    }

    void SwitchToWaitingToApply ()
    {
        state = State.WaitingToApply;
        randomizedSentence.SetActive(true);
    }

    void GetConfirmationInput ()
    {
        if (Input.GetKeyUp(KeyCode.A))
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
        GameManager.instance.SwitchState(GameState.Gameplay);
    }
}
