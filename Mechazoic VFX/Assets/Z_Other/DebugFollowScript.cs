using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFollowScript : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    //private Vector3 oldPos = Vector3.positiveInfinity;

    public Transform follow;

    private void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        ToggleParticleSystemEmission(false);
    }

    //private void Update()
    //{
    //    if (follow != null)
    //    {
    //        if (follow.transform.position != oldPos)
    //        {
    //            ToggleParticleSystemEmission(true);
    //            //transform.position = follow.transform.position;
    //            //transform.rotation = follow.transform.rotation;
    //            oldPos = transform.position;
    //        }
    //        else
    //        {
    //            ToggleParticleSystemEmission(false);
    //        }
    //    }
    //}


    public void ToggleParticleSystemEmission(bool _value)
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ParticleSystem.EmissionModule emission = ps.emission;
            emission.enabled = _value;
        }
    }
}
