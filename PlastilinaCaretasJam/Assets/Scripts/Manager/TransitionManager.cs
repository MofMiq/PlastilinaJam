using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private static string nextScene;

    public static void SetNextScene(string sceneName)
    {
        nextScene = sceneName;
    }
    public static void LoadTransition(string targetScene)
    {
        nextScene = targetScene;
        SceneManager.LoadScene("PruebasJuande");
    }

    public static void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("TransitionManager: No hay escena siguiente guardada");
        }
    }

    public static string GetNextScene()
    {
        return nextScene;
    }
}
