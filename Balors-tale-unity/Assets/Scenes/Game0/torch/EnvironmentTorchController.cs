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

    void Awake()
    {
        torchAnimator = GetComponent<Animator>();
    }


    
 

    public void BurnUp()
    {

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
        StopAllCoroutines();
        torchAnimator.SetBool("IsLight", false);
        fire_main.Stop(); 
        fire_smoke.Stop();
        burningAudioSource.Stop();//ambient off
        //sound burn down
    }
}
