using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private enum MovementState {idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;


    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();

    }

    private void UpdateAnimationState ()
    {
        MovementState state;
      
        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }


        if(rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < - 0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);
    }

    private bool IsGrounded()
    {
        var groundState = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        return groundState;
    }
}
