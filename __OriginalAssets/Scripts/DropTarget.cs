using UnityEngine;
using System.Collections;

public class DropTarget : MonoBehaviour
{
    private new Collider collider;
    private Renderer newRenderer;
    
	void Start ()
    {
        tag = "Drop Target";
        collider = GetComponent<Collider>();
        newRenderer = GetComponent<Renderer>();
        GameManager.instance.goalScore++;
        SpawnTarget();
	}

    public Transform targetPosition;
    private GameObject targetInstance;

    void SpawnTarget ()
    {
        targetInstance = Instantiate(GameManager.instance.targetEffect, targetPosition.position, targetPosition.rotation) as GameObject;
        targetInstance.transform.SetParent(transform);
    }

    public void TurnOff ()
    {
        //some animation for the target being hit
        ChangeColor();
        Destroy(targetInstance);
        SpawnSplashEffect();
        SpawnSuccessfullDropEffect();
        collider.enabled = false;
    }

    void SpawnSplashEffect ()
    {
        GameObject effect = Instantiate(GameManager.instance.splashEffect,
            targetPosition.position,
            targetPosition.rotation) as GameObject;
        effect.transform.SetParent(transform);
    }

    void SpawnSuccessfullDropEffect ()
    {
        GameObject effect = Instantiate(GameManager.instance.successfullDropEffect, 
            targetPosition.position,
            targetPosition.rotation) as GameObject;
        effect.transform.SetParent(transform);
    }

    public int materialIndex;
    public Color offColor;

    void ChangeColor ()
    {
        Material material = newRenderer.materials[materialIndex];
        material.color = offColor;
    }
}
