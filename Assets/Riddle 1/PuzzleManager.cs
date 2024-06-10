using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] PuzzlePieceHolder1;
    public GameObject[] PuzzlePieceHolder2;
    public GameObject[] PuzzlePieceHolder3;

    private DoorBarController specificDoorController; // Référence à la porte spécifique à ouvrir
    private int PurpleCounter;
    private int PinkCounter;
    private int YellowCounter;

    void Start()
    {
        // Trouver la porte avec le tag "PuzzleDoor"
        GameObject doorObject = GameObject.FindGameObjectWithTag("PuzzleDoor");
        if (doorObject != null)
        {
            specificDoorController = doorObject.GetComponent<DoorBarController>();
            Debug.Log("Puzzle door found: " + doorObject.name);
        }
        else
        {
            Debug.LogError("No PuzzleDoor found in the scene!");
        }
    }

    public void purpleATM()
    {
        PurpleCounter++;

        switch (PurpleCounter)
        {
            case 0:
                PuzzlePieceHolder1[0].SetActive(true);
                PuzzlePieceHolder1[1].SetActive(false);
                PuzzlePieceHolder1[2].SetActive(false);
                break;
            case 1:
                PuzzlePieceHolder1[0].SetActive(false);
                PuzzlePieceHolder1[1].SetActive(true);
                PuzzlePieceHolder1[2].SetActive(false);
                break;
            case 2:
                PuzzlePieceHolder1[0].SetActive(false);
                PuzzlePieceHolder1[1].SetActive(false);
                PuzzlePieceHolder1[2].SetActive(true);
                break;
            case 3:
                PuzzlePieceHolder1[0].SetActive(true);
                PuzzlePieceHolder1[1].SetActive(false);
                PuzzlePieceHolder1[2].SetActive(false);
                PurpleCounter = 0;
                break;
        }

        CheckPuzzleCompletion(); // Appeler la vérification après chaque modification
    }

    public void pinkATM()
    {
        PinkCounter++;

        switch (PinkCounter)
        {
            case 0:
                PuzzlePieceHolder2[0].SetActive(true);
                PuzzlePieceHolder2[1].SetActive(false);
                PuzzlePieceHolder2[2].SetActive(false);
                break;
            case 1:
                PuzzlePieceHolder2[0].SetActive(false);
                PuzzlePieceHolder2[1].SetActive(true);
                PuzzlePieceHolder2[2].SetActive(false);
                break;
            case 2:
                PuzzlePieceHolder2[0].SetActive(false);
                PuzzlePieceHolder2[1].SetActive(false);
                PuzzlePieceHolder2[2].SetActive(true);
                break;
            case 3:
                PuzzlePieceHolder2[0].SetActive(true);
                PuzzlePieceHolder2[1].SetActive(false);
                PuzzlePieceHolder2[2].SetActive(false);
                PinkCounter = 0;
                break;
        }

        CheckPuzzleCompletion(); // Appeler la vérification après chaque modification
    }

    public void yellowATM()
    {
        YellowCounter++;

        switch (YellowCounter)
        {
            case 0:
                PuzzlePieceHolder3[0].SetActive(true);
                PuzzlePieceHolder3[1].SetActive(false);
                PuzzlePieceHolder3[2].SetActive(false);
                break;
            case 1:
                PuzzlePieceHolder3[0].SetActive(false);
                PuzzlePieceHolder3[1].SetActive(true);
                PuzzlePieceHolder3[2].SetActive(false);
                break;
            case 2:
                PuzzlePieceHolder3[0].SetActive(false);
                PuzzlePieceHolder3[1].SetActive(false);
                PuzzlePieceHolder3[2].SetActive(true);
                break;
            case 3:
                PuzzlePieceHolder3[0].SetActive(true);
                PuzzlePieceHolder3[1].SetActive(false);
                PuzzlePieceHolder3[2].SetActive(false);
                YellowCounter = 0;
                break;
        }

        CheckPuzzleCompletion(); // Appeler la vérification après chaque modification
    }

    private void CheckPuzzleCompletion()
    {
        // Nouvelle combinaison pour réussir le puzzle
        bool isPurpleCorrect = PuzzlePieceHolder1[1].activeSelf;
        bool isPinkCorrect = PuzzlePieceHolder2[0].activeSelf;
        bool isYellowCorrect = PuzzlePieceHolder3[2].activeSelf;

        Debug.Log("CheckPuzzleCompletion called. Purple: " + isPurpleCorrect + ", Pink: " + isPinkCorrect + ", Yellow: " + isYellowCorrect);

        if (isPurpleCorrect && isPinkCorrect && isYellowCorrect)
        {
            if (specificDoorController != null)
            {
                Debug.Log("Puzzle completed. Opening door: " + specificDoorController.gameObject.name);
                specificDoorController.OpenDoor();
            }
            else
            {
                Debug.LogError("specificDoorController is null!");
            }
        }
    }
}
