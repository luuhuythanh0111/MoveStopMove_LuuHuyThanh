using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource effectAudioSource;
    [SerializeField] private AudioSource[] audioSources;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip[] audioClips;

    private int indexOfList = 0;

    public void PlayEffectSound(int index)
    {
        //if (audioSources[indexOfList].isPlaying == true)
        //{
        //    indexOfList++;
        //    if (indexOfList == audioSources.Length)
        //    {
        //        indexOfList = 0;
        //    }
        //    audioSources[indexOfList].PlayOneShot(audioClips[index]);
        //}
        //else
        //{
        //    audioSources[indexOfList].PlayOneShot(audioClips[index]);
        //    indexOfList++;
        //    if (indexOfList == audioSources.Length)
        //    {
        //        indexOfList = 0;
        //    }
        //}


        for(int i=0; i<audioSources.Length; i++)
        {
            if(audioSources[i].isPlaying == true)
            {
                continue;
            }
            else
            {
                audioSources[i].PlayOneShot(audioClips[index]);
                break;
            }
        }

        //effectAudioSource.PlayOneShot(audioClips[index]);
    }

    public bool GetMusicAudioSourceMute()
    {
        return musicAudioSource.mute;
    }

    public void MuteMusicAudioSource()
    {
        musicAudioSource.mute = true;
    }

    public void UnmuteMusicAudioSource()
    {
        musicAudioSource.mute = false;
    }
}

public enum AudioClipEnum
{
    Throw = 0,
    Die = 1,
    GetHit = 2,
    ButtonClick = 3,
    Lose = 4,
    Win = 5,
    ScaleUp = 6,
}
