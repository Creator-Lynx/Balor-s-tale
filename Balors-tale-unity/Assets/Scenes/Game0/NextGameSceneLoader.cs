using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGameSceneLoader : MonoBehaviour
{
    string sceneName = "Game1";
    AsyncOperation asyncOperation;
    public void PreLoadScene()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
    }

    public void StartScene()
    {
        if(asyncOperation != null)
        asyncOperation.allowSceneActivation = true;
    }

}
