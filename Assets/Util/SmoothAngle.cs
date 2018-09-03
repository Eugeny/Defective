using UnityEngine;
using System.Collections;

public class SmoothAngle : SmoothFloat
{
	public SmoothAngle (float speed) : base (speed)
	{
	}

	public override void Update ()
	{
		value = Mathf.SmoothDampAngle (value, target, ref speed, Time);
	}
}
