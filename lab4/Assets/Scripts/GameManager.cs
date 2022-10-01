using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    // Simple singleton script. This is the easiest way to create and understand a singleton script.
    [SerializeField] private HealthDisplay displayPlayerhealth;
    [SerializeField] private int health =3;

    private void Awake()
    {
        var numGameManager = FindObjectsOfType<GameManager>().Length;

        if (numGameManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            displayPlayerhealth.HealthUpdate(health);
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        health--;
        displayPlayerhealth.HealthUpdate(health);
        if (health == 0)
        {
            LoadMainMenu();
        }
        else
        {
            LoadLevel(GetCurrentBuildIndex());
        }
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
        DOTween.KillAll();
    }

    public void LoadMainMenu()
    {
        LoadLevel(0);
        Destroy(gameObject);
    }

    public void LoadNextLevel()
    {
        var nextSceneIndex = GetCurrentBuildIndex() + 1;
        
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        
        LoadLevel(nextSceneIndex);
        DOTween.KillAll();
    }

    private int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    
}
