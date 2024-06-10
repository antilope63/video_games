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
        if (other.CompareTag("Player") && !isOpen && !isPuzzleDoor)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen && !isPuzzleDoor)
        {
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
