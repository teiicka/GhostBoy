using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Parametars")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [Header ("Cayote Time")]
    [SerializeField] private float coyoteTime;
    [Header ("Wall Jumping")]
    [SerializeField] private float wallJumpX; //horizontal walljump force
    [SerializeField] private float wallJumpY; //vertical walljump force
    private float coyoteCounter;
    [Header ("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private float wallJumpCooldown;
    private float horizontalInput;
    [Header ("SFX")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update(){

        horizontalInput = Input.GetAxis("Horizontal");


        //Flip player when moving left/right
        if(horizontalInput > 0.01f){
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //animator
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Jump
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }
        //Adjustable jump height
        if(Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0){
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }
        if(onWall()){
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else{
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if(isGrounded()){
                coyoteCounter = coyoteTime; //reset coyote counter when on the ground
            }
            else{
                coyoteCounter -= Time.deltaTime; //stars decreasing coyote counter when not on the ground
            }
        }
    }

    private void Jump(){
        if(coyoteCounter < 0 && !onWall()) return;

        SoundManager.instance.PlaySound(jumpSound);

        if(onWall()){
            WallJump();
        }
        else{
            if(isGrounded()){
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
            else{
                if(coyoteCounter > 0){
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                }
            }
            coyoteCounter = 0;
        }
    }

    private void WallJump(){
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack(){
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
