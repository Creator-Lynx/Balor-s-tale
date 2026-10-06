using System.Collections;
using UnityEngine;

public class EnvironmentTorchController : MonoBehaviour
{

    [SerializeField] AudioSource burnupAudiosource;

    [SerializeField] AudioSource burningAudioSource;



    Animator torchAnimator;
    [SerializeField] ParticleSystem fire_main;
    [SerializeField] ParticleSystem fire_burst;
    [SerializeField] ParticleSystem fire_smoke;
    [SerializeField] Animation sparksLightAnimation;

    void Awake()
    {
        torchAnimator = GetComponent<Animator>();
    }
    int countToFire = 3;
    bool isFireUp = false;
    bool isOnMatchingDelay = false;


    
 

    void BurnUp()
    {

        isFireUp = true;
        torchAnimator.SetBool("IsLight", true);
        fire_main.Play();
        fire_smoke.Play();
        fire_burst.Stop();
        fire_burst.Play();
        burnupAudiosource.Play();
        burningAudioSource.Play();
    }


    public void EndOfFire()
    {
        //method for the end of scene, when fire stop working before scene transition

        //THERE IS NEED A SPECIAL SOUND!!! (in timeline)

        StopAllCoroutines();
        isOnMatchingDelay = true;
        isFireUp = true;
        torchAnimator.SetBool("IsLight", false);
        fire_main.Stop(); 
        fire_smoke.Stop();
        burningAudioSource.Stop();//ambient off
        //sound burn down
    }
}
