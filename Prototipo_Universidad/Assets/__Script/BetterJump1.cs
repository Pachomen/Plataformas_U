using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump1 : MonoBehaviour
{
    public float fallMultiplier;
    public float lowJumpMultiplier;
    Rigidbody2D rb;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && (!Input.GetButton("Jump_2")) )
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }
}
