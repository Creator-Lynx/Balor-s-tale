using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public class InGameMenu : MonoBehaviour
{
    [SerializeField] InputActionAsset InputActions;
    Canvas canvas;
    void Awake()
    {
        canvas = GetComponent<Canvas>();
    }
    bool stateMenu = false;
    // Update is called once per frame
    void Update()
    {
        if(!PlayerControlsOnOff.InCutscene) //need to get a func for cutscene pause
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(stateMenu)
            {
                canvas.enabled = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                stateMenu = false;
                InputActions.FindActionMap("Player").Enable();
            }
            else
            {
                canvas.enabled = true;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                stateMenu = true;
                InputActions.FindActionMap("Player").Disable();
            }
        }
    }
}
