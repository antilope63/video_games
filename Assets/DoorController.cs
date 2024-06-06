using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource audioSource;

    private bool isOpen = false; // Variable pour suivre si la porte est ouverte
    private bool isAnimating = false; // Variable pour suivre si une animation est en cours

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen && !isAnimating)
        {
            doorAnimator.SetTrigger("open");
            PlaySound(openSound);
            isOpen = true; // Marquer la porte comme ouverte
            isAnimating = true; // Marquer qu'une animation est en cours
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen && !isAnimating)
        {
            doorAnimator.SetTrigger("close");
            PlaySound(closeSound);
            isOpen = false; // Marquer la porte comme fermée
            isAnimating = true; // Marquer qu'une animation est en cours
        }
    }

    void Update()
    {
        // Vérifier si l'animation est terminée
        if (isAnimating)
        {
            if (doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !doorAnimator.IsInTransition(0))
            {
                isAnimating = false; // L'animation est terminée
            }
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
