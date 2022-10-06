using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SoAudioClips deadAudioClips;
    [SerializeField] private SoAudioClips winAudioClips;

    private void Awake()
    {
        var numGameManager = FindObjectsOfType<GameManager>().Length;

        if (numGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        UpdateLives();
    }
    
    public void ProcessPlayerDeath()
    {
        playerLives--;
        PlayDeadSound();
        
        switch (playerLives)
        {
            case >= 1:
                LoadScene(GetCurrentBuildIndex());
                UpdateLives();
                break;
            default:
                ReturnToMainMenu();
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        LoadScene(0);
        Destroy(gameObject);
    }
    
    public void LoadNextLevel()
    {
        var nextSceneIndex = GetCurrentBuildIndex() + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        
        LoadScene(nextSceneIndex);
    }

    private int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    private void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        DOTween.KillAll();
    }

    private void UpdateLives()
    {
        uiManager.UpdateLives(playerLives);
    }

    private void PlayDeadSound()
    {
        audioSource.PlayOneShot(deadAudioClips.GetAudioClip());
    }
    
    public void PlayWinSound()
    {
        audioSource.PlayOneShot(winAudioClips.GetAudioClip());
    }
}
