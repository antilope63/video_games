using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_cube : MonoBehaviour
{
    public float forceSaut = 5f; // Force du saut
    private Rigidbody rb; // Référence au composant Rigidbody
    private bool estAuSol; // Indique si le joueur est au sol

    void Start()
    {
        // Obtenir le composant Rigidbody
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody non trouvé sur player_cube. Assurez-vous qu'il en a un attaché.");
        }
    }

    void Update()
    {
        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && estAuSol)
        {
            Debug.Log("Saut déclenché."); // Message de débogage
            rb.velocity = (Vector3.up * forceSaut);
            Debug.Log("Force appliquée : " + (Vector3.up * forceSaut)); // Message de débogage
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision détectée avec : " + collision.gameObject.name); // Message de débogage
        Debug.Log("Tag de l'objet en collision : " + collision.gameObject.tag); // Afficher le tag de l'objet en collision

        // Vérifie si le joueur est au sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Le joueur est au sol."); // Message de débogage
            estAuSol = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision finie avec : " + collision.gameObject.name); // Message de débogage
        Debug.Log("Tag de l'objet en collision : " + collision.gameObject.tag); // Afficher le tag de l'objet en collision

        // Vérifie si le joueur quitte le sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Le joueur a quitté le sol."); // Message de débogage
            estAuSol = false;
        }
    }
}
