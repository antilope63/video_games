using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_test : MonoBehaviour
{
    private Renderer cubeRenderer;

    void Start()
    {
        // Obtenir le composant Renderer du cube
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Vous pouvez ajouter du code ici si nécessaire
    }

    public void ChangeColor(Color newColor)
    {
        // Changer la couleur du cube
        if (cubeRenderer != null)
        {
            cubeRenderer.material.color = newColor;
        }
    }
}