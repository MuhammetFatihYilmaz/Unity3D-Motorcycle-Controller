using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    //
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource motorcycleEngineSound;
    [SerializeField] AudioSource motorcycleSpeedUpSound;
    [SerializeField] AudioClip motorcycleBrakingSound;

    void Start()
    {
        Instance = this;
    }

    public void PlayMotorcycleSpeedUpSound()
    {
        motorcycleSpeedUpSound.Play();
    }

    public void StopMotorcycleSpeedUpSound()
    {
        motorcycleSpeedUpSound.Stop();
    }

    public void PlayMotorcycleEngineSound()
    {
        motorcycleEngineSound.Play();
    }

    public void StopMotorcycleEngineSound()
    {
        motorcycleEngineSound.Stop();
    }

    public void PlayMotorcycleBrakingSound()
    {
        audioSource.PlayOneShot(motorcycleBrakingSound);
    }

}
