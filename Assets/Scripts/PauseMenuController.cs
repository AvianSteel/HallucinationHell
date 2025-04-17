using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] private PlayerController controller;
    private bool isPaused;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        isPaused = controller.IsPaused;
        pausePanel.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        controller.IsPaused = false;
        pausePanel.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = controller.IsPaused;
    }
}
