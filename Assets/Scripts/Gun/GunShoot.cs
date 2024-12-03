using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public KeyCode TriggerKey;
    public bool CanFire = true;
    public Camera playerCamera;
    public GameObject impactEffect;
    public bool isShooting = false;
    public ParticleSystem fireEffect;
    public float shootDamage = 10f;
    public float range = 100f;
    public Color rayColor = Color.red;
    public float shootingDelay = 0.3f;

    void Start()
    {
        fireEffect = GameObject.Find("Muzzle").GetComponent<ParticleSystem>();
        fireEffect.Stop();
        if (TriggerKey == KeyCode.None) TriggerKey = KeyCode.Mouse0;
    }

    void Update()
    {
        if (Input.GetKeyUp(TriggerKey) && CanFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        isShooting = true;
        RaycastHit hit;
        Vector3 startPosition = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        Debug.DrawRay(startPosition, direction * range, rayColor, 2f);

        if (Physics.Raycast(startPosition, direction, out hit, range))
        {
            if (hit.transform.TryGetComponent<EnemyDamage>(out var enemyDamage))
            {
                enemyDamage.Takedamage(shootDamage);
            }

            if (impactEffect != null)
            {
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 2f);
            }
            else
            {
                Debug.LogWarning("impactEffect n'est pas assigné !");
            }
        }
        fireEffect.Play();
        StartCoroutine(ResetShootingAfterDelay());
    }

    IEnumerator ResetShootingAfterDelay()
    {
        yield return new WaitForSeconds(shootingDelay);
        isShooting = false;
    }
}