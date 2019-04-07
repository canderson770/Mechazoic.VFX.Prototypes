using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour
{
    public ParticleSystem particleLauncher;
    public Gradient particleColorGradient;
    public ParticleDecalPool dropletDecalPool;

    private List<ParticleCollisionEvent> collisionEvents;


    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(particleLauncher, other, collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            dropletDecalPool.ParticleHit(collisionEvents[i]);
            i++;
        }
    }
}
