using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordInputController : MonoBehaviour
{
    public GameObject passwordPanel; // Référence au panneau du mot de passe
    public GameObject passwordInputFieldObject; // Référence à l'input field GameObject
    public GameObject codetextObject; // Référence à l'InputField pour le code (codetext)
    public Button submitButton; // Référence au bouton de soumission
    public string correctPassword = "8156"; // Le mot de passe correct

    private InputField passwordInputField;
    private InputField codetext;

    private void Start()
    {
        // Vérifiez que les objets sont assignés
        if (passwordInputFieldObject == null)
        {
            Debug.LogError("Password Input Field Object is not assigned.");
        }
        else
        {
            // Obtenir le composant InputField à partir du GameObject
            passwordInputField = passwordInputFieldObject.GetComponent<InputField>();
            if (passwordInputField == null)
            {
                Debug.LogError("Password Input Field component is not found on the assigned object.");
            }
        }

        if (codetextObject == null)
        {
            Debug.LogError("Code Text Object is not assigned.");
        }
        else
        {
            // Obtenir le composant InputField à partir du GameObject
            codetext = codetextObject.GetComponent<InputField>();
            if (codetext == null)
            {
                Debug.LogError("Code Text Input Field component is not found on the assigned object.");
            }
        }

        // Assurez-vous que le panneau est désactivé au début
        if (passwordPanel != null)
        {
            passwordPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Password Panel is not assigned.");
        }

        // Ajoutez un listener pour le bouton de soumission
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(CheckPassword);
        }
        else
        {
            Debug.LogError("Submit Button is not assigned.");
        }
    }

    public void OpenPasswordPanel()
    {
        Debug.Log("Opening password panel...");
        if (passwordPanel != null)
        {
            passwordPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Password Panel is not assigned.");
        }
    }

    public void ClosePasswordPanel()
    {
        if (passwordPanel != null)
        {
            passwordPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Password Panel is not assigned.");
        }
    }

    private void CheckPassword()
    {
        if (codetext != null)
        {
            string enteredPassword = codetext.text;

            if (enteredPassword == correctPassword)
            {
                Debug.Log("Correct password!");
                // Ajouter une action lorsque le mot de passe est correct, par exemple ouvrir une porte
                ClosePasswordPanel();
            }
            else
            {
                Debug.Log("Incorrect password. Try again.");
                if (passwordInputField != null)
                {
                    passwordInputField.text = ""; // Réinitialise le champ d'input
                }
                else
                {
                    Debug.LogError("Password Input Field is not assigned.");
                }
            }
        }
        else
        {
            Debug.LogError("Code Text Input Field is not assigned.");
        }
    }
}
