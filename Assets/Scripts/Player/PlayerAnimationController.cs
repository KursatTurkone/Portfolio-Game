using System;
using Animancer;
using Animancer.Units;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private Rigidbody Rigidbody;

    [SerializeField] private AnimationClip upperBodyIdle;
    [SerializeField] private ClipTransition upperBodyShoot;
    [SerializeField] private TransitionAssetBase _Transition;
    [SerializeField] private AvatarMask upperBodyMask;
    [SerializeField] private AvatarMask lowerBodyMask;
    [SerializeField] private StringAsset _ParameterX;
    [SerializeField] private StringAsset _ParameterY;
    [SerializeField, Seconds] private float _ParameterSmoothTime = 0.15f;

    private AnimancerLayer lowerLayer;
    private AnimancerLayer upperLayer;
    private SmoothedVector2Parameter _SmoothedParameters;
    private Vector2 localMovement;

    private void Start()
    {
        PlayerManager.Instance._playerAnimationController = this; 
        lowerLayer = animancer.Layers[0];
        upperLayer = animancer.Layers[1];
        
        upperLayer.Mask = upperBodyMask;
      
        lowerLayer.Mask = lowerBodyMask; 
        animancer.Play(_Transition);
        _SmoothedParameters = new SmoothedVector2Parameter(
            animancer,
            _ParameterX,
            _ParameterY,
            _ParameterSmoothTime);
        upperLayer.Play(upperBodyIdle);
    }

    private void Update()
    {
        GetLocalMovement();
    }

    public void GetLocalMovement()
    {
        Vector3 v = Rigidbody.velocity;
        Vector3 lv = transform.InverseTransformDirection(v);
        localMovement = new Vector2(lv.x, lv.z).normalized;
        localMovement *= Mathf.Clamp(v.magnitude, 0f, 1f);
        _SmoothedParameters.TargetValue = localMovement;
     
    }

    public void ShootAnimation()
    {
        var anim = upperLayer.Play(upperBodyShoot);

        anim.Events(this).OnEnd = () =>
        {
            upperLayer.Play(upperBodyIdle); 
        };
    }
    public void StopShootMovement()
    {
        if (upperLayer.IsPlayingClip(upperBodyShoot.Clip))
        {
            upperLayer.Stop();
        }
        upperLayer.Play(upperBodyIdle);
    }
}