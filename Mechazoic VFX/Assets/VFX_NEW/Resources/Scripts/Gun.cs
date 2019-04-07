using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    private GunFire gunFire;
    private bool canFire = true;

    public enum GunType { Default, DoubleDamage, Stun, ArmorPiercing, MegaLaser };
    [Header("Current Gun")]
    public GunType currentGun = GunType.Default;


    [Space(10)]
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 100;
    public float bulletLifetime = 3;
    public float fireRatePerSecond = 2;

    public ParticleDecalPool pool;


    private void Start()
    {
        if (bulletSpawnPoint == null)
            bulletSpawnPoint = transform;

        gunFire = GetComponent<GunFire>();

        if (bulletSpawnPoint == null)
            Debug.LogError("bulletSpawnPoint not found on " + gameObject.name + "!");

        if (bulletPrefab == null)
            Debug.LogError("bulletPrefab not found on " + gameObject.name + "!");
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }


    private void Fire()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && canFire)
        {
            GameObject bullet = Instantiate(bulletPrefab) as GameObject;

            bullet.transform.position = bulletSpawnPoint.position;
            bullet.transform.rotation = bulletSpawnPoint.rotation;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            else
                Debug.LogWarning("Rigidbody not found on " + bullet.name);

            //  Audio
            PlayAudio(bullet);

            //  Fire effect
            FireVFX(bullet);

            Destroy(bullet, bulletLifetime);

            canFire = false;

            StartCoroutine(FireRateReset());
        }
    }

    private IEnumerator FireRateReset()
    {
        yield return new WaitForSeconds(1 / fireRatePerSecond);
        canFire = true;
    }

    private void PlayAudio(GameObject bullet)
    {
        AudioSource[] sources = bullet.GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            AudioSource tempSource = CustomPlayAtOneShot.PlayClipAt(source.clip, transform.position, source.volume);
            tempSource.pitch = source.pitch;
        }
    }

    private void FireVFX(GameObject bullet)
    {
        gunFire.FireVFX();

        //Bullet bulletScript = bullet.GetComponent<Bullet>();
        //if (bulletScript != null)
        //{
        //    if (pool != null)
        //        bulletScript.pool = pool;
        //}
    }
}
