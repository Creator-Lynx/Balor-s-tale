using UnityEngine;
using UnityEngine.InputSystem;

public class ChainRestrictedPlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;
    [SerializeField] GameObject restrictedCamera;

    InputAction interact;

    void Awake()
    {
        interact = InputSystem.actions.FindAction("Interact");
        mainPlayer.SetActive(false);
    }


    void Update()
    {
        if(interact.WasPressedThisFrame())
        {
            //temp changing the controlls
            ChangeControllsToPlayer();
        }
    }

    void ChangeControllsToPlayer()
    {
        mainPlayer.SetActive(true);
        restrictedCamera.SetActive(false);
        gameObject.SetActive(false);
    }
}
