/*****************************************************************************
// File Name : Level1Controller.cs
// Author : Bryson Welch
// Creation Date : April 21, 2025
//
// Brief Description : Sets important variables and shows tutorial text
*****************************************************************************/
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    [SerializeField] private GameObject umbrellaText;

    /// <summary>
    /// Sets the umbrella tutorial text to false
    /// </summary>
    void Start()
    {
        umbrellaText.gameObject.SetActive(false);
    }
    /// <summary>
    /// makes tutorial text appear
    /// </summary>
    public void UmbrellaTutorial()
    {
        umbrellaText.gameObject.SetActive(true);
    }
}
