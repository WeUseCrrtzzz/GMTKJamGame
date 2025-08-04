using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool IsPaused { get; private set; } = false;

    [Header("UI Elements")]
    public GameObject pauseMenuUI;

    [Header("Optional Systems")]
    public GameObject allRadiusDamageZone; // assign this if it exists

    [Header("Scene Names")]
    public string mainMenuSceneName = "MainMenu";
    public string mainGameSceneName = "MainGame";

    private void Start()
    {
        if (pauseMenuUI != null) 
        {
            pauseMenuUI?.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        pauseMenuUI?.SetActive(true);

        if (allRadiusDamageZone != null)
            allRadiusDamageZone.SetActive(false);
    }

    public void ResumeGame()
    {
        IsPaused = false;
        pauseMenuUI?.SetActive(false);

        if (allRadiusDamageZone != null)
            allRadiusDamageZone.SetActive(true);
    }

    public void TogglePause()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }

    public void ReturnToMainMenu()
    {
        IsPaused = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void OpenOptions()
    {
        Debug.Log("Options button clicked");
        // Optional: Toggle a UI panel or load a different scene
        // Example: optionsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // for testing in Editor
        #endif
    }
}
