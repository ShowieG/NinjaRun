using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public DangerManager dangerManagerScript;

    public SoundLibrary[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private bool startSecondSong =  false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        /*else
        {
            Destroy(gameObject);
        }*/
    }

    private void Start()
    {
        PlayMusic("Crickets");
    }

    // BGM1 plays via GameManager, once that is done playing it will switch to BGM2
    private void Update()
    {
        if(dangerManagerScript.dangerLevel == 3)
        {
            if(startSecondSong == false)
            {
                //print("Play second music");
                PlayMusic("BGM2");
                startSecondSong = true;
            }
        }
    }

    public void PlayMusic(string name)
    {
        SoundLibrary s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log("Sound (" + name + ") not found");
        } else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        SoundLibrary s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound (" + name + ") not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.audioClip);
        }
    }
}
