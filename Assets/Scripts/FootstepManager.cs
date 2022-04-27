using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class FootstepManager : MonoBehaviour
{
    public float stepRate = 0.5f;
    public List<AudioClip> stepSounds = new List<AudioClip>();

    float m_stepCooldown;
    float m_SprintMultiplier;
    AudioSource source;
    FirstPersonController controller;
    StarterAssetsInputs inputs;

    void Start()
    {
        source = GetComponent<AudioSource>();
        controller = GetComponent<FirstPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
        m_SprintMultiplier = controller.SprintSpeed / controller.MoveSpeed;
    }

    void Update()
    {
        m_stepCooldown -= (inputs.sprint ? Time.deltaTime * m_SprintMultiplier : Time.deltaTime);

        if (inputs.move != Vector2.zero && m_stepCooldown < 0f)
        {
            PlayStep();
            m_stepCooldown = stepRate;
        }
    }

    void PlayStep()
    {
        AudioClip clip = stepSounds[Random.Range(0, stepSounds.Count)];
        source.PlayOneShot(clip);
    }
}
