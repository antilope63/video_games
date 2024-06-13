using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] PuzzlePieceHolder1; // Red pieces
    public GameObject[] PuzzlePieceHolder2; // Purple pieces
    public GameObject[] PuzzlePieceHolder3; // Yellow pieces

    private DoorBarController specificDoorController;
    private int PurpleCounter;
    private int PinkCounter;
    private int YellowCounter;

    void Start()
    {
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

        InitializePuzzlePieces();
    }

    private void InitializePuzzlePieces()
    {
        if (PuzzlePieceHolder1.Length > 0) PuzzlePieceHolder1[0].SetActive(true);
        if (PuzzlePieceHolder1.Length > 1) PuzzlePieceHolder1[1].SetActive(false);
        if (PuzzlePieceHolder1.Length > 2) PuzzlePieceHolder1[2].SetActive(false);

        if (PuzzlePieceHolder2.Length > 0) PuzzlePieceHolder2[0].SetActive(true);
        if (PuzzlePieceHolder2.Length > 1) PuzzlePieceHolder2[1].SetActive(false);
        if (PuzzlePieceHolder2.Length > 2) PuzzlePieceHolder2[2].SetActive(false);

        if (PuzzlePieceHolder3.Length > 0) PuzzlePieceHolder3[0].SetActive(true);
        if (PuzzlePieceHolder3.Length > 1) PuzzlePieceHolder3[1].SetActive(false);
        if (PuzzlePieceHolder3.Length > 2) PuzzlePieceHolder3[2].SetActive(false);
    }

    public void purpleATM()
    {
        Debug.Log("Purple Hit");
        PurpleCounter++;
        if (PurpleCounter >= PuzzlePieceHolder1.Length) PurpleCounter = 0;
        ActivatePuzzlePiece(PuzzlePieceHolder1, PurpleCounter);
        CheckPuzzleCompletion();
    }

    public void pinkATM()
    {
        PinkCounter++;
        if (PinkCounter >= PuzzlePieceHolder2.Length) PinkCounter = 0;
        ActivatePuzzlePiece(PuzzlePieceHolder2, PinkCounter);
        CheckPuzzleCompletion();
    }

    public void yellowATM()
    {
        YellowCounter++;
        if (YellowCounter >= PuzzlePieceHolder3.Length) YellowCounter = 0;
        ActivatePuzzlePiece(PuzzlePieceHolder3, YellowCounter);
        CheckPuzzleCompletion();
    }

    private void ActivatePuzzlePiece(GameObject[] puzzlePieces, int activeIndex)
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] != null)
            {
                puzzlePieces[i].SetActive(i == activeIndex);
            }
        }
    }

    public void CheckPuzzleCompletion()
    {
        // Nouvelle combinaison pour réussir le puzzle
        bool isPurpleCorrect = PuzzlePieceHolder1.Length > 0 && PuzzlePieceHolder1[1].activeSelf;
        bool isPinkCorrect = PuzzlePieceHolder2.Length > 0 && PuzzlePieceHolder2[1].activeSelf;
        bool isYellowCorrect = PuzzlePieceHolder3.Length > 1 && PuzzlePieceHolder3[1].activeSelf;

        Debug.Log("Checking");
        if (isPurpleCorrect && isPinkCorrect && isYellowCorrect)
        {
            Debug.Log("Correct code entered: Red Cube, Purple Sphere, Yellow Cylinder.");
            if (specificDoorController != null)
            {
                specificDoorController.OpenDoor();
            }
        }
        else
        {
            Debug.Log("Incorrect code. Keep trying.");
        }
    }
}
