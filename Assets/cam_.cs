using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public float raycastRange = 10.0f;
    private PlayerMovementController playerMovementController;

    void Start()
    {
        playerMovementController = GetComponentInParent<PlayerMovementController>();
    }

    void Update()
    {
        // Raycasting
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (playerMovementController.heldObject == null)
                    {
                        playerMovementController.PickUpObject(hit.collider.gameObject);
                    }
                    else
                    {
                        playerMovementController.DropObject();
                    }
                }
            }
        }
    }
}
