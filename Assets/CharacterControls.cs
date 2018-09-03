using UnityEngine;
using System.Collections;

public class CharacterControls : MonoBehaviour
{
	public static CharacterControls instance;
	public float Speed = 2;
	private CharacterController controller;
	private SmoothVector inputSpeed = new SmoothVector (0.1f);
	private SmoothAngle direction = new SmoothAngle (0.1f);
	public bool EnableMotion = true;
	public Animator Animator;
	public bool Awake = false;

	void Start ()
	{
		instance = this;
		controller = GetComponent<CharacterController> ();
		direction.Force (transform.eulerAngles.y);
	}

	void Update ()
	{
		if (!EnableMotion)
			return;
		
		var input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		if (input.sqrMagnitude > 0)
			input.Normalize ();
		if (Input.GetKey (KeyCode.LeftShift))
			input *= 2;

		input = Quaternion.AngleAxis (CameraControls.instance.transform.eulerAngles.y, Vector3.up) * input;
		inputSpeed.Smooth = input;
		inputSpeed.Update ();

		if (input.magnitude > 0.01f) {
			direction.Smooth = Vector3.forward.AngleTo (inputSpeed.Smooth);
		}

		transform.eulerAngles = new Vector3 (0, direction.Smooth, 0);
		direction.Update ();

		Animator.SetFloat ("WalkSpeed", input.magnitude);
		Animator.SetBool ("Awake", Awake);

		controller.SimpleMove (inputSpeed.Smooth * Speed);
	}

	float pushPower = 10.0f;
	float weight = 6.0f;

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		var body = hit.collider.attachedRigidbody;
		Vector3 force;

		// no rigidbody
		if (body == null || body.isKinematic) {
			return;
		}

		// We use gravity and weight to push things down, we use
		// our velocity and push power to push things other directions
		if (hit.moveDirection.y < -0.3) {
			force = new Vector3 (0.0f, -0.5f, 0.0f) * 0.9f * weight;
		} else {
			force = hit.controller.velocity * pushPower;
		}

		// Apply the push
		body.AddForceAtPosition (force, hit.point);
	}
}
