using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    public List<ParticleSystem> particleFX;
    public AudioClip audioFX;

    public void PlayParticleFX()
    {
        foreach (ParticleSystem fx in particleFX)
        {
            fx.Play();
            GetComponent<AudioSource>().PlayOneShot(audioFX);
        }
    }
}
