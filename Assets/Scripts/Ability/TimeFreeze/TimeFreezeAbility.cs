using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TimeFreeze : Ability
{

    private Camera _camera;
    public PostProcessProfile Profile;
    public float MaxVignetteIntensity;
    private PostProcessVolume PostProcessVolume;
    private AudioManager AudioManager;

    private event EventHandler UnFreezeEvent;
    private event EventHandler FreezeEvent;

    void Start()
    {
        if (TriggerKey == KeyCode.None) TriggerKey = KeyCode.T;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        AudioManager = AudioManager.Instance;
        PostProcessVolume = _camera.GetComponent<PostProcessVolume>();
        FreezeEvent += (object Sender, EventArgs e) => { AudioManager.Frozen = true; };
        UnFreezeEvent += (object Sender, EventArgs e) => { AudioManager.Frozen = false; };
    }

    private IEnumerator Freeze()
    {
        FreezeEvent?.Invoke(this, EventArgs.Empty);
        while (PostProcessVolume.weight <= 1)
        {
            PostProcessVolume.weight += 0.01f;
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
        StartCoroutine(TimingController.Time(TimeType.REALTIME, Duration, () => StartCoroutine(UnFreeze())));
    }

    private IEnumerator UnFreeze()
    {
        UnFreezeEvent?.Invoke(this, EventArgs.Empty);
        PostProcessVolume.weight = 1;
        while (PostProcessVolume.weight > 0)
        {
            PostProcessVolume.weight -= 0.01f;
            yield return new WaitForSeconds(0.1f * Time.deltaTime);
        }
        PostProcessVolume.weight = 0;
        GoOnCooldown();
    }

    void Update()
    {
        if (Input.GetKeyDown(TriggerKey))
            StartCoroutine(Freeze());
    }

}
