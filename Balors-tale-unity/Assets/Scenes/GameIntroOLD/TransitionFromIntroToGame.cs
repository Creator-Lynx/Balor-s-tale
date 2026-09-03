using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionFromIntroToGame : MonoBehaviour
{
    public void StartGame()
    {
        //there is may be a corutine
        SceneManager.LoadScene("Game");
    }

    void Awake() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
