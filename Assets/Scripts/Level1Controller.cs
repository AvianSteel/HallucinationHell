using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject umbrellaPanel;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerController controller;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = controller.IsPaused;
        pausePanel.gameObject.SetActive(false);
        umbrellaPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        isPaused = controller.IsPaused;
    }
    public void UmbrellaTutorial()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        pausePanel.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        umbrellaPanel.gameObject.SetActive(true);
    }

    public void HideTutorial()
    {
        controller.IsPaused = false;
        Time.timeScale = 1;
        pausePanel.gameObject.SetActive(false);
        umbrellaPanel.gameObject.SetActive(false);
    }
}
