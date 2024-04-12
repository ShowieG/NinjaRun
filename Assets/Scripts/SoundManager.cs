using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    //think i should add a sound source thats seperate to play all the bgm
    public enum Sound
    {
        Crickets,
        BGM1,
        BGM2,
        PlayerMoveLeft,
        PlayerMoveRight,
        EnemySlash,
        GunCharge,
        GunShoot,
        Spikes,
        StartSound,
        Smoke1,
        Smoke2,
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundLibrary.SoundAudioClip soundAudioClip in SoundLibrary.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
