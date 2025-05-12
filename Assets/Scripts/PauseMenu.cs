using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    public Character character;
    public GameObject resume;

    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        character.cursorLocked = false;
        EventSystem.current.SetSelectedGameObject(resume);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
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
