using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void onPause(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;

        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
    }

    public void onMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0);
    }

    public void onRestart()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(1);
    }

    public void onQuit()
    {
        Time.timeScale = 1;

        Application.Quit();
    }
}
