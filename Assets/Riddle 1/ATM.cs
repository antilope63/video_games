using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATM : MonoBehaviour
{
    public GameObject atmScreen;
    public PuzzleManager puzzleManager; 
    public string puzzleType; 
    private Behaviour halo;

    void Start()
    {
        
        halo = (Behaviour)GetComponent("Halo");
        if (halo != null)
        {
            halo.enabled = false; 
        }
    }

  
    public void Interact()
    {
        Debug.Log("I'm doing my best");
        if (puzzleManager != null)
        {
            switch (puzzleType)
            {
                case "purple":
                    puzzleManager.purpleATM();
                    break;
                case "pink":
                    puzzleManager.pinkATM();
                    break;
                case "yellow":
                    puzzleManager.yellowATM();
                    break;
                default:
                    Debug.LogWarning("Type de puzzle non reconnu: " + puzzleType);
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered space");
            LeanTween.scale(atmScreen, Vector3.one, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Left the space");
            LeanTween.scale(atmScreen, Vector3.zero, 1);
        }
    }

    public void SetHalo(bool shouldEnable)
    {
        if (halo != null)
        {
            halo.enabled = shouldEnable;
        }
    }
}
