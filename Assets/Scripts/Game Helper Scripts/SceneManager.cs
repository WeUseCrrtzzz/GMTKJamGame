using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "MainGame"; // set to the actual gameplay scene name

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
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
