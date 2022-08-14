using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseUI;
    private BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;    
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (buildManager.IsBuilding())
            {
                buildManager.SelectTurretToBuild(null);
                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Toggle();
            }
        }
    }


    public void Toggle()
    {
        PauseUI.SetActive(!PauseUI.activeSelf);

        if (PauseUI.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void GoToMenu()
    {
        Toggle();
        SceneManager.LoadScene("MainMenu");
    }
}
