using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public string openDirection; // "right", "left", "up", "down"
    public AudioClip openSound;
    public AudioClip closeSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayOpenAnimation();
            PlaySound(openSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCloseAnimation();
            PlaySound(closeSound);
        }
    }

    private void PlayOpenAnimation()
    {
        switch (openDirection)
        {
            case "right":
                doorAnimator.SetTrigger("openRight");
                break;
            case "left":
                doorAnimator.SetTrigger("openLeft");
                break;
            case "up":
                doorAnimator.SetTrigger("openUp");
                break;
            case "down":
                doorAnimator.SetTrigger("openDown");
                break;
        }
    }

    private void PlayCloseAnimation()
    {
        switch (openDirection)
        {
            case "right":
                doorAnimator.SetTrigger("closeRight");
                break;
            case "left":
                doorAnimator.SetTrigger("closeLeft");
                break;
            case "up":
                doorAnimator.SetTrigger("closeUp");
                break;
            case "down":
                doorAnimator.SetTrigger("closeDown");
                break;
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
