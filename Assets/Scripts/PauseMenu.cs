using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    public Character character;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused) 
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        character.cursorLocked = false;
    }

     public void DeactivateMenu()
    {
        Time .timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        character.cursorLocked = true;
    }

    public void OptionsMenu()
    {
        Time.timeScale = 2f;
        SceneManager.LoadScene(3);
    }
}
