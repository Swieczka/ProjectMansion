using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D _rb;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementSpeedx;
    [SerializeField] private float _movementSpeedy;
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _maxMoveSpeedInit;
    [SerializeField] private float _groundLinearDrag;
    [SerializeField] private float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0 && _horizontalDirection < 0f) || (_rb.velocity.x < 0 && _horizontalDirection > 0f);
    
    [Header("Sliding & Wall Sliding Variables")]
    [SerializeField] private float _slideSpeed = 10f;
    [SerializeField] private bool _slideClicked;
    [SerializeField] private bool _isWallSliding;
    [SerializeField] private float _wallSlidingDrag;
    [SerializeField] private bool _canSlide => Input.GetButton("Slide") && _onGround;

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    private bool _canJump => Input.GetButtonDown("Jump") && ( _onGround || _extraJumpsValue > 0);
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f; 
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private int _extraJumps = 1;
    private int _extraJumpsValue;

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private bool _onGround;
    

    [Header("Colliders")]
    [SerializeField] Collider2D _walkCollider;
    [SerializeField] Collider2D _slideCollider;

    void Start()
    {
        _isWallSliding = false;
        _slideClicked = false;
        _maxMoveSpeedInit = _maxMoveSpeed;
        _slideSpeed += _maxMoveSpeed;
        _rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalDirection = GetInput().x;
        if (_canJump)
        {
            Jump();
        }
        Slide();
        WallSlide();
        _movementSpeedx = _rb.velocity.x;
        _movementSpeedy = _rb.velocity.y;
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    private void FixedUpdate()
    {
        CheckCollisions();
        MovePlayer();
        if (_onGround)
        {
            _extraJumpsValue = _extraJumps;
        }
        ApplyDrag();

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
            _rb.drag = 1f;
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
    private void Slide()
    {
        if (_canSlide)
        {
            
            _maxMoveSpeed = _slideSpeed;
            _rb.AddForce(new Vector2(_horizontalDirection, 0f) * (_slideSpeed / 2));
            _slideCollider.enabled = true;
            _walkCollider.enabled = false;
            GetComponent<SpriteRenderer>().color = Color.white;
            if(!_slideClicked)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                _slideClicked = true;
            }
            
            transform.localScale = new Vector3(1f, 1f, 1f);
            
        }
        else
        {
            Vector3 left_check_spot = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.1f, transform.position.z);
            Vector3 right_check_spot = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.1f, transform.position.z);
            bool _left_check = Physics2D.Raycast(left_check_spot, Vector2.up, _groundRaycastLength, _groundLayer);
            bool _right_check = Physics2D.Raycast(right_check_spot, Vector2.up, _groundRaycastLength, _groundLayer);
            if (!(_left_check || _right_check) && _onGround)
            {
                _maxMoveSpeed = _maxMoveSpeedInit;
                _slideCollider.enabled = false;
                _walkCollider.enabled = true;
                GetComponent<SpriteRenderer>().color = Color.green;
                transform.localScale = new Vector3(1f, 2f, 1f);
                _slideClicked = false;
            }
            
        }
        
    }
    private void CheckCollisions()
    {
        Vector3 left_check_spot = new Vector3(transform.position.x - 0.5f, transform.position.y-0.5f, transform.position.z);
        Vector3 right_check_spot = new Vector3(transform.position.x + 0.5f, transform.position.y-0.5f, transform.position.z);
        bool _left_check = Physics2D.Raycast(left_check_spot, Vector2.down, _groundRaycastLength, _groundLayer);
        bool _right_check = Physics2D.Raycast(right_check_spot, Vector2.down, _groundRaycastLength, _groundLayer);
        _onGround = _left_check || _right_check;
       // _onGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRaycastLength, _groundLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 left_check = new Vector3(transform.position.x - 0.5f, transform.position.y-0.5f, transform.position.z);
        Vector3 right_check = new Vector3(transform.position.x+0.5f, transform.position.y-0.5f, transform.position.z);
        Gizmos.DrawLine(right_check, right_check + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(left_check, left_check + Vector3.down * _groundRaycastLength);
        Gizmos.color = Color.red;
        Vector3 left_slide_check = new Vector3(transform.position.x - 0.5f, transform.position.y - 0.1f, transform.position.z);
        Vector3 right_slide_check = new Vector3(transform.position.x + 0.5f, transform.position.y - 0.1f, transform.position.z);
        Gizmos.DrawLine(left_slide_check, left_slide_check + Vector3.up * _groundRaycastLength);
        Gizmos.DrawLine(right_slide_check, right_slide_check + Vector3.up * _groundRaycastLength);
        Gizmos.color = Color.magenta;
        Vector3 left_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 right_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 left_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        Vector3 right_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        Gizmos.DrawLine(left_wallslide_checkdown, left_wallslide_checkdown + Vector3.left * (_groundRaycastLength/1.5f));
        Gizmos.DrawLine(right_wallslide_checkdown, right_wallslide_checkdown + Vector3.right * (_groundRaycastLength / 1.5f));
        Gizmos.DrawLine(left_wallslide_check, left_wallslide_check + Vector3.left * (_groundRaycastLength / 1.5f));
        Gizmos.DrawLine(right_wallslide_check, right_wallslide_check + Vector3.right * (_groundRaycastLength / 1.5f));
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

    private void WallSlide()
    {
        Vector3 left_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 right_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 left_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        Vector3 right_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        bool _left_check_up = Physics2D.Raycast(left_wallslide_check, Vector2.left, _groundRaycastLength, _groundLayer);
        bool _right_check_up = Physics2D.Raycast(right_wallslide_check, Vector2.right, _groundRaycastLength, _groundLayer);
        bool _left_check_down = Physics2D.Raycast(left_wallslide_checkdown, Vector2.left, _groundRaycastLength, _groundLayer);
        bool _right_check_down = Physics2D.Raycast(right_wallslide_checkdown, Vector2.right, _groundRaycastLength, _groundLayer);
        if(!_onGround && _rb.velocity.y < 0 && (_left_check_up || _right_check_up || _left_check_down || _right_check_down))
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private void ApplyDrag()
    {
         if(_isWallSliding)
         {
             _rb.drag = _wallSlidingDrag;
         }
         else
        if (_onGround)
        {
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Spikes")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
