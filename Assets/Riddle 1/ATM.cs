using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATM : MonoBehaviour
{
    public GameObject[] puzzlePieces;  // Les pièces de puzzle (cube, sphère, cylindre)
    public string puzzleType;  // Le type de puzzle (red, purple, yellow)
    private int pieceCounter = 0;
    public GameObject puzManager;

    void Start()
    {
        if (puzzlePieces == null || puzzlePieces.Length == 0)
        {
            //Debug.LogError("Puzzle pieces not assigned for " + gameObject.name);
        }
        else
        {
            // Initialiser les pièces en activant seulement la première
            ActivatePuzzlePiece(0);
        }
    }

    public void Interact()
    {
        if (puzzlePieces != null && puzzlePieces.Length > 0)
        {
            pieceCounter = (pieceCounter + 1) % puzzlePieces.Length;
            ActivatePuzzlePiece(pieceCounter);
            //Debug.Log("Changed piece to " + puzzlePieces[pieceCounter].name + " on " + gameObject.name);

            // Forcer le rafraîchissement de l'affichage
            StartCoroutine(ForceRefresh());
        }
        else
        {
            Debug.LogError("No puzzle pieces assigned!");
        }
    }

    private void ActivatePuzzlePiece(int activeIndex)
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            bool shouldBeActive = (i == activeIndex);
            puzzlePieces[i].SetActive(shouldBeActive);
            Debug.Log(puzzlePieces[i].name + " active state set to " + shouldBeActive);
        }
        puzManager.GetComponent<PuzzleManager>().CheckPuzzleCompletion();
    }

    private IEnumerator ForceRefresh()
    {
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
    }
}
