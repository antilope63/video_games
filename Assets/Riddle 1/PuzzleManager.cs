using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject[] PuzzlePieceHolder1;
    public GameObject[] PuzzlePieceHolder2;
    public GameObject[] PuzzlePieceHolder3;

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
        PurpleCounter++;
        Debug.Log("purpleATM called. PurpleCounter: " + PurpleCounter);

        switch (PurpleCounter)
        {
            case 0:
                Debug.Log("Before activating purple piece 0");
                ActivatePuzzlePiece(PuzzlePieceHolder1, 0);
                Debug.Log("After activating purple piece 0");
                break;
            case 1:
                Debug.Log("Before activating purple piece 1");
                ActivatePuzzlePiece(PuzzlePieceHolder1, 1);
                Debug.Log("After activating purple piece 1");
                break;
            case 2:
                Debug.Log("Before activating purple piece 2");
                ActivatePuzzlePiece(PuzzlePieceHolder1, 2);
                Debug.Log("After activating purple piece 2");
                break;
            case 3:
                Debug.Log("Before resetting purple pieces");
                ActivatePuzzlePiece(PuzzlePieceHolder1, 0);
                PurpleCounter = 0;
                Debug.Log("After resetting purple pieces");
                break;
        }

        CheckPuzzleCompletion(); // Appeler la vérification après chaque modification
    }

    public void pinkATM()
    {
        PinkCounter++;
        Debug.Log("pinkATM called. PinkCounter: " + PinkCounter);

        switch (PinkCounter)
        {
            case 0:
                Debug.Log("Before activating pink piece 0");
                ActivatePuzzlePiece(PuzzlePieceHolder2, 0);
                Debug.Log("After activating pink piece 0");
                break;
            case 1:
                Debug.Log("Before activating pink piece 1");
                ActivatePuzzlePiece(PuzzlePieceHolder2, 1);
                Debug.Log("After activating pink piece 1");
                break;
            case 2:
                Debug.Log("Before activating pink piece 2");
                ActivatePuzzlePiece(PuzzlePieceHolder2, 2);
                Debug.Log("After activating pink piece 2");
                break;
            case 3:
                Debug.Log("Before resetting pink pieces");
                ActivatePuzzlePiece(PuzzlePieceHolder2, 0);
                PinkCounter = 0;
                Debug.Log("After resetting pink pieces");
                break;
        }

        CheckPuzzleCompletion(); // Appeler la vérification après chaque modification
    }

    public void yellowATM()
    {
        YellowCounter++;
        Debug.Log("yellowATM called. YellowCounter: " + YellowCounter);

        switch (YellowCounter)
        {
            case 0:
                Debug.Log("Before activating yellow piece 0");
                ActivatePuzzlePiece(PuzzlePieceHolder3, 0);
                Debug.Log("After activating yellow piece 0");
                break;
            case 1:
                Debug.Log("Before activating yellow piece 1");
                ActivatePuzzlePiece(PuzzlePieceHolder3, 1);
                Debug.Log("After activating yellow piece 1");
                break;
            case 2:
                Debug.Log("Before activating yellow piece 2");
                ActivatePuzzlePiece(PuzzlePieceHolder3, 2);
                Debug.Log("After activating yellow piece 2");
                break;
            case 3:
                Debug.Log("Before resetting yellow pieces");
                ActivatePuzzlePiece(PuzzlePieceHolder3, 0);
                YellowCounter = 0;
                Debug.Log("After resetting yellow pieces");
                break;
        }

        CheckPuzzleCompletion();
    }

    private void ActivatePuzzlePiece(GameObject[] puzzlePieces, int activeIndex)
    {
        Debug.Log("Activating puzzle piece. Active Index: " + activeIndex);
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            if (puzzlePieces[i] != null)
            {
                bool shouldBeActive = (i == activeIndex);
                puzzlePieces[i].SetActive(shouldBeActive);
                Debug.Log($"{puzzlePieces[i].name} active state set to {shouldBeActive}");
            }
            else
            {
                Debug.LogError($"Puzzle piece at index {i} is null!");
            }
        }
    }

    private void CheckPuzzleCompletion()
    {
        // Nouvelle combinaison pour réussir le puzzle
        bool isPurpleCorrect = PuzzlePieceHolder1.Length > 1 && PuzzlePieceHolder1[1].activeSelf;
        bool isPinkCorrect = PuzzlePieceHolder2.Length > 0 && PuzzlePieceHolder2[0].activeSelf;
        bool isYellowCorrect = PuzzlePieceHolder3.Length > 2 && PuzzlePieceHolder3[2].activeSelf;

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
