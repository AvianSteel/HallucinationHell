/*****************************************************************************
// File Name : Level2Controller.cs
// Author : Bryson Welch
// Creation Date : April 21, 2025
//
// Brief Description : Sets important variables and shows tutorial text
*****************************************************************************/
using UnityEngine;

public class Level2Controller : MonoBehaviour
{

    [SerializeField] private GameObject staffTutorialText;
    /// <summary>
    /// Sets Staff Text to false
    /// </summary>
    void Start()
    {
        staffTutorialText.SetActive(false);
    }
    /// <summary>
    /// Makes tutorial text appear
    /// </summary>
    public void StaffTutorial()
    {
        staffTutorialText.SetActive(true);
    }
}
