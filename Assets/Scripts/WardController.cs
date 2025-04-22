using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardController : MonoBehaviour
{
    [SerializeField] private GameObject ward;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Staff"))
        {
            Destroy(ward);
        }
    }
}
