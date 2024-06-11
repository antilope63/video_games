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
            Debug.Log("Halo component found and disabled initially.");
        }
        else
        {
            Debug.LogError("Halo component not found!");
        }

        if (atmScreen == null)
        {
            Debug.LogError("atmScreen is not assigned!");
        }
        else
        {
            Debug.Log("atmScreen assigned successfully.");
            Debug.Log("Initial scale of atmScreen: " + atmScreen.transform.localScale);
        }
    }

    public void Interact()
    {
        Debug.Log("ATM Interact called");
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
            Debug.Log("Player entered ATM space");
            if (halo != null)
            {
                SetHalo(true);
                Debug.Log("Halo enabled.");
            }

            if (atmScreen != null)
            {
                Debug.Log("Current scale of atmScreen before scaling: " + atmScreen.transform.localScale);
                LeanTween.scale(atmScreen, Vector3.one, 1).setOnComplete(() =>
                {
                    Debug.Log("atmScreen scaled to one");
                    Debug.Log("New scale of atmScreen: " + atmScreen.transform.localScale);
                });
            }
            else
            {
                Debug.LogError("atmScreen is not assigned!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player left ATM space");
            if (halo != null)
            {
                SetHalo(false);
                Debug.Log("Halo disabled.");
            }

            if (atmScreen != null)
            {
                Debug.Log("Current scale of atmScreen before scaling: " + atmScreen.transform.localScale);
                LeanTween.scale(atmScreen, Vector3.zero, 1).setOnComplete(() =>
                {
                    Debug.Log("atmScreen scaled to zero");
                    Debug.Log("New scale of atmScreen: " + atmScreen.transform.localScale);
                });
            }
            else
            {
                Debug.LogError("atmScreen is not assigned!");
            }
        }
    }

    public void SetHalo(bool shouldEnable)
    {
        if (halo != null)
        {
            halo.enabled = shouldEnable;
            Debug.Log("Halo set to: " + shouldEnable);
        }
        else
        {
            Debug.LogError("Halo component is missing!");
        }
    }
}
