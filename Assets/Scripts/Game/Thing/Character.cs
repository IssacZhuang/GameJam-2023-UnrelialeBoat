using System;
using System.Collections.Generic;
using UnityEngine;
using Vocore;

public class Character : BaseThing<CharacterConfig>
{
    private Func<float, float> _speedCurve;
    private bool _isPaused = false;
    public bool _hasKey = false;
    private float _t;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _velocity = Vector2.zero;
    private bool _isMoving = false;

    private static readonly Dictionary<KeyCode, Vector2> _keyMap = new Dictionary<KeyCode, Vector2>()
    {
        {KeyCode.W, Vector2.up},
        {KeyCode.S, Vector2.down},
        {KeyCode.A, Vector2.left},
        {KeyCode.D, Vector2.right},
        {KeyCode.UpArrow, Vector2.up},
        {KeyCode.DownArrow, Vector2.down},
        {KeyCode.LeftArrow, Vector2.left},
        {KeyCode.RightArrow, Vector2.right},
    };


    public override void OnCreate()
    {
        Vector4 controlPoint = Config.speedCurve;
        _speedCurve = UtilsCurve.GenerateBizerLerpCurve(controlPoint.x, controlPoint.y, controlPoint.z, controlPoint.w);

        _rigidbody = Instance.GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Character没有Rigidbody2D");
        }

        _animator = Instance.GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Character没有Animator");
        }

        OnStopMove();

    }

    public override void OnSpawn()
    {
        Current.MainCharacter = this;
        Current.CameraTrace.SetTarget(this.Instance.transform, true, true);
        this.BindEvent<bool>(EventCharacter.eventSetCharacterPaused, SetCharacterIsPasued);

        base.OnSpawn();
    }

    public override void OnUpdate()
    {
        if (!_isPaused){
            _animator.enabled = true;
            if (HasMovementKey())
            {
                _movementDirection = GetMovementDirection();
                _t += Time.deltaTime / Config.accelarateDuration;
                _t = Mathf.Clamp01(_t);

                if (!_isMoving)
                {
                    _isMoving = true;
                    OnStartMove();
                }
                else
                {
                    _animator.ResetTrigger("LeftRun");
                    _animator.ResetTrigger("RightRun");
                    _animator.ResetTrigger("UpRun");
                    _animator.ResetTrigger("DownRun");
                    _animator.SetTrigger(GetTriigerByDirection(_movementDirection));
                }
            }
            else
            {
                _t -= Time.deltaTime * Config.deaccelarateDuration;
                _t = Mathf.Clamp01(_t);
                _movementDirection = Vector2.zero;

                if (_isMoving)
                {
                    _isMoving = false;
                    OnStopMove();
                }
            }

            float speed = _speedCurve(_t) * Config.speed;
            _velocity = _movementDirection.normalized * speed;
        }else{
            _animator.enabled = false;
        }


    }

    public override void OnTick()
    {
        _rigidbody.velocity = _velocity;
        //Debug.Log(_t + "," + _rigidbody.velocity);
    }

    public void OnStartMove()
    {
        //Debug.Log("OnStartMove");
        _animator.ResetTrigger("Idle");
        _animator.SetTrigger(GetTriigerByDirection(_movementDirection));
    }

    public void OnStopMove()
    {
        //Debug.Log("OnStopMove");
        _animator.ResetTrigger("LeftRun");
        _animator.ResetTrigger("RightRun");
        _animator.ResetTrigger("UpRun");
        _animator.ResetTrigger("DownRun");
        _animator.SetTrigger("Idle");
    }

    public string GetTriigerByDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                return "RightRun";
            }
            else
            {
                return "LeftRun";
            }
        }
        else
        {
            if (direction.y > 0)
            {
                return "UpRun";
            }
            else
            {
                return "DownRun";
            }
        }
    }

    public bool HasMovementKey()
    {
        foreach (var pair in _keyMap)
        {
            if (Input.GetKey(pair.Key))
            {
                return true;
            }
        }
        return false;
    }

    public Vector2 GetMovementDirection()
    {
        Vector2 movementDirection = Vector2.zero;
        foreach (var pair in _keyMap)
        {
            if (Input.GetKey(pair.Key))
            {
                movementDirection += pair.Value;
            }
        }
        return movementDirection;
    }

    private void SetCharacterIsPasued(bool flag){
        _isPaused = flag;
    }

    public void SetHasKey(bool flag){
        _hasKey = flag;
    }
    public bool GetHasKey(){
        return _hasKey;
    }
}
