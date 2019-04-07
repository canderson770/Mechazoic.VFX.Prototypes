using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool beingDestroyed = false;
    public ParticleCollisionDecal particleCollision;

    public bool destroyOnCollision = true;
    public float offset = .5f;
    [Range(0, 1)] public float threshold = .6f;

    [Header("Fire")]
    public GameObject fireEffect;
    public float fireVFXDuration = 2;

    [Header("Hit")]
    public GameObject hitEffect;
    public float hitVFXDuration = 3;


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.layer != LayerMask.NameToLayer("MyPlayer"))
        {
            if (destroyOnCollision && !beingDestroyed)
            {
                beingDestroyed = true;

                if (hitEffect != null)
                    SpawnHitEffect(coll);

                Destroy(gameObject, .1f);
            }
        }
    }

    private void SpawnHitEffect(Collision coll)
    {
        float newOffset = 0;

        if (Mathf.Abs(coll.contacts[0].normal.y) > threshold)
            newOffset = offset / 2;
        else
            newOffset = offset;

        GameObject hit = Instantiate(hitEffect, coll.contacts[0].point + (coll.contacts[0].normal * newOffset), transform.rotation);
        Destroy(hit, hitVFXDuration);
    }
}
