using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{

    public ColorSync ColorSync;
    public Vector3 Speed = new Vector3(0f, 0f, 100f);
    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule module;
    private ParticleSystem.TrailModule trailModule;
    private ParticleSystem.ShapeModule shapeModule;
    private RotationSpeedSync rotationSpeedSync;


    void Start()
    {
        rotationSpeedSync = GameObject.Find("RotationSpeed").GetComponent<RotationSpeedSync>();
        _particleSystem = GetComponent<ParticleSystem>();
        module = _particleSystem.main;
        trailModule = _particleSystem.trails;
        shapeModule = _particleSystem.shape;
    }

    void Update()
    {
        //module.startColor = new ParticleSystem.MinMaxGradient(ColorSync.CurrentColor);
        transform.Rotate(Time.deltaTime * Speed * (rotationSpeedSync.RotationSpeed * 2f));
        trailModule.colorOverLifetime = ColorSync.CurrentColor;
    }
}
