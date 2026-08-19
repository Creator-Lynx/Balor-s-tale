using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TorchController : MonoBehaviour
{
    InputAction fireAction;

    [SerializeField] AudioClip matchClip;
    [SerializeField] AudioSource matchAudioSource;
    float basePitch;
    [SerializeField] float matchAudioPitchRandomRange = 0.3f;
    [SerializeField] AudioSource burnupAudiosource;

    [SerializeField] float baseBurningTime = 10f;
    [SerializeField] float randomBurningTime = 5f;

    [SerializeField] float matchingDelay = 0.5f;

    Animator torchAnimator;
    [SerializeField] ParticleSystem igniSparks;
    [SerializeField] ParticleSystem fire_main;
    [SerializeField] ParticleSystem fire_burst;
    [SerializeField] ParticleSystem fire_smoke;
    [SerializeField] Animation sparksLightAnimation;

    void Awake()
    {
        fireAction = InputSystem.actions.FindAction("Interact");
        basePitch = matchAudioSource.pitch;
        torchAnimator = GetComponent<Animator>();
    }
    int countToFire = 3;
    bool isFireUp = false;
    bool isOnMatchingDelay = false;
    void Update()
    {
        if(!isFireUp)
        if(!isOnMatchingDelay)
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
        fire_main.Stop(); 
        fire_smoke.Stop();
        //sound burn down
        //ambient off
    }

    int count = 0;
    void Matching()
    {
        igniSparks.Play();
        if(count >= countToFire)
        {
            BurnUp();
            return;
        }
        matchAudioSource.pitch = basePitch + Random.Range(0f, matchAudioPitchRandomRange);
        matchAudioSource.PlayOneShot(matchClip);

        sparksLightAnimation.Stop();
        sparksLightAnimation.Play();

        count++;

        StartCoroutine(MatchingDelay());
    }
    IEnumerator MatchingDelay()
    {
        isOnMatchingDelay = true;
        yield return new WaitForSeconds(matchingDelay);
        isOnMatchingDelay = false;
    }

    void BurnUp()
    {
        count = 0;
        isFireUp = true;
        torchAnimator.SetBool("IsLight", true);
        fire_main.Play();
        fire_smoke.Play();
        fire_burst.Stop();
        fire_burst.Play();
        burnupAudiosource.Play();
        StartCoroutine(BurnTime());
    }
}
