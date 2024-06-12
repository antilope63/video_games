using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float mouseSensitivity = 2.0f;
    public float jumpForce = 5.0f;
    public float sprintDuration = 2.0f;
    public float sprintCooldown = 3.0f;
    public Transform holdPosition; // Position où l'objet sera tenu
    public float raycastRange = 10.0f; // Portée du raycast

    public GameObject hintPanel1; // Référence au premier HintPanel
    public GameObject hintPanel2; // Référence au deuxième HintPanel
    public GameObject hintPanel3; // Référence au troisième HintPanel
    public GameObject hintPanel4; // Référence au quatrième HintPanel

    private float verticalRotation = 0f;
    private bool isGrounded;
    private float sprintTimer = 0f;
    private float sprintCooldownTimer = 0f;
    private bool canSprint = true;
    public GameObject heldObject = null; // Rendre public pour accéder depuis ObjectInteraction
    private GameObject currentHintPanel = null;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Empêche le Rigidbody de tourner à cause de la physique

        // Assurez-vous que les panneaux sont désactivés au début
        if (hintPanel1 != null) hintPanel1.SetActive(false);
        if (hintPanel2 != null) hintPanel2.SetActive(false);
        if (hintPanel3 != null) hintPanel3.SetActive(false);
        if (hintPanel4 != null) hintPanel4.SetActive(false);
    }

    void Update()
    {
        // Mouvements de la souris pour la rotation de la caméra
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Gestion du sprint
        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            sprintTimer += Time.deltaTime;
            if (sprintTimer > sprintDuration)
            {
                canSprint = false;
                sprintCooldownTimer = 0f;
            }
        }
        else
        {
            sprintTimer = 0f;
        }

        if (!canSprint)
        {
            sprintCooldownTimer += Time.deltaTime;
            if (sprintCooldownTimer > sprintCooldown)
            {
                canSprint = true;
            }
        }

        // Déplacements ZQSD (WASD) avec sprint
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && canSprint;
        float currentSpeed = isSprinting ? sprintSpeed : speed;

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveVertical) * currentSpeed * Time.deltaTime;
        transform.position += movement;

        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump initiated");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Mise à jour de la position de l'objet tenu
        if (heldObject != null)
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
        }

        // Interaction avec les objets (ramasser et déposer)
        HandleObjectInteraction();

        // Interaction avec les panneaux (énigmes)
        HandlePanelInteraction();
    }

    private void HandleObjectInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (heldObject == null)
                    {
                        PickUpObject(hit.collider.gameObject);
                    }
                    else
                    {
                        DropObject();
                    }
                }
            }
        }
    }

    private void HandlePanelInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Désactiver le panneau courant
                if (currentHintPanel != null)
                {
                    currentHintPanel.SetActive(false);
                    Debug.Log("Current panel disabled: " + currentHintPanel.name);
                }

                // Activer le panneau correspondant
                if (hit.collider.CompareTag("enigme2_1"))
                {
                    Debug.Log("Toggling hintPanel1");
                    ToggleHintPanel(hintPanel1);
                }
                else if (hit.collider.CompareTag("enigme2_2"))
                {
                    Debug.Log("Toggling hintPanel2");
                    ToggleHintPanel(hintPanel2);
                }
                else if (hit.collider.CompareTag("enigme2_3"))
                {
                    Debug.Log("Toggling hintPanel3");
                    ToggleHintPanel(hintPanel3);
                }
                else if (hit.collider.CompareTag("enigme2_4"))
                {
                    Debug.Log("Toggling hintPanel4");
                    ToggleHintPanel(hintPanel4);
                }
                else
                {
                    Debug.Log("No matching tag found for: " + hit.collider.gameObject.name);
                }
            }
        }
    }

    private void ToggleHintPanel(GameObject hintPanel)
    {
        if (hintPanel != null)
        {
            bool isActive = hintPanel.activeSelf;
            hintPanel.SetActive(!isActive);
            Debug.Log("Panel toggled: " + hintPanel.name + " | New state: " + !isActive);

            // Mettre à jour le panneau courant
            currentHintPanel = hintPanel;
        }
        else
        {
            Debug.Log("HintPanel n'est pas assigné.");
        }
    }

    public void PickUpObject(GameObject obj)
    {
        Debug.Log("Picking up: " + obj.name);
        heldObject = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.transform.SetParent(holdPosition, true); // worldPositionStays true to maintain world space
        obj.transform.localPosition = Vector3.zero; // Reset local position
        obj.transform.localRotation = Quaternion.identity; // Reset local rotation
    }

    public void DropObject()
    {
        if (heldObject != null)
        {
            Debug.Log("Dropping: " + heldObject.name);
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Vérifie si le joueur est au sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                Debug.Log("Player grounded");
            }
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Le joueur n'est plus au sol
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (isGrounded)
            {
                Debug.Log("Player not grounded");
            }
            isGrounded = false;
        }
    }
}
