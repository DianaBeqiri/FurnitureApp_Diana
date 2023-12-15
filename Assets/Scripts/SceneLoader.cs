using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad; // The name of the scene you want to load.

    void Start()
    {
        // Invoke the LoadScene method after 3 seconds (adjust the time delay as needed).
        Invoke("LoadScene", 4f);
    }

    void LoadScene()
    {
        // Load the specified scene.
        SceneManager.LoadScene(sceneToLoad);
    }
    public void UiScene()
    {
        SceneManager.LoadScene("AssetBundleGamePlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("UI");
    }
}
