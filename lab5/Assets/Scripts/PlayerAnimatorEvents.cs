using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
   [SerializeField] private PlayerAudioController audioController;

    public void PlayWalkSound()
    {
        audioController.PlayWalkSound();
    } 
}
