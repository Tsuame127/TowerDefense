using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    #region Singleton
    public static MainMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("BuildManager instace already created");
            return;
        }
        instance = this;
    }
    #endregion

    public string levelToLoad = "LevelSelect";

    public void Play()
    {
        SceneManager.LoadScene(levelToLoad);
    }
    public void Options()
    {
        Debug.Log("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
