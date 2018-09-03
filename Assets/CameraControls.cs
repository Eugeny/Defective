using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour
{
	internal static CameraControls instance;

	public Transform Target;
	public float Zoom = 5f, Angle = 0f;
	private Vector3 offsetClose = new Vector3 (0, 6f, -2f);
	private Vector3 offsetFar = new Vector3 (0, 22f, -5f);

	private SmoothVector smoothTargetPosition = new SmoothVector (.2f);
	private SmoothVector offset = new SmoothVector (.2f);
	private SmoothFloat extraZoom = new SmoothFloat (.2f);
	private SmoothAngle angle = new SmoothAngle (.1f);

	private Vector3 lastMousePosition;

	void Start() {
		instance = this;
	}

	void Update ()
	{
		smoothTargetPosition.Smooth = Target.position;
		offset.Smooth = Vector3.Lerp (offsetClose, offsetFar, Zoom / 10f);
		extraZoom.Smooth = Input.GetKey (KeyCode.Tab) ? 2 : 1;
		angle.Smooth = Angle;

		smoothTargetPosition.Update ();
		offset.Update ();
		extraZoom.Update ();
		angle.Update ();

		var rot = Quaternion.Euler (0, angle.Smooth, 0);
		var targetPosition = smoothTargetPosition.Smooth + rot * offset.Smooth * extraZoom.Smooth;
		var targetRotation = rot * Quaternion.Euler (Mathf.Rad2Deg * Mathf.Atan2 (offset.Smooth.y, 2 - offset.Smooth.z), 0, 0);

		transform.position = targetPosition;
		transform.rotation = targetRotation;

		Zoom -= Input.mouseScrollDelta.y;
		Zoom = Mathf.Clamp (Zoom, 0, 3);

		if (Input.GetMouseButton (0) || Input.GetMouseButton (1)) {
			float d = (Input.mousePosition.x - lastMousePosition.x) / 360;
			if (d > .15f)
				Debug.Log (d);
			if (d < .1f)
				Angle += d * 180;
			Angle %= 360;
		}
		lastMousePosition = Input.mousePosition;
	}
		
}
