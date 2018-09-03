using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
	public static UIFade instance;
	private SmoothFloat alpha = new SmoothFloat (1);
	public bool Enable;

	void Start ()
	{
		instance = this;
		alpha.Force (1);
	}

	void Update ()
	{
		alpha.Smooth = Enable ? 1 : 0;
		alpha.Update ();
		GetComponent<Image> ().color = new Color (0, 0, 0, alpha.Smooth);
	}
}
