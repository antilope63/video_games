using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceDebugger : MonoBehaviour
{
    public GameObject[] puzzlePieces;

    void Update()
    {
        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            Debug.Log(puzzlePieces[i].name + " active state: " + puzzlePieces[i].activeSelf);
        }
    }
}
