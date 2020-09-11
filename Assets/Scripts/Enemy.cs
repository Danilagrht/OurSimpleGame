using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public float JumpForce = 300f;
    public bool facingRight = true;

    private float spawnX,spawnY;
    private DateTime t = DateTime.Now;
    private bool _isGrounded;
    private bool isPlatformed;
    private Rigidbody2D _rb;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        spawnX = transform.position.x;
        spawnY = transform.position.y;
    }


    void FixedUpdate()
    {
        //JumpLogic();

        MovementLogic();
    }

    private void MovementLogic()
    {
        Vector2 movement = new Vector3(Speed, 0f);

        int countFacingRight;

        if(facingRight) countFacingRight = 1;
        else countFacingRight = -1;

        transform.Translate(movement * Time.fixedDeltaTime * countFacingRight);
        //_rb.AddForce(Vector2.right*movement*Speed*Time.fixedDeltaTime);
    }

    /*private void JumpLogic()
    {
        if (_isGrounded)
        {
            if ((Input.GetAxis("Vertical") > 0)||(Input.GetAxis("Jump") > 0))
            {
                _rb.AddForce(Vector2.up * JumpForce);
            }
        }
    }*/

    private void Flip(){
    	facingRight = !facingRight;
    	Vector3 theScale = transform.localScale;
    	theScale.x *= -1;
    	transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.tag == "DieBox")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Flip();
        }
	}
    void OnCollisionStay2D(Collision2D collision)
    {
        IsGroundedUpate(collision, true);
        if(collision.gameObject.tag == ("Platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        IsGroundedUpate(collision, false);
        if(collision.gameObject.tag == ("Platform"))
        {
            this.transform.parent = null;
        }
    }

    
    private void IsGroundedUpate(Collision2D collision, bool value)
    {
        if ((collision.gameObject.tag == ("Ground"))||(collision.gameObject.tag == ("Platform")))
        {
            _isGrounded = value;
        }
    }
}
