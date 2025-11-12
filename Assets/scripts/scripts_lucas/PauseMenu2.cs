using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    public MonoBehaviour cameraLookScript; // arraste aqui o script de olhar da câmera (ex: MouseLook)

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

        // Bloqueia o mouse no centro e esconde
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Reativa o controle da câmera
        if (cameraLookScript != null)
            cameraLookScript.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        // Libera o mouse pra clicar no menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Desativa o controle da câmera
        if (cameraLookScript != null)
            cameraLookScript.enabled = false;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
