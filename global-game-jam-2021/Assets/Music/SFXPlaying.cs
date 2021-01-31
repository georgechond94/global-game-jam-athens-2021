using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlaying : MonoBehaviour
{
    public AudioSource Footstep;
    public AudioSource Magpie_Sing;
    public AudioSource Magpie_Wings;
    public AudioSource TsokaroShot;
    public AudioSource Soundtrack;

    public void PlayFootstep()
    {
        Footstep.Play();
    
    }
    public void PlaySoundtrack()
    {
        Soundtrack.Play();

    }

    public void PlayMagpieSings()
    {
        Magpie_Sing.Play();
    }

    public void PlayMagpie_Wings()
    {
        Magpie_Wings.Play();
    }

    public void PlayTsokaroShot()
    {
        TsokaroShot.Play();
    }

   void OnTriggerEnter()
    {
        Footstep.Play();
    }
}