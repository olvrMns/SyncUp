using System;
using UnityEngine;

public class EnemyGunController : MonoBehaviour
{

    //private GameObject player;
    public Transform Muzzle;
    public float ShootingCooldownInSeconds = 4f;
    private Coroutine cooldownCoroutine = null;
    public event EventHandler BeforeFiring;
    public event EventHandler AfterFiring;
    public bool CanFire = false;
    public bool OnCooldown = false;
    public bool GunWasFired = false;
    public bool OnPlayer = false;
    private Ray trajectoryRay;
    private RaycastHit hit;
    public GameObject BaseBulletModelPrefab;
    public GameObject MultiBulletsPrefab;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = AudioManager.Instance;
        //player = GameObject.Find("PlayerArms");
        BeforeFiring += (object sender, EventArgs e) =>
        {
            GunWasFired = true; 
        };

        AfterFiring += (object sender, EventArgs e) =>
        {
            if (GunWasFired) GoOnCooldown();
            GunWasFired = false;
            
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
        if (Physics.Raycast(Muzzle.position, Muzzle.forward, out hit, float.MaxValue) && CanFire) 
        {
            //Debug.DrawRay(Muzzle.position, player.transform.position, Color.green);
            trajectoryRay = new Ray(Muzzle.position, hit.point);
            if (hit.transform.gameObject.CompareTag("Player"))
                OnPlayer = true;
             else 
                OnPlayer = false;
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
        if (CanFire && !OnCooldown && !audioManager.Frozen)
        {
            Debug.Log("OUI");
            GameObject _gameObject = Instantiate(MultiBulletsPrefab, Muzzle.position, Quaternion.Euler(Vector3.zero));
            _gameObject.AddComponent<BulletDamage>();
            SoundBullet soundBullet = _gameObject.GetComponent<SoundBullet>();
            soundBullet.Direction = Muzzle.forward;
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
