using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using UnityEngine;

public class Platform : MonoBehaviour {

	public float speed = 3f;
	public int direction = 1;
	private DateTime t = DateTime.Now;
	public Vector2 currentPlatformSpeed;
	void Start () 
	{
	}
	
	void FixedUpdate () 
	{
		MovementLogic();
	}

	private void MovementLogic()
	{
		Vector2 movement = new Vector2(speed, 0f);
		Vector2 currentPlatformSpeed = movement*direction*Time.fixedDeltaTime;
		transform.Translate(currentPlatformSpeed);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if((collision.gameObject.tag == ("Wall"))||(collision.gameObject.tag == ("DieBox")))
		{
			if ((DateTime.Now - t).TotalMilliseconds >= 100)
            {
				direction *= -1;
				t = DateTime.Now;
            }
		}
	}


}