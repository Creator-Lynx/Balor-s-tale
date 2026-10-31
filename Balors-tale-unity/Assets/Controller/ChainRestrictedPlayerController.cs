using System.Collections;
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

    [Space(40)]
    [Header("Break the chain gameplay")]
    [SerializeField] float delayBetweenInput = 1.2f;

    bool readyToChainBreak = true;


    void Update()
    {
        if(interact.WasPressedThisFrame() && readyToChainBreak)
        {
            StartCoroutine(ChainBreakingDelay());
            CommitChainBreak();
        }
    }

    [SerializeField] int countToChainBreak = 8;
    int count = 0;
    void CommitChainBreak()
    {
        count++;

        //Make sound
        //Debug.Log("Breaking chains" + count);
        //Make animation

        if(count >= countToChainBreak)
        {
            PerformChainBreak();
            //tmp just change controlls
            ChangeControllsToPlayer();
        }
    } 

    IEnumerator ChainBreakingDelay()
    {
        readyToChainBreak = false;
        yield return new WaitForSeconds(delayBetweenInput);
        readyToChainBreak = true;
    }

    void PerformChainBreak()
    {
       
    }
    void ChangeControllsToPlayer()
    {
        mainPlayer.SetActive(true);
        restrictedCamera.SetActive(false);
        gameObject.SetActive(false);
    }
}
