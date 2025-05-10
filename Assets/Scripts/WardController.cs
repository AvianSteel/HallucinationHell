/*****************************************************************************
// File Name : WardController.cs
// Author : Bryson Welch
// Creation Date : April 12, 2025
//
// Brief Description : Collision and triggers for a removable wall
*****************************************************************************/
using UnityEngine;

public class WardController : MonoBehaviour
{
    [SerializeField] private GameObject ward;
    /// <summary>
    /// Gets destroyed if it touches the staff object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Staff"))
        {
            Destroy(ward);
        }
    }
}
