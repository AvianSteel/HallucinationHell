/*****************************************************************************
// File Name : MainMenuController.cs
// Author : Bryson Welch
// Creation Date : April 6, 2025
//
// Brief Description : Main Menu Buttons
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Makes sure that the cursor can move and is visible
    /// </summary>
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    /// <summary>
    /// Loads the first level
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
    /// <summary>
    /// Takes you to the start screen
    /// </summary>
    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }
}
