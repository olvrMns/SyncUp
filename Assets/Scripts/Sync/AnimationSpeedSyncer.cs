using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedSyncer : MonoBehaviour
{

    private Animator animator;
    private AnimationSync animationSync;

    void Start()
    {
        if (animationSync == null) animationSync = GameObject.Find("AnimationSpeedSync").GetComponent<AnimationSync>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.speed = animationSync.AnimationSpeed;
    }
}
