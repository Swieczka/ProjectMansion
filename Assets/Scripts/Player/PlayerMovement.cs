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
    [SerializeField] private float _movementAcceleration;
    [SerializeField] private float _movementBuff;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _maxMoveSpeedInit;
    [SerializeField] private float _groundLinearDrag;
    [SerializeField] private float _groundLinearDragTemp = 0;
    [SerializeField] private float _horizontalDirection;
    [SerializeField] private float _verticalDirection;
    [SerializeField] private bool _facingRight = true;
    private bool _changingDirection => (_rb.velocity.x > 0 && _horizontalDirection < 0f) || (_rb.velocity.x < 0 && _horizontalDirection > 0f);

    [Header("Crouching & Wall Sliding Variables")]
    private bool isCrouching;
    [SerializeField] private float _crouchSpeed = 6f;
    [SerializeField] private bool _isWallSliding;
    [SerializeField] private float _wallSlidingDrag;
    [SerializeField] private bool _canCrouch => Input.GetButton("Crouch") && _onGround;
    public bool _Head;

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    private bool _canJump => (Input.GetButtonDown("Jump") && ( _onGround || _extraJumpsValue > 0)) && _JumpRes && _MoveRes;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f; 
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    public int _extraJumps = 1;
    public int _extraJumpsValue;

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    [SerializeField] private bool _onGround;

    [Header("Dash Variables")]
    [SerializeField] float _dashSpeed = 15f;
    [SerializeField] float _dashLength = 0.3f;
    [SerializeField] float _dashBufferLength = 0.1f;
    [SerializeField] private float _dashBufferCounter;
    [SerializeField] bool _isDashing;
    [SerializeField] bool _hasDashed;
    bool _canDash =>  (_dashBufferCounter > 0f && !_hasDashed) && _DashRes && _MoveRes;


    [Header("Colliders")]
    [SerializeField] Collider2D _walkCollider;
    [SerializeField] Collider2D _slideCollider;

    [Header("Restrictions")]
    public bool _MoveRes = true;
    public bool _JumpRes = true;
    public bool _DashRes = true;
    public bool _DoubleJumpRes = true;

    GameManager gameManager;
    Animator animator;
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        _isWallSliding = false;
        _maxMoveSpeedInit = _maxMoveSpeed;
        _rb= GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontalDirection = GetInput().x;
        _verticalDirection = GetInput().y;
        
        if(Input.GetButtonDown("Dash"))
        {
            _dashBufferCounter = _dashBufferLength;
        } 
        else
        {
            _dashBufferCounter -= Time.deltaTime;
            
        }
        if (_canJump)
        {
            Jump();
        }
        WallSlide();
        if ((_horizontalDirection < 0f && _facingRight || _horizontalDirection > 0f && !_facingRight))
        {
            SwitchDirection();
        }
        if(_MoveRes)
        {
            Animation();
        }
        
    }

    private void FixedUpdate()
    {
        
        CheckCollisions();
        if(_canDash)
        {
            StartCoroutine(Dash(_horizontalDirection));
        }
        if(!_isDashing && _MoveRes)
        {
            
            MovePlayer();
            Crouch();
            if (_onGround && _DoubleJumpRes)
            {
                _extraJumpsValue = _extraJumps;
            }
            ApplyDrag();
        }
       

    }
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Horizontal"));
    }
    private void MovePlayer()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f)*_movementAcceleration*_movementBuff);
        if(_rb.velocity.x < 0.1f && _rb.velocity.x > -0.1f)
        {
            _rb.velocity = new Vector2(0,_rb.velocity.y);
        }
        if (_rb.velocity.y < 0.1f && _rb.velocity.y > -0.1f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
        }
        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed*_movementBuff)
        {
            _rb.velocity=new Vector2(Mathf.Sign(_rb.velocity.x)*_maxMoveSpeed*_movementBuff,_rb.velocity.y);
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
        animator.Play("Base Layer.Jump Start");
    }
    private void Crouch()
    {
        if (_canCrouch)
        {
            isCrouching = true;
            _maxMoveSpeed = _crouchSpeed;
            _slideCollider.enabled = true;
            _walkCollider.enabled = false;  
        }
        else
        {
            Vector3 left_check_spot = new Vector3(transform.position.x - 0.3f, transform.position.y -0.1f, transform.position.z);
            Vector3 right_check_spot = new Vector3(transform.position.x + 0.3f, transform.position.y -0.1f, transform.position.z);
            Vector3 up_crouch_check = new Vector3(transform.position.x - 0.4f, transform.position.y + 1f, transform.position.z);
            bool _left_check = Physics2D.Raycast(left_check_spot, Vector2.up, _groundRaycastLength*1.3f, _groundLayer);
            bool _right_check = Physics2D.Raycast(right_check_spot, Vector2.up, _groundRaycastLength*1.3f, _groundLayer);
            bool _up_check = Physics2D.Raycast(up_crouch_check, Vector2.right, _groundRaycastLength, _groundLayer);
            if(_up_check)
            {
                _Head = true;
            }
            if (!(_left_check || _right_check || _up_check))
            {
                isCrouching = false;
                _maxMoveSpeed = _maxMoveSpeedInit;
                _slideCollider.enabled = false;
                _walkCollider.enabled = true;
                _Head = false;
            }
            
        }
        
    }
    IEnumerator Dash(float x)
    {
        float dashStartTime = Time.time;
        _hasDashed = true;
        _isDashing = true;

        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0f;
        _rb.drag = 0f;
        if(_onGround)
        {
            isCrouching = true;
            _slideCollider.enabled = true;
            _walkCollider.enabled = false;
        }
        Vector2 dir;
        if(x!=0f)
        {
            dir = new Vector2(x, 0f);
        }
        else
        {
            if(_facingRight) dir = new Vector2(1f, 0f);
            else dir = new Vector2(-1f, 0f);
        }
        while(Time.time < dashStartTime + _dashLength)
        {
            _rb.velocity = dir.normalized * _dashSpeed;
            yield return null;
        }
        _isDashing = false;
        yield return new WaitForSeconds(1f);
        _hasDashed = false;
    }
    private void CheckCollisions()
    {
        Vector3 left_check_spot = new Vector3(transform.position.x - 0.3f, transform.position.y-0.5f, transform.position.z);
        Vector3 right_check_spot = new Vector3(transform.position.x + 0.3f, transform.position.y-0.5f, transform.position.z);
        bool _left_check = Physics2D.Raycast(left_check_spot, Vector2.down, _groundRaycastLength, _groundLayer);
        bool _right_check = Physics2D.Raycast(right_check_spot, Vector2.down, _groundRaycastLength, _groundLayer);
        _onGround = _left_check || _right_check;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 left_check = new Vector3(transform.position.x - 0.3f, transform.position.y-0.5f, transform.position.z);
        Vector3 right_check = new Vector3(transform.position.x + 0.3f, transform.position.y-0.5f, transform.position.z);
        Gizmos.DrawLine(right_check, right_check + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(left_check, left_check + Vector3.down * _groundRaycastLength);
        Gizmos.color = Color.red;
        Vector3 left_slide_check = new Vector3(transform.position.x - 0.3f, transform.position.y -0.1f , transform.position.z);
        Vector3 right_slide_check = new Vector3(transform.position.x + 0.3f, transform.position.y -0.1f, transform.position.z);
        Gizmos.DrawLine(left_slide_check, left_slide_check + Vector3.up * _groundRaycastLength*1.3f);
        Gizmos.DrawLine(right_slide_check, right_slide_check + Vector3.up * _groundRaycastLength*1.3f);
        Vector3 up_crouch_check = new Vector3(transform.position.x - 0.4f, transform.position.y+1f, transform.position.z);
        Gizmos.DrawLine(up_crouch_check, up_crouch_check + Vector3.right * _groundRaycastLength);
        Vector3 left_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 right_wallslide_check = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 left_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        Vector3 right_wallslide_checkdown = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(left_wallslide_check, left_wallslide_check + Vector3.left * (_groundRaycastLength/1.9f));
        Gizmos.DrawLine(right_wallslide_check, right_wallslide_check + Vector3.right * (_groundRaycastLength / 1.9f));
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(left_wallslide_checkdown, left_wallslide_checkdown + Vector3.left * (_groundRaycastLength / 1.9f));
        Gizmos.DrawLine(right_wallslide_checkdown, right_wallslide_checkdown + Vector3.right * (_groundRaycastLength / 1.9f));
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
        bool _left_check_up = Physics2D.Raycast(left_wallslide_check, Vector2.left, _groundRaycastLength/1.9f, _groundLayer);
        bool _right_check_up = Physics2D.Raycast(right_wallslide_check, Vector2.right, _groundRaycastLength/1.9f, _groundLayer);
        bool _left_check_down = Physics2D.Raycast(left_wallslide_checkdown, Vector2.left, _groundRaycastLength/1.9f, _groundLayer);
        bool _right_check_down = Physics2D.Raycast(right_wallslide_checkdown, Vector2.right, _groundRaycastLength/1.9f, _groundLayer);
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
        else if (_onGround)
        {
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            
        }
        FallMultiplier();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Spikes")
        {
            gameManager.LoadGame();
        }
        if(collision.collider.gameObject.tag == "JumpWall")
        {
            _extraJumpsValue = _extraJumps;
        }
        if(collision.collider.gameObject.tag == "PushingEnemy")
        {
            _dashBufferCounter = _dashBufferLength;
            StartCoroutine(Dash(collision.collider.gameObject.GetComponent<Pushing>().facingLeft*-1));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Web")
        {
            _extraJumps = 0;
            _extraJumpsValue = 0;
            _movementBuff = 0.5f;
        }
        if(collision.gameObject.tag =="Spikes")
        {
            gameManager.LoadGame();
        }
        if(collision.gameObject.tag == "Icy")
        {
            _groundLinearDragTemp = _groundLinearDrag;
            _groundLinearDrag = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "IcyDead")
        {
            _extraJumps = 0;
            _extraJumpsValue = 0;
            _movementBuff -= 0.01f;
            GetComponent<SpriteRenderer>().color -= new Color(0.02f, 0.01f,0,0);
            if (_movementBuff <= 0f)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
                _movementBuff = 1;
                gameManager.LoadGame();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Web")
        {
            _extraJumps = 1;
            _movementBuff = 1f;
        }
        if (collision.gameObject.tag == "Icy")
        {
            _groundLinearDrag = _groundLinearDragTemp;
          
        }
        if (collision.gameObject.tag == "IcyDead")
        {
                GetComponent<SpriteRenderer>().color = Color.white;
                _movementBuff = 1;
        }

    }
    private void SwitchDirection()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Animation()
    {
        if(_canJump)
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", true);
            animator.Play("Base Layer.Jump Start"); 
        }
        else
        {
            animator.SetBool("isJumping", false);
            if (_rb.velocity.y >=0)
            {
                animator.SetBool("isFalling", false);
            }
            if (_rb.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
            }
        }
        if (_onGround)
        {
            if(isCrouching)
            {
                animator.SetBool("isCrouching", true);
            }
            else
            {
                animator.SetBool("isCrouching", false);
            }
            animator.SetBool("isLanding", true);
            animator.SetBool("isFalling", false);
            if (_horizontalDirection!=0)
            {
                animator.SetBool("isRunning", true);
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            animator.SetBool("isLanding", false);
        }
    }
}
