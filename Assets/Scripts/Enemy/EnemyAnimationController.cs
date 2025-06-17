using System;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private AnimancerComponent animancer;
    [SerializeField] private AnimationClip idleAnimation;
    [SerializeField] private AnimationClip walkAnimation;
    [SerializeField] private ClipTransition attackAnimation; //So we could specify when to give damage
    [SerializeField] private AnimationClip winAnimation;
    private void Awake()
    {
        TryGetComponent(out animancer);
    }




    public void PlayIdleAnimation()
    {
        if (animancer != null)
        {
            animancer.Play(idleAnimation);
        }
    }

    public void PlayWalkAnimation()
    {
        if (animancer != null)
        {
            //if walkAnimation already playing, we don't want to play it again
            if (animancer.IsPlaying(walkAnimation))
                return;
            animancer.Play(walkAnimation);
        }
    }

    public void PlayAttackAnimation()
    {
        if (animancer != null)
        {
            //if attackAnimation already playing, we don't want to play it again
            if (animancer.IsPlaying(attackAnimation))
                return;

            var anim = animancer.Play(attackAnimation);
            anim.Events(this).OnEnd = PlayIdleAnimation;
        }
    }
    public void PlayEnemyWinAnimation()
    {
        if (animancer != null)
        {
            if (animancer.IsPlaying(winAnimation))
                return;
            animancer.Play(winAnimation,1f);
        }
    }
}