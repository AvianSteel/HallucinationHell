using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    [SerializeField] private GameObject umbrellaText;

    // Start is called before the first frame update
    void Start()
    {
        umbrellaText.gameObject.SetActive(false);
    }
    public void UmbrellaTutorial()
    {
        umbrellaText.gameObject.SetActive(true);
    }
}
