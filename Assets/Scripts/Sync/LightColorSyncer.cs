using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorSyncer : MonoBehaviour
{
    private Light _light;
    public ColorSync ColorSync;

    void Start()
    {
        _light = GetComponent<Light>();
        if (ColorSync == null) ColorSync = GameObject.Find("ColorSyncLight").GetComponent<ColorSync>();
    }

    void Update()
    {
        _light.color = ColorSync.CurrentColor;
    }
}
