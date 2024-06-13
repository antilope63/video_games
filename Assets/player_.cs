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

    private float verticalRotation = 0f;
    private bool isGrounded;
    private float sprintTimer = 0f;
    private float sprintCooldownTimer = 0f;
    private bool canSprint = true;
    public GameObject heldObject = null; // Rendre public pour accéder depuis ObjectInteraction

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Empêche le Rigidbody de tourner à cause de la physique
        Cursor.lockState = CursorLockMode.Locked;
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
