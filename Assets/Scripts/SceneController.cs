using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private int _menuSceneIndex = 0;
    private int _currentSceneIndex;

    public void TryLoadNextScene()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(++_currentSceneIndex);
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }

}
