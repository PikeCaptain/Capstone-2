using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public TMP_Text survivedWavesText;
    public GameObject restart;
    // public TMP_Text survivedWavesText; // Use this line if you're using TextMeshPro

    private int wavesSurvived = 0;

    public void SetWavesSurvived(int waves)
    {
        wavesSurvived = waves;
    }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(restart);

        survivedWavesText.text = "You Survived " + wavesSurvived + " Waves!";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Time.timeScale = 2f;
        SceneManager.LoadScene(0);
    }
}
