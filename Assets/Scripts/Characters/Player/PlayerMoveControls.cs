using UnityEngine;
using System.Collections;
public class PlayerMoveControls : MonoBehaviour
{
	public float speed;
	public float jumpForce;
    public float maxJumpTime;
    private float _jumpTimer;
    private bool _jumpActive;
    private GatherInput _gI;
    private Rigidbody2D _rb;
    private Animator _anim;
    private int _lookDirection = 1;
    public int additionalJumps;
    private int _startAdditionalJumps;
    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool _grounded;
    public bool knockBack;
    public bool hasControl = true;
    [Header("Ladder")]
    public bool onLadders;
    public float climbSpeed;
    public float climbHorizontalSpeed;
    private float _startGravity;
    [Header("Wall Slide")]
    public Transform wallSlidePoint;
    public float wallSlideSpeed;
    private bool _wallSlide;
    [Header("Wall Jump")]
    public float wallJumpTimer;
    private bool _wallJumpActive;
    private bool _wallJumpOneTime;
    private float _startWallJumpTimer;
    private int _wallJumpDir;
    public float walljumpForceX;
    public float wallJumpFOrceY;
    private RaycastHit2D _wallSlideHit;
    [Header("Crouch")]
    public bool crouch;
    private bool _forceCrouch;
    private bool _headCheck;
    public Transform headCheckPos;
    public float headCheckRadius;
    public CapsuleCollider2D standColl;
    public PolygonCollider2D standStatsColl;
    public BoxCollider2D crouchColl;
    public BoxCollider2D crouchStatColl;
    private PlayerAttackControls _pAC;

    public float crouchSpeed;

    private void Start() {
        _gI = GetComponent<GatherInput>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _pAC = GetComponent<PlayerAttackControls>();

        _startGravity = _rb.gravityScale;
        _startAdditionalJumps = additionalJumps;
        _startWallJumpTimer = wallJumpTimer;
    }

    private void Update() {
        SetAnimatorValues();
        CrouchPlayerConditions();
        JumpPlayerConditions();
    }

    private void FixedUpdate() {
        CheckStatus();
        if (knockBack || hasControl == false)
            return;
        Move();
        WallSlide();
        Jump();
        WallJump();
    }

    private void Move() {
        if (_wallJumpActive)
            return;
        Flip();
        if (crouch == false)
            _rb.velocity = new Vector2(speed * _gI.valueX, _rb.velocity.y);
        else if (crouch == true)
            _rb.velocity = new Vector2(crouchSpeed * _gI.valueX, _rb.velocity.y);
        if (onLadders) {
            _rb.gravityScale = 0.0f;
            _rb.velocity = new Vector2(climbHorizontalSpeed * _gI.valueX, climbSpeed * _gI.valueY);
            if (_rb.velocity.y == 0)
                _anim.enabled = false;
            else
                _anim.enabled = true;
        }
    }

    public void ExitLadders() {
        _rb.gravityScale = _startGravity;
        onLadders = false;
        _anim.enabled = true;
    }

    private void Jump() {
        if (_jumpActive)
            _rb.velocity = new Vector2(_gI.valueX * speed, jumpForce);
    }
    private void JumpPlayerConditions() {
        if (_gI.jumpInput) {
            if ((_grounded || onLadders) && _jumpActive == false) {
                _jumpActive = true;
                _jumpTimer = maxJumpTime;
                ExitLadders();
            }
            else if (_jumpActive == false && additionalJumps > 0 && _wallSlideHit == false) {
                _jumpActive = true;
                additionalJumps -= 1;
                _jumpTimer = maxJumpTime;
            }
            else if (_jumpActive == false && _wallJumpOneTime == false && _wallSlideHit) {
                _wallJumpActive = true;
                _wallJumpOneTime = true;
                _wallJumpDir = (-1 * _lookDirection);
                wallJumpTimer = _startWallJumpTimer;
                ForceFlip();
            }
        }
        else {
            _jumpActive = false;
            _wallJumpOneTime = false;
        }

        if (_jumpActive == true) 
            _jumpTimer -= Time.deltaTime;
        if (_jumpTimer <= 0) {
            _jumpActive = false;
            _gI.jumpInput = false;
        }

        if (_wallJumpActive == true)
            wallJumpTimer -= Time.deltaTime;
        if (wallJumpTimer <= 0) {
            _wallJumpActive = false;
        }

        if (_grounded || onLadders) 
            additionalJumps = _startAdditionalJumps;
    }

    private void CrouchPlayerConditions() {
        if (_gI.tryToCrouch) {
            if (_grounded && _pAC.attackStarted == false && onLadders == false) {
                crouch = true;
                standColl.enabled = false;
                standStatsColl.enabled = false;
                crouchColl.enabled = true;
                crouchStatColl.enabled = true;
            }
        }
        else if (_forceCrouch == false) {
            crouch = false;
            crouchColl.enabled = false;
            crouchStatColl.enabled = false;
            standColl.enabled = true;
            standStatsColl.enabled = true;
        }
    }
    private void WallJump() {
        if (_wallJumpActive)
            _rb.velocity = new Vector2(_wallJumpDir * walljumpForceX, wallJumpFOrceY);
    }

    private void CheckStatus() {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        if (leftCheckHit || rightCheckHit) {
            _grounded = true;
        }
        else {
            _grounded = false;
            crouch = false;
        }

        _headCheck = Physics2D.OverlapCircle(headCheckPos.position, headCheckRadius, groundLayer);
        if (_headCheck && _grounded) 
            _forceCrouch = true;
        else
            _forceCrouch = false;

        _wallSlideHit = Physics2D.Raycast(wallSlidePoint.position, _lookDirection * Vector2.right, 0.1f, groundLayer);
        if (_wallSlideHit && !_grounded) {
            if (_gI.tryToWallSlide) 
                _wallSlide = true;
            else
                _wallSlide = false;
        }
        else 
            _wallSlide = false;

        SeeRays(leftCheckHit, rightCheckHit);
    }

    private void WallSlide() {
        if (_wallSlide) 
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlideSpeed, 5));
    }

    private void SeeRays(RaycastHit2D leftCheckHit, RaycastHit2D rightCheckHit) {
        Color color1 = leftCheckHit ? Color.red : Color.green;
        Color color2 = rightCheckHit ? Color.red : Color.green;
        Debug.DrawRay(leftPoint.position, Vector2.down * rayLength, color1);
        Debug.DrawRay(rightPoint.position, Vector2.down * rayLength, color2);
    }

    private void Flip() {
        if (_gI.valueX * _lookDirection < 0) {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            _lookDirection *= -1;
        }
    }

    private void ForceFlip() {
        transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
        _lookDirection *= -1;
    }

    private void SetAnimatorValues() {
        _anim.SetFloat("Speed", Mathf.Abs(_rb.velocity.x));
        _anim.SetFloat("vSpeed", _rb.velocity.y);
        _anim.SetBool("Grounded", _grounded);
        _anim.SetBool("Climb", onLadders);
        _anim.SetBool("Crouch", crouch);
    }

    public IEnumerator KnockBackCO(float forceX, float forceY, float duration, Transform otherObject) {
        int knockBackDirection;
        if (transform.position.x <= otherObject.position.x) 
            knockBackDirection = -1;
        else
            knockBackDirection = 1;
        knockBack = true;
        _rb.velocity = Vector2.zero;
        Vector2 theForce = new Vector2(knockBackDirection * forceX, forceY);
        _rb.AddForce(theForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        knockBack = false;
        _rb.velocity = Vector2.zero;
    }
}
