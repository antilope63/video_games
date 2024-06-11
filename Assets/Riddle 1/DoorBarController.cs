using UnityEngine;

public class DoorBarController : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource audioSource;
    public bool isPuzzleDoor = false; // Indique si c'est une porte de puzzle

    private bool isOpen = false; // Indique si la porte est ouverte

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called on " + gameObject.name);
        if (other.CompareTag("Player") && !isOpen && !isPuzzleDoor)
        {
            Debug.Log("Opening door on trigger enter: " + gameObject.name);
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit called on " + gameObject.name);
        if (other.CompareTag("Player") && isOpen && !isPuzzleDoor)
        {
            Debug.Log("Closing door on trigger exit: " + gameObject.name);
            CloseDoor();
        }
    }

    public void OpenDoor() // Méthode publique pour ouvrir la porte
    {
        Debug.Log("OpenDoor called on " + gameObject.name + ", isPuzzleDoor: " + isPuzzleDoor);
        if (!isOpen) // Vérification pour éviter les appels multiples
        {
            doorAnimator.SetTrigger("open");
            PlaySound(openSound);
            isOpen = true; // Marquer la porte comme ouverte
        }
    }

    public void CloseDoor() // Méthode publique pour fermer la porte
    {
        Debug.Log("CloseDoor called on " + gameObject.name + ", isPuzzleDoor: " + isPuzzleDoor);
        if (isOpen) // Vérification pour éviter les appels multiples
        {
            doorAnimator.SetTrigger("close");
            PlaySound(closeSound);
            isOpen = false; // Marquer la porte comme fermée
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
