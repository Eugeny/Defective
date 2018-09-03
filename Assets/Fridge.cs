using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Activable
{
	public ActionObject CakeAO;

	public override void ActivateObject ()
	{
		GetComponent<Animator> ().SetBool ("Open", true);
		CakeAO.gameObject.SetActive (true);
	}
}
