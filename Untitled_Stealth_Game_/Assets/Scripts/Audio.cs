using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] public List<AudioClip> audioClips;
    AudioSource aSource;
   
    void Start()
    {
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(int clip_)
    {
        switch (clip_) {
            case 1:
                aSource.clip = audioClips[0];
                aSource.Play();
                break;
            case 2:
                aSource.clip = audioClips[1];
                aSource.Play();
                break;
            case 3:
                aSource.clip = audioClips[2];
                aSource.Play();
                break;
            case 4:
                aSource.clip = audioClips[3];
                aSource.Play();
                break;
            default:
                break;

        }
       
    }
}
