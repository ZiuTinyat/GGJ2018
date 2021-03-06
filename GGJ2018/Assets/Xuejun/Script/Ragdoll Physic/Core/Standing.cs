﻿using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class Standing : MonoBehaviour
{
    // Head and Hip
    public List<RigidbodyMovement> m_rigidsToLift;         

    [BoxGroup("Stand Force")]
    public AnimationCurve m_getUpCurve;

    [BoxGroup("Stand Force")]
    public float m_standForceMutiplier;

    [BoxGroup("Stand Force")]
    public AnimationCurve m_standForceCurve;

    [BoxGroup("Down Force"), ReadOnly]
    public float m_gravity;

    [BoxGroup("Down Force")]
    public float m_gravityMutiplier;

    private CharacterInformation _characterInfo;

    private ConstantForce _leftKneeForce;

    private ConstantForce _rightKneeForce;

    private Controller _controller;

    private GrabHandler _grabHandler;

    private HealthHandler _healthHandler;

    private Rigidbody[] _rigs;

    private void Start()
    {
        _rigs = GetComponentsInChildren<Rigidbody>();
        _characterInfo = GetComponent<CharacterInformation>();
        _healthHandler = GetComponent<HealthHandler>();
        _controller = GetComponent<Controller>();
        _grabHandler = GetComponent<GrabHandler>();

        if (GetComponentInChildren<LeftKnee>() != null)
        {
            _leftKneeForce = GetComponentInChildren<LeftKnee>().GetComponent<ConstantForce>();
        }

        if (GetComponentInChildren<RightKnee>() != null)
        {
            _rightKneeForce = GetComponentInChildren<RightKnee>().GetComponent<ConstantForce>();
        }
    }        


    private void FixedUpdate()
    {    
        if (_leftKneeForce && _rightKneeForce)
        {
            if (_characterInfo.m_sinceGrounded < 0.15f && _characterInfo.m_sinceFallen > 0f)
            {
                _leftKneeForce.enabled = true;
                _rightKneeForce.enabled = true;
            }
            else
            {
                _leftKneeForce.enabled = false;
                _rightKneeForce.enabled = false;
            }
        }

        if (_characterInfo.m_isGround || _grabHandler.HoldSomethingAnchored)
        {
            m_gravity = 0f;
        }
        else
        {
            m_gravity += Time.fixedDeltaTime;
        }

        if (_characterInfo.m_sinceFallen > 0f)
        {
            ApplyGravity();
            if (_characterInfo.m_isGround)
            {
                Stand();
            }
        }
    }

    private void ApplyGravity()
    {
        foreach (var rb in _rigs)
        {
            if (!rb.CompareTag("IgnoreRigidbody"))
            {
                rb.AddForce(Vector3.down * Time.fixedDeltaTime * m_gravity * m_gravityMutiplier, ForceMode.Acceleration);
            }
        }
    }


    private void Stand()
    {
        float upStrength = m_standForceCurve.Evaluate(_characterInfo.ShortestDistanceFromHeadToGround ) * m_standForceMutiplier;

        if (_characterInfo.m_sinceFallen < 1f)
        {
            upStrength *= m_getUpCurve.Evaluate(_characterInfo.m_sinceFallen);
        }

        foreach (var rigToLift in m_rigidsToLift)
        {
            float accel = upStrength + rigToLift.m_rigidbody.velocity.magnitude * 100f * rigToLift.m_forceMutiplier * Time.fixedDeltaTime;
            rigToLift.m_rigidbody.AddForce(Vector2.up * accel, ForceMode.Force);
        }     
    }

}
