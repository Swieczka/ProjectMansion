using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _groundLinearDrag;
    private float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0 && _horizontalDirection < 0f) || (_rb.velocity.x < 0 && _horizontalDirection > 0f);

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private bool _canJump => Input.GetButtonDown("Jump") && ( _onGround || _extraJumpsValue > 0);
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f; 
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private int _extraJumps = 1;
    private int _extraJumpsValue;

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private bool _onGround;

    void Start()
    {
        _rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalDirection = GetInput().x;
     //   if (Input.GetButtonDown("Jump") && _onGround) _canJump = true;
        if (_canJump)
        {
            Jump();
        }
    }
    private void FixedUpdate()
    {
        
        CheckCollisions();
        MovePlayer();
        
        if(_onGround)
        {
            _extraJumpsValue = _extraJumps;
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
        
    }
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Horizontal"));
    }
    private void MovePlayer()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f)*_movementAcceleration);
        if(Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity=new Vector2(Mathf.Sign(_rb.velocity.x)*_maxMoveSpeed,_rb.velocity.y);
        }
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _groundLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }
    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }
    private void Jump()
    {
        if(!_onGround)
        {
            _extraJumpsValue--;
        }
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up*_jumpForce,ForceMode2D.Impulse);
    }
    private void CheckCollisions()
    {
        _onGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundRaycastLength);
    }

    private void FallMultiplier()
    {
        if(_rb.velocity.y <0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if(_rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }
}
