using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDecalPool : MonoBehaviour
{
    public float offset = 2;
    public int maxDecals = 100;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;

    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private VFXDecalData[] particleData;
    private ParticleSystem.Particle[] particles;
    public Gradient colorGradient;

    private void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new VFXDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new VFXDecalData();
        }
    }

    public void ParticleHit(Collision coll)
    {
        SetParticleData(coll);
        DisplayParticles();
    }

    public void SetData(Collision coll)
    {
        print(coll);
        SetParticleData(coll);
        DisplayParticles();
    }

    private void SetParticleData(Collision coll)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        particleData[particleDecalDataIndex].position = coll.collider.ClosestPointOnBounds(coll.contacts[0].point) + (coll.contacts[0].normal * offset);
        Vector3 particleRotationEuler = Quaternion.LookRotation(coll.contacts[0].normal).eulerAngles;
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;
        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);
        particleData[particleDecalDataIndex].color = colorGradient.Evaluate(Random.Range(0f, 1f));

        particleDecalDataIndex++;
    }

    private void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
            particles[i].startColor = particleData[i].color;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
