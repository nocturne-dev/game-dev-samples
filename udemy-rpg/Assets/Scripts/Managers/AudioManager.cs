using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource[] bgm;
    [SerializeField] AudioClip[] sfx;

    // Start is called before the first frame update
    void Awake()
    {
        int numOfAudioManager = FindObjectsOfType<AudioManager>().Length;
        if(numOfAudioManager > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBGM(int musicToPlay)
    {
        if (bgm[musicToPlay].isPlaying)
        {
            return;
        }

        StopMusic();

        if(musicToPlay < bgm.Length)
        {
            bgm[musicToPlay].Play();
        }
    }

    void StopMusic()
    {
        foreach(AudioSource music in bgm)
        {
            music.Stop();
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay < sfx.Length)
        {
            AudioSource.PlayClipAtPoint(sfx[soundToPlay], Camera.main.transform.position);
        }
    }
}
