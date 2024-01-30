using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiasmaHolder : MonoBehaviour
{

    [SerializeField] private ParticleSystem miasmaSpawnerParticleSystem;
    private ParticleSystem.Particle[] spawnedParticles;
   

    [SerializeField] private int numberOfMiasmaParticles; //Set number of max particles on Miasma Holder


    // Start is called before the first frame update
    void Start()
    {
        spawnedParticles = new ParticleSystem.Particle[miasmaSpawnerParticleSystem.main.maxParticles];

    }

    // Update is called once per frame
    void Update()
    {

        int nbSpawnedParticlesAlive = miasmaSpawnerParticleSystem.GetParticles(spawnedParticles);



        for (int i = 0; i < nbSpawnedParticlesAlive; i++)
        {
            if (spawnedParticles[i].remainingLifetime <= spawnedParticles[i].startLifetime / 10 && spawnedParticles[i].remainingLifetime != 0) //if remaining life time is under a value, but if == 0, the particle is supposed to be killed (in miasmaParticleKill)
            {
                spawnedParticles[i].remainingLifetime = spawnedParticles[i].startLifetime * 0.9f;
            }
        }

        // Apply the particle changes to the Particle System
        miasmaSpawnerParticleSystem.SetParticles(spawnedParticles, nbSpawnedParticlesAlive);



    }

    private void OnValidate()
    {
        //Just to update in EditorMode the number of particles spawning on MiasmaHolder. Does not change in play mode
        var main = miasmaSpawnerParticleSystem.main;
        main.maxParticles = numberOfMiasmaParticles;
    }
}