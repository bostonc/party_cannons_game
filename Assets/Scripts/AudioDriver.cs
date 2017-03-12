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
    public AudioClip rotateSound_0;
    public AudioClip rotateSound_1;
    public AudioClip launchSound_0;
    public AudioClip launchSound_1;
    public AudioClip launchSound_2;
    public AudioClip launchSound_3;
    public AudioClip hitPlayerSound_0;
    public AudioClip hitPlayerSound_1;
    public AudioClip hitWallSound;
    //runner
    public AudioClip jumpSound;
    //general
    public AudioClip gameStartSound;
    public AudioClip gameEndSound;
    public AudioClip swapSound;
    public AudioClip gameMusic_0;
    public AudioClip gameMusic_1;
    public AudioClip gameMusic_2;

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
        int num = -1;
        switch (s)
        {
            case SoundType.rotate:
                num = Random.Range(0, 2);
                if (num == 0 && rotateSource != null) rotateSource.PlayOneShot(rotateSound_0);
                if (num == 1 && rotateSource != null) rotateSource.PlayOneShot(rotateSound_1);
                break;
            case SoundType.launch:
                num = Random.Range(0, 4);
                if (num == 0 && launchSource != null) launchSource.PlayOneShot(launchSound_0);
                if (num == 1 && launchSource != null) launchSource.PlayOneShot(launchSound_1);
                if (num == 2 && launchSource != null) launchSource.PlayOneShot(launchSound_2);
                if (num == 3 && launchSource != null) launchSource.PlayOneShot(launchSound_3);
                break;
            case SoundType.hitPlayer:
                num = Random.Range(0, 2);
                if (num == 0 && hitPlayerSource != null) hitPlayerSource.PlayOneShot(hitPlayerSound_0);
                if (num == 1 && hitPlayerSource != null) hitPlayerSource.PlayOneShot(hitPlayerSound_1);
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
