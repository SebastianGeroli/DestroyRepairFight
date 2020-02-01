﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 3f;
	Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}
	public void Move(Vector2 input)
	{
		Vector3 input3 = new Vector3(input.x, rb.velocity.y, input.y);
		rb.velocity = input3 * speed;
		transform.LookAt(input3 + transform.position);
	}
	// Update is called once per frame
	void Update()
	{

	}
}
