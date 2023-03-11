using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;  
    private Animator animator;
    private float directionX = 0;
    private float directionY = 0;

    private SpriteRenderer sprite;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpForce = 17f;

    private enum MovementState {
        IDLE,   // 0
        RUNNING,// 1
        JUMPING,// 2
        FALLING,// 3
    }

    private MovementState state;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        directionX = Input.GetAxisRaw("Horizontal");  // this name we too get from the InputManager
                                                            // raw to have movement stop immediately
        // if (directionX > 0) // we moved right
        // if (directionX < 0) // we moved left
        rigidBody.velocity = new Vector2(directionX * moveSpeed, rigidBody.velocity.y); // vector 2 because we are in 2D

        // if (Input.GetKeyDown("space")) // we can look it up by key, and hardcode the specific key "space" OR
        if(Input.GetButtonDown("Jump")) //  we can use it by the name of the button in File > Project Settings > InputManager  
        {
            rigidBody.velocity = new Vector3(0, jumpForce, 0);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (directionX > 0f) {
            state = MovementState.RUNNING;
            sprite.flipX = false;
        }
        else if (directionX < 0f)  // left
        { 
            state = MovementState.RUNNING;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.IDLE;
        }

        if (rigidBody.velocity.y > .1f) // even if we're not jumping, it's never quite 0, so we compare with .1
        {
            state = MovementState.JUMPING;
        } 
        else if(rigidBody.velocity.y < -.1f)
        {
            state = MovementState.FALLING;
        }
        animator.SetInteger("state", (int)state);
    }
}
