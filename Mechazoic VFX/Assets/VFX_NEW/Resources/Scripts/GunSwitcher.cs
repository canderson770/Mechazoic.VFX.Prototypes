using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    private Gun gun;
    private GunFire gunFire;
    private int index = 0;


    public KeyCode key = KeyCode.Alpha1;
    public Gun.GunType currentGun = Gun.GunType.Default;
    public GameObject[] bulletPrefabs;


    private void Start()
    {
        gun = GetComponent<Gun>();
        gunFire = GetComponent<GunFire>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            index = (index + 1) % bulletPrefabs.Length;
            currentGun = (Gun.GunType)(index % 4);
            gun.currentGun = gunFire.currentGun = currentGun;
            gun.bulletPrefab = bulletPrefabs[index];
        }
    }
}
