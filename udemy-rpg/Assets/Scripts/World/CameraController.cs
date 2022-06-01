using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    // TESTING - AUDIO SELECTION
    [SerializeField] int audioToPlay = 0;
    private bool musicStarted = false;
    
    private CinemachineVirtualCamera cvc;

    public void SetCinemachineVirtualCameraFollow(GameObject follow)
    {
        cvc = GetComponent<CinemachineVirtualCamera>();
        if (!cvc) 
        { 
            return; 
        }

        cvc.Follow = follow.transform;
    }

    private void LateUpdate()
    {
        if (!musicStarted &&
            FindObjectOfType<AudioManager>())
        {
            musicStarted = true;
            FindObjectOfType<AudioManager>().PlayBGM(audioToPlay);
        }
    }

    public int GetAudioToPlay()
    {
        return audioToPlay;
    }
}
