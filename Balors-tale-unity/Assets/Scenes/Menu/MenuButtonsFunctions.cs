using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsFunctions : MonoBehaviour
{
    public void ButtonStart()
    {
        //hide menu corrutine
        

        //async load gameintro

        // for test simple load intro scene
        SceneManager.LoadScene("Game0");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ButtonReStart()
    {
        //hide menu corrutine

        //async load gameintro

        // for test simple load intro scene
        SceneManager.LoadScene(0);
        //SceneManager.LoadScene("SplashScreen");
    }

    [SerializeField] GameObject settingsLayer;
    public void ButtonSettings()
    {
        settingsLayer.SetActive(!settingsLayer.activeSelf);
    }

    public void ButtonExit()
    {
        //Hide menu corutine
        
        Application.Quit();
    }
}
