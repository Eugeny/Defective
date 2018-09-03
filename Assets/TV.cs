using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour {
	public Transform Display;

	public void Enable() {
		Display.gameObject.SetActive (true);
	}
}
