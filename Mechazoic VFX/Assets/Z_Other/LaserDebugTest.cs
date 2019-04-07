using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDebugTest : MonoBehaviour
{
    private Animator anim;

    public Camera cam;

    public GameObject glowStartVFX;
    public GameObject glowEndVFX;
    public GameObject followVFX;
    public ParticleSystem ps;

    public ParticleSystem[] debrisParticlesSystems;

    public GameObject firePoint;

    public LineRenderer[] lines;

    public float maxLength;
    public float offset;


    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            foreach (LineRenderer line in lines)
            {
                line.SetPosition(0, firePoint.transform.position);
            }

            RaycastHit hit;

            Vector3 mousePos = Input.mousePosition;
            Ray rayMouse = cam.ScreenPointToRay(mousePos);

            if (Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maxLength))
            {
                if (hit.collider)
                {
                    Vector3 pos = hit.point + (hit.normal * offset);

                    Material hitMat = hit.collider.gameObject.GetComponent<Renderer>().material;
                    foreach (ParticleSystem ps in debrisParticlesSystems)
                    {
                        var renderer = ps.GetComponent<ParticleSystemRenderer>();
                        renderer.material = hitMat;
                    }

                    foreach (LineRenderer line in lines)
                    {
                        line.SetPosition(1, pos);
                    }

                    if (glowEndVFX != null)
                    {
                        glowEndVFX.transform.position = pos;
                        glowEndVFX.transform.LookAt(hit.point);
                    }
                    if (glowStartVFX != null)
                    {
                        glowStartVFX.transform.LookAt(glowEndVFX.transform);
                    }

                    if (followVFX != null)
                    {
                        followVFX.transform.position = pos;
                        Transform newTransform = transform;
                        newTransform.LookAt(hit.point);
                        //followVFX.transform.LookAt(hit.point);
                        var shape = ps.shape;
                        shape.rotation = newTransform.localEulerAngles;

                        followVFX.GetComponent<DebugFollowScript>().ToggleParticleSystemEmission(true);
                    }

                    anim.Play("MegaLaserOn");
                }
                else
                {
                    anim.Play("MegaLaserOff");
                }
            }
        }
        else
        {
            anim.Play("MegaLaserOff");
            followVFX.GetComponent<DebugFollowScript>().ToggleParticleSystemEmission(false);
        }
    }
}
