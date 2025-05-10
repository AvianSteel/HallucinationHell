/*****************************************************************************
// File Name : LevelEndController.cs
// Author : Bryson Welch
// Creation Date : April 10, 2025
//
// Brief Description : Moves to the next level
*****************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndController : MonoBehaviour
{
    /// <summary>
    /// Moves to the next scene in unity order
    /// </summary>
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
