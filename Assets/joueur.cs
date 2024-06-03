using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public float vitesseforward = 5f;
    public float othervitesse = 5f; // Vitesse de déplacement latéral et arrière
    public float vitesseSprint = 8f; // Vitesse de déplacement avec Shift
    public float sensibilitéSouris = 100f;
    public Transform joueurBody; // Assurez-vous d'assigner ce Transform dans l'inspecteur
    private float rotationX = 0f; // Stocke la rotation actuelle sur l'axe X

    void Start()
    {
        // Verrouille le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Rotation autour de l'axe Y (tourner la tête à droite et à gauche)
        float sourisX = Input.GetAxis("Mouse X") * sensibilitéSouris * Time.deltaTime;
        joueurBody.Rotate(Vector3.up * sourisX);

        // Rotation autour de l'axe X (regarder en haut et en bas)
        float sourisY = Input.GetAxis("Mouse Y") * sensibilitéSouris * Time.deltaTime;
        rotationX -= sourisY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limiter la rotation pour éviter les rotations complètes

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        float vitesseActuelle = vitesseforward;

        // Si Shift est enfoncé, utiliser la vitesse de sprint
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            vitesseActuelle = vitesseSprint;
        }

        // Déplacement vers l'avant (Z sur AZERTY, W sur QWERTY)
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            joueurBody.Translate(Vector3.forward * vitesseActuelle * Time.deltaTime);
        }

        // Déplacement vers l'arrière (S sur AZERTY et QWERTY)
        if (Input.GetKey(KeyCode.S))
        {
            joueurBody.Translate(Vector3.back * othervitesse * Time.deltaTime);
        }

        // Déplacement vers la gauche (Q sur AZERTY, A sur QWERTY)
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
        {
            joueurBody.Translate(Vector3.left * othervitesse * Time.deltaTime);
        }

        // Déplacement vers la droite (D sur AZERTY et QWERTY)
        if (Input.GetKey(KeyCode.D))
        {
            joueurBody.Translate(Vector3.right * othervitesse * Time.deltaTime);
        }
    }
}
