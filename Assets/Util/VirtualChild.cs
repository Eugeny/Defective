using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VirtualChild : MonoBehaviour
{
	public Transform Parent;

	void Update ()
	{
		transform.position = Parent.position;
		transform.rotation = Parent.rotation;
		transform.localScale = Parent.localScale;
	}
}
