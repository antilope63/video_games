using UnityEngine;

public class DoorBarController : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource audioSource;

    private bool isOpen = false; // Variable pour suivre si la porte est ouverte

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            CloseDoor();
        }
    }

    private void OpenDoor()
    {
        doorAnimator.SetTrigger("open");
        PlaySound(openSound);
        isOpen = true; // Marquer la porte comme ouverte
    }

    private void CloseDoor()
    {
        doorAnimator.SetTrigger("close");
        PlaySound(closeSound);
        isOpen = false; // Marquer la porte comme fermée
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
