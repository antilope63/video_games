using UnityEngine;
using UnityEngine.Events;

public class DoorBarController : MonoBehaviour
{
    private Animator doorAnimator;
    private bool isOpen = false;

    // Définir la variable isDoorOpen
    public static bool isDoorOpen = false;

    // Définir l'événement OnDoorOpened
    public static UnityEvent OnDoorOpened = new UnityEvent();

    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }

        // S'abonner à l'événement OnDoorOpened
        PuzzleManager.OnPuzzleCompleted.AddListener(OpenDoor);
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            doorAnimator.SetTrigger("open");
            isOpen = true;
            Debug.Log("Door opened: " + gameObject.name);
            isDoorOpen = true;

            // Déclencher l'événement OnDoorOpened
            if (OnDoorOpened != null)
            {
                OnDoorOpened.Invoke();
            }
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
