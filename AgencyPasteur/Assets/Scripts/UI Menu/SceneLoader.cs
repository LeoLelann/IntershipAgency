using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Scene sceneToLoad;
    [SerializeField] private string _sceneName;
    public void LoadSceneByName()
    {
        SceneManager.LoadScene(_sceneName);
    }

    //public void LoadNextInBuild()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}