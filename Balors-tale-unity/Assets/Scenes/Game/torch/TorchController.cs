using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorchController : MonoBehaviour
{
    InputAction fireAction;

    [SerializeField] AudioClip matchClip;
    [SerializeField] AudioSource matchAudioSource;
    [SerializeField] AudioSource burnupAudiosource;

    [SerializeField] float baseBurningTime = 10f;
    [SerializeField] float randomBurningTime = 5f;

    Animator torchAnimator;

    void Awake()
    {
        fireAction = InputSystem.actions.FindAction("Interact");
    }
    int countToFire = 3;
    bool isFireUp = false;
    void Update()
    {
        if(!isFireUp)
        if(fireAction.WasPressedThisFrame())
        {
            Matching();
        }

    }

    IEnumerator BurnTime()
    {
        yield return new WaitForSeconds (baseBurningTime + Random.Range(0f, randomBurningTime));
        isFireUp = false;
        torchAnimator.SetBool("IsLight", false);
        //sound burn down
        //ambient off
    }

    int count = 0;
    void Matching()
    {
        matchAudioSource.PlayOneShot(matchClip);
        //vfx
        count++;
        
    }

    void BurnUp()
    {
        count = 0;
        torchAnimator.SetBool("IsLight", true);
        burnupAudiosource.Play();
        StartCoroutine(BurnTime());
    }
}
