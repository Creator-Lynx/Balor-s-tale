using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlsOnOff : MonoBehaviour
{
    public static bool InCutscene = false; 
    [SerializeField] InputActionAsset InputActions;
    public void ControlsOn()
    {
        InCutscene = false;
        InputActions.FindActionMap("Player").Enable();
    }
    public void ControlsOff()
    {
        InCutscene = true;
        InputActions.FindActionMap("Player").Disable();
    }
}
