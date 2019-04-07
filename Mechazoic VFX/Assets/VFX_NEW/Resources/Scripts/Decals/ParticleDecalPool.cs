using UnityEngine;

public class ParticleDecalPool : MonoBehaviour
{
    public static ParticleDecalPool instance = null;
    public int maxDecals = 100;
    public float decalSizeMin = .5f;
    public float decalSizeMax = 1.5f;
    public float offset = 0;

    private ParticleSystem decalParticleSystem;
    private int particleDecalDataIndex;
    private ParticleDecalData[] particleData;
    private ParticleSystem.Particle[] particles;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        decalParticleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[maxDecals];
        particleData = new ParticleDecalData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            particleData[i] = new ParticleDecalData();
        }
    }


    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent)
    {
        SetParticleData(particleCollisionEvent);

        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Collider>().enabled = false;
        sphere.transform.localScale = Vector3.one * .5f;
        sphere.transform.position = particleCollisionEvent.intersection + (-particleCollisionEvent.normal * offset);

        DisplayParticles();
    }

    private void SetParticleData(ParticleCollisionEvent particleCollisionEvent)
    {
        if (particleDecalDataIndex >= maxDecals)
        {
            particleDecalDataIndex = 0;
        }

        print(particleDecalDataIndex); /////////

        particleData[particleDecalDataIndex].position = particleCollisionEvent.intersection + (-particleCollisionEvent.normal * offset);

        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        particleData[particleDecalDataIndex].rotation = particleRotationEuler;

        particleData[particleDecalDataIndex].size = Random.Range(decalSizeMin, decalSizeMax);

        particleDecalDataIndex++;
    }

    private void DisplayParticles()
    {
        for (int i = 0; i < particleData.Length; i++)
        {
            particles[i].position = particleData[i].position;
            particles[i].rotation3D = particleData[i].rotation;
            particles[i].startSize = particleData[i].size;
        }

        decalParticleSystem.SetParticles(particles, particles.Length);
    }
}
