/*****************************************************************************
// File Name : PauseMenuController.cs
// Author : Bryson Welch
// Creation Date : April 12, 2025
//
// Brief Description : Controls the player's pause menu
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] private PlayerController controller;
    private bool isPaused;

    [SerializeField] private GameObject pausePanel;
    /// <summary>
    /// Makes sure the game runs and is not frozen on start
    /// </summary>
    private void Start()
    {
        isPaused = controller.IsPaused;
        pausePanel.gameObject.SetActive(false);
    }
    /// <summary>
    /// Resumes gameplay
    /// </summary>
    public void ResumeGame()
    {
        controller.IsPaused = false;
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    /// <summary>
    /// Closes the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
    /// <summary>
    /// Resets the stage, used for if stuck or got OOB
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Updates the isPaused variable
    /// </summary>
    void Update()
    {
        isPaused = controller.IsPaused;
    }
}
