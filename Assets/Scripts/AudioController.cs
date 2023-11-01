using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMusic()
    {
        int index = Random.Range(0, audioClips.Length);
        playerAudio.PlayOneShot(audioClips[index]);
    }
}
