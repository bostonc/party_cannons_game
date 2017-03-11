using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    rotate,
    launch,
    hitPlayer,
    hitWall,
    jump,
    gameStart,
    gameEnd,
    swap
};

public class AudioDriver : MonoBehaviour
{
    static public AudioDriver S;

    //NOTE: for most predictable functionality, it is best 
    //to have a separate AudioSource for each AudioClip,
    //or sounds could be clipped in undesireable ways.

    //SOUNDS
    //cannon
    public AudioClip rotateSound;
    public AudioClip launchSound;    
    public AudioClip hitPlayerSound;
    public AudioClip hitWallSound;
    //runner
    public AudioClip jumpSound;
    //general
    public AudioClip gameStartSound;
    public AudioClip gameEndSound;
    public AudioClip swapSound;

    //SOURCES
    //cannon
    public AudioSource rotateSource;
    public AudioSource launchSource;
    public AudioSource hitPlayerSource;
    public AudioSource hitWallSource;
    //runner
    public AudioSource jumppSource;
    //general
    public AudioSource gameStartSource;
    public AudioSource gameEndSource;
    public AudioSource swapSource;
    public AudioSource generalSource; //to be used for discrete, non-overlapping sounds only!


    public bool _________________;

    private void Awake()
    {
        S = this;
    }

    public void play(SoundType s)
    {
        switch(s)
        {
            case SoundType.rotate:
                if (rotateSource != null) rotateSource.PlayOneShot(rotateSound);
                break;
            case SoundType.launch:
                if (launchSource != null) launchSource.PlayOneShot(launchSound);
                break;
            case SoundType.hitPlayer:
                if (hitPlayerSource != null) hitPlayerSource.PlayOneShot(hitPlayerSound);
                break;
            case SoundType.hitWall:
                if (hitWallSource != null) hitWallSource.PlayOneShot(hitWallSound);
                break;
            case SoundType.jump:
                if (jumppSource != null) jumppSource.PlayOneShot(jumpSound);
                break;
            case SoundType.gameStart:
                if (gameStartSource != null) gameStartSource.PlayOneShot(gameStartSound);
                break;
            case SoundType.gameEnd:
                if (gameEndSource != null) gameEndSource.PlayOneShot(gameEndSound);
                break;
            case SoundType.swap:
                if (swapSource != null) swapSource.PlayOneShot(swapSound);
                break;
            default:
                Debug.Assert(false);
                break;
        }
    }

    public void play(AudioClip clip, float volume)
    {
        if (generalSource != null) generalSource.PlayOneShot(clip, volume);
    }



}
