using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionDecal : MonoBehaviour
{
    public Bullet bullet;

    public ParticleSystem particleSystemCollision;
    public List<ParticleCollisionEvent> collisionEvents;

    //public ParticleSystem splatterParticles;
    public ParticleDecalPool splatDecalPool;


    private void Start()
    {
        Time.timeScale = 1f;

        particleSystemCollision = GetComponent<ParticleSystem>();
        particleSystemCollision.Emit(1);

        collisionEvents = new List<ParticleCollisionEvent>();

        //splatDecalPool = ParticleDecalPool.instance;
        //bullet = GetComponentInParent<Bullet>();
        //if (bullet != null)
        //{
        //    splatDecalPool = bullet.pool;
        //}
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(particleSystemCollision, other, collisionEvents);

        for (int i = 0; i < collisionEvents.Count; i++)
        {
            if (splatDecalPool != null)
                splatDecalPool.ParticleHit(collisionEvents[i]);
            //ParticleDecalPool.instance.ParticleHit(collisionEvents[i]);
            //EmitAtLocation(collisionEvents[i]);
        }
        Destroy(bullet.gameObject);
    }

    //private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    //{
    //    //splatterParticles.transform.position = particleCollisionEvent.intersection;
    //    //splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
    //    //ParticleSystem.MainModule psMain = splatterParticles.main;

    //    //splatterParticles.Emit(10);
    //}
}
