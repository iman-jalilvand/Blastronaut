using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor; // Allows stopping Play Mode in the editor
#endif

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial"); // Load the Tutorial (game) scene
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // Stop Play Mode in the Unity Editor
        #else
        Application.Quit(); // Quit the game when built (does not work in the Unity Editor)
        #endif
    }
}
