using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATM : MonoBehaviour
{
    public GameObject[] puzzlePieces;  // Les pièces de puzzle (cube, sphère, cylindre)
    public string puzzleType;  // Le type de puzzle (red, purple, yellow)
    private int pieceCounter = 0;
    public GameObject puzManager;
    public GameObject atmScreen;  // L'écran de l'ATM
    public float detectionRange = 3.0f;  // Distance de détection

    private Transform player;

    void Start()
    {
        if (puzzlePieces == null || puzzlePieces.Length == 0)
        {
            Debug.LogError("Puzzle pieces not assigned for " + gameObject.name);
        }
        else
        {
            // Initialiser les pièces en activant seulement la première
            ActivatePuzzlePiece(0);
        }

        // Trouver le joueur dans la scène (supposant que le joueur a le tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (atmScreen != null)
        {
            atmScreen.SetActive(false);  // Assure que l'écran est désactivé au début
        }
        else
        {
            Debug.LogError("ATM screen not assigned!");
        }
    }

    void Update()
    {
        // Gérer la visibilité de l'écran en fonction de la distance avec le joueur
        if (player != null && atmScreen != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= detectionRange)
            {
                atmScreen.SetActive(true);  // Activer l'écran de l'ATM
            }
            else
            {
                atmScreen.SetActive(false);  // Désactiver l'écran de l'ATM
            }
        }
    }

    public void Interact()
    {
        if (puzzlePieces != null && puzzlePieces.Length > 0)
        {
            pieceCounter = (pieceCounter + 1) % puzzlePieces.Length;
            ActivatePuzzlePiece(pieceCounter);
            Debug.Log("Changed piece to " + puzzlePieces[pieceCounter].name + " on " + gameObject.name);

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
