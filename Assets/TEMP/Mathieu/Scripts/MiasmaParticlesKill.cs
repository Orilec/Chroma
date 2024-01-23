using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmaParticlesKill : MonoBehaviour
{

    private ParticleSystem particleS;
    private List<ParticleSystem.Particle> particlesEnter = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> particlesExit = new List<ParticleSystem.Particle>();


    InteractorScript[] interactors;

    private List<SphereCollider> killColliders; 

    private void Awake()
    {
        particleS = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        killColliders = new List<SphereCollider>(); 

        FindInteractors(); 
        SetTriggers(); 
    }

    // Update is called once per frame
    void Update()
    {
        FindInteractors();
        SetTriggers();
    }

    private void FindInteractors()
    {
        interactors = FindObjectsOfType<InteractorScript>();
    }

    private void SetTriggers()
    {
        foreach (InteractorScript interactor in interactors)
        {
            SphereCollider sphereCollider = interactor.SphereCollider;

            if (!killColliders.Contains(sphereCollider))
            {
                particleS.trigger.AddCollider(interactor.SphereCollider); //Add to trigger colliders list in Particle System
                killColliders.Add(sphereCollider); //Add to gameobject list
            }
        }
        
    }

    private void OnParticleTrigger()
    {
        int numEnter = particleS.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesEnter);
        int numExit = particleS.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, particlesExit);

        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle particle = particlesEnter[i];
            particle.startColor = new Color(particle.startColor.r, particle.startColor.g, particle.startColor.b, 0f); //Alpha to 0
            
            particlesEnter[i] = particle; 

        }

        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle particle = particlesExit[i];
            particle.remainingLifetime = particlesExit[i].startLifetime;
            particle.startColor = new Color(particle.startColor.r, particle.startColor.g, particle.startColor.b, 1f); 
            particlesExit[i] = particle;

        }


        particleS.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particlesEnter);
        particleS.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, particlesExit);
    }
}
