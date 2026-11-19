using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class ChainRestrictedPlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;
    [SerializeField] GameObject restrictedCamera;
    [SerializeField] ChainSoundRandomizer chainSoundRandomizer;
    [SerializeField] PlayableDirector endOfRestrictedStageCutscene;

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

        if(count >= countToChainBreak)
        {
            PerformChainBreak();
            //tmp just change controlls
            //ChangeControllsToPlayer();
            return;
        }

        //Make sound --- done
        chainSoundRandomizer.Play();
        
        //Make animation --- done
        if(Random.Range(0, 2) == 0)
        {
            restrictedCameraAnimator.SetTrigger("TriggerShake1");
        }
        else
        {
            restrictedCameraAnimator.SetTrigger("TriggerShake2");
        }
    } 


    [Space(30)]
    [Header("Visuals")]
    [SerializeField] Animator restrictedCameraAnimator;

    IEnumerator ChainBreakingDelay()
    {
        readyToChainBreak = false;
        yield return new WaitForSeconds(delayBetweenInput);
        readyToChainBreak = true;
    }

    void PerformChainBreak()
    {
       //Animate chain falling -- done
       //sound of a chain breaking -- done

       endOfRestrictedStageCutscene.Play();
    }

    //calls by playable director
    void ChangeControllsToPlayer()
    {
        mainPlayer.SetActive(true);
        restrictedCamera.SetActive(false);
        gameObject.SetActive(false);
    }
}
