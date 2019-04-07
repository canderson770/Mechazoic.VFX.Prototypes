using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDecalControl : MonoBehaviour
{
    public ParticleSystem decalSpawner;


    private void EmitAtLocation(Transform transform)
    {
        decalSpawner.Emit(1);
    }
}
