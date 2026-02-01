

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        //if (sceneName == "Scenes/01.SceneOruga")
        //{
        //    Debug.Log("LLEGO" + sceneName);
        //    SceneManager.LoadScene("Scenes/PruebasJuande");    
        //}
        SceneManager.LoadScene(sceneName);
    }
}
