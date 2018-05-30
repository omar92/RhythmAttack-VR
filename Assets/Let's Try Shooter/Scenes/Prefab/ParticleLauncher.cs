using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem particlesLauncher;
    public ParticleSystem splatterParticles;
    public Gradient particleColorGradient;
    List<ParticleCollisionEvent> collisionEvents;

    void Start () {

        collisionEvents = new List<ParticleCollisionEvent>();

    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particlesLauncher, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count ; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
        
    }
    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticles.transform.position = particleCollisionEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        ParticleSystem.MainModule psMain = splatterParticles.main;
        psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f, 1f));
        splatterParticles.Emit(1);
    }
    
    void Update () {
        if (Input.GetButton("Fire1"))
        {
            ParticleSystem.MainModule psMain = particlesLauncher.main;
            //psMain.startColor = particleColorGradient.Evaluate(Random.Range(0f,1f));
            particlesLauncher.Emit(1);
        }
        

    }
}
