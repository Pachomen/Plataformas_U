using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    Rigidbody2D rb;
    Animator myAnimator;
    //Movement
    public float movement = 0f;
    public float speed = 10f;
    public float smoothVelocity = 0.05f;
    Vector3 V3velocity = Vector3.zero;
    private bool controllerAct = true;

    //Flip
    private bool facingRight;

    //Jump
    private bool jump;
    public float jumpForce;
    //Ground
    public float groundedSkin = 0.05f;
    public LayerMask mask;
    private bool isGrounded;
    Vector2 playerSize;
    Vector2 boxSize;

    void Awake() {
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);
    }

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        facingRight = true;
        
    }

    // Update is called once per frame
    void Update() {
        movement = Input.GetAxis("Horizontal") * speed;
        if (!controllerAct) movement = 0;
        HandleInput();
    }

    void FixedUpdate() {
        //Grounded
        //Flip
        Flip(movement);
        //Movement
        HandleMovement(movement);

        HandleLayers();
        
        //Reset_Values
        resetValues();


    }

    void HandleInput() {
        if (Input.GetButtonDown("Jump") && isGrounded) {
            jump = true;
        }
    }

    void HandleMovement(float movement) {

        //rb.velocity = new Vector2(movement, rb.velocity.y);
        //Smooth movement - smoothVelocity Makes smother the movement
        Vector3 velocity = new Vector2(movement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocity, ref V3velocity, smoothVelocity);

        if (jump) {
            
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAnimator.SetTrigger("Jump_Parameter");
            isGrounded = false;
        } else {

            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            isGrounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
            myAnimator.SetBool("Land_Parameter", true);
        }

        myAnimator.SetFloat("Speed_Patameter", Mathf.Abs(movement));

    }

    void Flip(float movement) {
        if (movement > 0 && !facingRight || movement < 0 && facingRight) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = !facingRight;
        }
    }

    void HandleLayers() {
        if (!isGrounded && controllerAct) {
            
            myAnimator.SetLayerWeight(1, 1);
        } else if(isGrounded && controllerAct){
            myAnimator.SetLayerWeight(1, 0);
        }
        else if (!controllerAct) {
            myAnimator.SetLayerWeight(2, 1);
        }
    }

    void enemyJump() {
        jump = true;
    }
    void EnemyKnockBack(float enemyPosX) {
        jump = true;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
        GetComponent<Rigidbody2D>().AddForce(Vector2.left * side * jumpForce, ForceMode2D.Impulse);
        controllerAct = false;
        Invoke("ControllerActive", 0.4f);
    }

    void ControllerActive() {
        controllerAct = true;
        myAnimator.SetLayerWeight(2, 0);
    }
    void resetValues() {
        jump = false;
    }
}
