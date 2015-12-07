using UnityEngine;
using System.Collections;

public class PlayerManager : DestructiveSingleton<PlayerManager>
{
    public GameObject currentCharacter; //should only be set in gameplay scenes
    private bool spawnCharacter;

    public void SetCharacter (Character character)
    {
        PlayerController playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        if (playerController != null)
        {
            Destroy(playerController.gameObject);
        }
        Transform spawnPoint = PlanetObject.instance.spawnPoint;
        currentCharacter = Instantiate(character.prefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        CameraFollow.instance.toFollow = currentCharacter.transform;
        UpdateCharacterSize();
    }

    [HideInInspector] public Vector3 size;

    void UpdateCharacterSize ()
    {
        Bounds bounds = currentCharacter.GetComponent<Collider>().bounds;
        size = bounds.extents;
    }

    public Dropable dropable;
}
