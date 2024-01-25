using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiasmaParticlesKill : MonoBehaviour
{

    private ParticleSystem particleS;
    [SerializeField] private ParticleSystem particleDestination; 

    private List<ParticleSystem.Particle> particlesEnter = new List<ParticleSystem.Particle>();
    private List<ParticleSystem.Particle> particlesExit = new List<ParticleSystem.Particle>();


    InteractorScript[] interactors;

    private List<CapsuleCollider> killColliders; 

    private void Awake()
    {
        particleS = GetComponent<ParticleSystem>();

    }

    private void Start()
    {
        killColliders = new List<CapsuleCollider>(); 

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

        for (int i = 0; i < interactors.Length; i++)
        {
            CapsuleCollider capsuleCollider = interactors[i].CapsuleCollider;

            if (!killColliders.Contains(capsuleCollider))
            {
                particleS.trigger.AddCollider(capsuleCollider);  
                killColliders.Add(capsuleCollider); //Add to gameobject list
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

            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
            {
                position = particle.position,
                velocity = particle.velocity,
                startColor = new Color(particle.startColor.r, particle.startColor.g, particle.startColor.b, 0f),
                startSize = particle.startSize,
                startLifetime = particle.remainingLifetime,
            };

            particle.remainingLifetime = 0;
            particlesEnter[i] = particle;


            particleDestination.Emit(emitParams, 1);

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

    private void TransferParticles(List<ParticleSystem.Particle> transferredParticles)
    {
        // Acc�dez aux particules du syst�me source
        List<ParticleSystem.Particle> sourceParticles = transferredParticles;
        int numParticles = transferredParticles.Count;

        // �mettez de nouvelles particules dans le syst�me de destination
        particleDestination.Emit(numParticles);

        // Acc�dez aux particules nouvellement �mises dans le syst�me de destination
        ParticleSystem.Particle[] destinationParticles = new ParticleSystem.Particle[particleDestination.particleCount];
        numParticles = particleDestination.GetParticles(destinationParticles);

        // Transf�rez les propri�t�s n�cessaires
        for (int i = 0; i < numParticles; i++)
        {
            // Exemple de transfert de la position et de la couleur
            destinationParticles[i].position = sourceParticles[i].position;
            destinationParticles[i].startColor = sourceParticles[i].startColor;
        }

        // Appliquez les modifications aux particules du syst�me de destination
        particleDestination.SetParticles(destinationParticles, numParticles);
    }
}
