using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
public class Square : MonoBehaviour
{
    public float Speed = 5f;
    public int death = 0;
    public float JumpForce = 300f;
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
        JumpLogic();

        MovementLogic();
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector3(moveHorizontal, 0f);

        transform.Translate(movement * Speed * Time.fixedDeltaTime);
        //_rb.AddForce(Vector2.right*movement*Speed*Time.fixedDeltaTime);
    }

    private void JumpLogic()
    {
        if (_isGrounded)
        {
            if ((Input.GetAxis("Vertical") > 0)||(Input.GetAxis("Jump") > 0))
            {
                _rb.AddForce(Vector2.up * JumpForce);
            }
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.tag == "DieBox") 
        {
            transform.position = new Vector3(spawnX,spawnY,transform.position.z);
            death++;
        }
		
		if (collision.gameObject.tag == "EndLevel") SceneManager.LoadScene("Level_2");
	}
    void OnCollisionStay2D(Collision2D collision)
    {
        IsGroundedUpate(collision, true);
        if(collision.gameObject.tag == ("Platform"))
        {
            this.transform.parent = collision.transform;
        }
        if(collision.gameObject.tag == ("CheckPoint"))
        {
            spawnX = transform.position.x;
            spawnY = transform.position.y;
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

    void OnGUI()
    {
    	GUI.Box (new Rect(0,0,100,20),"Death : " + death);
    }
}
