using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalArmBehavior : MonoBehaviour
{
    public LineRenderer laser;

    AudioSource laserAudioSource;

    void Start()
    {
        if (laser != null)
        {
            laserAudioSource = laser.gameObject.GetComponent<AudioSource>();
        }
    }
    public void ActivateLaser()
    {
        laser.enabled = true;

        if (laserAudioSource != null)
        {
            laserAudioSource.enabled = true;
        }
    }
}
