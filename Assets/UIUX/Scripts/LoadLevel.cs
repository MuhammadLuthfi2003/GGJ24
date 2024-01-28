using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Function to load the specified level by name
    public void LoadLevel(string levelToLoad)
    {
        if (levelToLoad == "MainMenu")
        {
            PauseManager.isPaused = false;
            PauseManager.isWin = false;
        }
        SceneManager.LoadScene(levelToLoad);
    }
}