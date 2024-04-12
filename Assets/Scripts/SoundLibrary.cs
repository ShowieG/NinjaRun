using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    private static SoundLibrary _i;

    public static SoundLibrary i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<SoundLibrary>("Sounds"));
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
}
