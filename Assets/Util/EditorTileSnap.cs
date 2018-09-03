using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EditorTileSnap : MonoBehaviour
{
	void Update ()
	{
		transform.localPosition = new Vector3 (
			Mathf.Round (transform.localPosition.x),
			transform.localPosition.y,
			Mathf.Round (transform.localPosition.z)
		);
	}
}
