using System;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{

    private GameObject player;
    private Transform enemyTransform;
    public float ShootingCooldownInSeconds = 4f;
    private Coroutine cooldownCoroutine = null;
    public event EventHandler BeforeFiring;
    public event EventHandler AfterFiring;
    public bool CanFire = false;
    public bool OnCooldown = false;
    public bool GunWasFired = false;
    private Ray trajectoryRay;
    private RaycastHit hit;
    public GameObject BaseBulletModelPrefab;
    public GameObject MultiBulletsPrefab;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        enemyTransform = GetComponent<Transform>();
        BeforeFiring += (object sender, EventArgs e) =>
        {
            Debug.Log("Fire");
            GunWasFired = true;
        };

        AfterFiring += (object sender, EventArgs e) =>
        {
            GunWasFired = false;
            GoOnCooldown();
            
        };
    }

    private void GoOnCooldown()
    {
        if (cooldownCoroutine == null) 
        {
            OnCooldown = true;
            cooldownCoroutine = StartCoroutine(TimingController.Time(TimeType.SCALEDTIME, ShootingCooldownInSeconds, () =>
            {
                OnCooldown = false;
                cooldownCoroutine = null;
            }));
        }
    }

    private void UpdateBulletDirection()
    {
        Vector3 heightenedPosition = new Vector3(
            enemyTransform.position.x, 
            enemyTransform.position.y + 1.2f, 
            enemyTransform.position.z);
        if (Physics.Raycast(heightenedPosition, player.transform.position, out hit) && CanFire) 
        {
            Debug.DrawRay(heightenedPosition, player.transform.position, Color.green);
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                Debug.Log("traj");
                trajectoryRay = new Ray(enemyTransform.position, hit.point);
            }
        }
    }

    public void Fire()
    {
        BeforeFiring?.Invoke(this, EventArgs.Empty);
        InstantiateSoundBullet();
        AfterFiring?.Invoke(this, EventArgs.Empty);
    }

    private void InstantiateSoundBullet()
    {
        if (CanFire && !OnCooldown)
        {
            GameObject _gameObject = Instantiate(MultiBulletsPrefab, enemyTransform.transform.position, Quaternion.Euler(Vector3.zero));
            _gameObject.AddComponent<BulletDamage>();
            SoundBullet soundBullet = _gameObject.GetComponent<SoundBullet>();
            soundBullet.BaseBulletModel = BaseBulletModelPrefab;
            soundBullet.Ray = trajectoryRay;
            soundBullet.InstantiateElements();
            soundBullet.StartTravelling();
        }
    }

    private void Update()
    {
        UpdateBulletDirection();
    }
}
