﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ChariotController : MonoBehaviour {
	public int m_PlayerNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
	public float m_Speed = 12f;                 // How fast the tank moves forward and back.
	public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
	//public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
	public bool hasPassenger;
	public Transform pickUpPoint;
	private Rigidbody m_Rigidbody;              // Reference used to move the tank.
	private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
	private string m_TurnAxisName;              // The name of the input axis for turning.
	private float m_MovementInputValue;         // The current value of the movement input.
	private float m_TurnInputValue;             // The current value of the turn input.

	private void Awake ()
	{
		m_Rigidbody = GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
		// The axes names are based on player number.
		m_MovementAxisName = "Vertical";// + m_PlayerNumber;
		m_TurnAxisName = "Horizontal";//+ m_PlayerNumber;
	}
	void FixedUpdate()
	{
		Move ();
		Turn ();
	}
	// Update is called once per frame
	void Update () {
		// Store the value of both input axes.
		m_MovementInputValue = CrossPlatformInputManager.GetAxis (m_MovementAxisName);
		m_TurnInputValue = CrossPlatformInputManager.GetAxis  (m_TurnAxisName);
		if (Input.GetButtonDown ("Fire1")) {
			Move ();
		}
	}

	private void Move ()
	{
		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

		// Apply this movement to the rigidbody's position.
		m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
	}
	private void Turn ()
	{
		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

		// Apply this rotation to the rigidbody's rotation.
		m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
	}
}
