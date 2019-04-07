using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public Gun.GunType currentGun = Gun.GunType.Default;
    public float fireVFXDuration = .5f;
    public List<GameObject> gunFireList;


    private void Awake()
    {
        foreach (GameObject fireEffect in gunFireList)
        {
            fireEffect.SetActive(false);
        }
    }


    public void FireVFX()
    {
        StartCoroutine(RunFireVFX());
    }

    private IEnumerator RunFireVFX()
    {
        int index = (int)currentGun;

        if (index > gunFireList.Count)
            index = 0;

        gunFireList[index].SetActive(true);

        yield return new WaitForSeconds(fireVFXDuration);

        gunFireList[index].SetActive(false);
    }
}
