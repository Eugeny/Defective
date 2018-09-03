using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBrainsButton : MonoBehaviour
{
	public static UIBrainsButton instance;
	public RectTransform GUIRoot;
	SmoothFloat offset = new SmoothFloat (0.25f);
	public bool Visible;

	void Start ()
	{
		instance = this;
		offset.Force (1000);
	}

	void Update ()
	{
		offset.Smooth = Visible ? 0 : 1000;
		offset.Update ();
		GUIRoot.localPosition = new Vector3 (0, offset.Smooth, 0);
	}

	public void Toggle ()
	{
		Visible = !Visible;
	}
}
