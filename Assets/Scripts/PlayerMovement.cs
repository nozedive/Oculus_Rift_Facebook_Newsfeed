using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	private float moveSpeed = 5;

	private Vector3 input;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		input = new Vector3( Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		rigidbody.AddForce(moveSpeed*input);
	}
}
