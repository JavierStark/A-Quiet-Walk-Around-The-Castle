using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerSFX : MonoBehaviour
{
    [SerializeField] private List<AudioClip> lightFootSteeps = new List<AudioClip>();
    [SerializeField] private List<AudioClip> mediumFootSteeps = new List<AudioClip>();

    [SerializeField] private AudioSource footAudioSource; 
    
    private float _footSteepTimer = 0;
    [SerializeField] private float timeBetweenFootSteeps;

    private void Start()
    {
        _footSteepTimer = timeBetweenFootSteeps;
    }

    public void PlayLightFootSteep()
    {
        _footSteepTimer += Time.deltaTime;
        if (_footSteepTimer < timeBetweenFootSteeps) return;
        
        footAudioSource.PlayOneShot(GetRandomClip(lightFootSteeps));
        _footSteepTimer = 0;
    }
    
    public void PlayMediumFootSteep()
    {
        _footSteepTimer += Time.deltaTime;
        if (_footSteepTimer < timeBetweenFootSteeps) return;
        
        footAudioSource.PlayOneShot(GetRandomClip(mediumFootSteeps));
        _footSteepTimer = 0;
    }

    public void CancelFootSteepTimer()
    {
        _footSteepTimer = timeBetweenFootSteeps;
    }

    private AudioClip GetRandomClip(List<AudioClip> clips)
    {
        return clips[Random.Range(0, clips.Count)];
    }
}
