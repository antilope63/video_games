using UnityEngine;

public class DoorBarController : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isOpen = false;

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            doorAnimator.SetTrigger("open");
            isOpen = true;
            Debug.Log("Door opened: " + gameObject.name);
        }
    }

    public void CloseDoor()
    {
        if (isOpen)
        {
            doorAnimator.SetTrigger("close");
            isOpen = false;
            Debug.Log("Door closed: " + gameObject.name);
        }
    }
}
