using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingMan : Activable{
	public override void ActivateObject ()
	{
		GetComponent<Animator> ().SetBool ("Poked", true);
	}
}
