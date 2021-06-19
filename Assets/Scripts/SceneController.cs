using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int _menuSceneIndex = 0;
    private int _currentSceneIndex;

    public void TryLoadNextScene()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (++_currentSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }

}
