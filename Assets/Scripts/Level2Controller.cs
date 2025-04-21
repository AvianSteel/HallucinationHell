using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{

    [SerializeField] private GameObject staffTutorialText;
    // Start is called before the first frame update
    void Start()
    {
        staffTutorialText.SetActive(false);
    }
    public void StaffTutorial()
    {
        staffTutorialText.SetActive(true);
    }
}
