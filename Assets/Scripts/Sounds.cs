using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip[] sounds;
    private AudioSource audioSrc => GetComponent<AudioSource>();

    public void PlaySound(AudioClip audioClip) 
    {
        audioSrc.PlayOneShot(audioClip, 0.5f);
    }
}
