using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoAudioClips walkAudioClips;
    [SerializeField] private SoAudioClips jumpAudioClips;
    [SerializeField] private SoAudioClips jumpPadAudioClips;
    [SerializeField] private SoAudioClips collectAudioClips;
    [SerializeField] private SoAudioClips respawnAudioClips;
  

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpAudioClips.GetAudioClip());
    }

    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(walkAudioClips.GetAudioClip());
    }

    public void PlayJumpPadSound()
    {
        audioSource.PlayOneShot(jumpPadAudioClips.GetAudioClip());
    }

    public void PlayCollectSound()
    {
        audioSource.PlayOneShot(collectAudioClips.GetAudioClip());
    }

    public void PlayRespawnSound()
    {
        audioSource.PlayOneShot(respawnAudioClips.GetAudioClip());
    }
}
