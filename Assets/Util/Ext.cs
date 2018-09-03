using System;
using System.Collections;
using UnityEngine;

public enum Layers:int
{
	Blueprint = 9,
	Corpse = 11,
	Character = 12,
}

public static class Ext
{
	public static float AngleToFlat (this Vector3 a, Vector3 b)
	{
		return new Vector3 (a.x, 0, a.z).AngleTo (new Vector3 (b.x, 0, b.z));
	}

	public static float AngleTo (this Vector3 a, Vector3 b)
	{
		var angle = Vector3.Angle (a, b);
		if (Vector3.Cross (a, b).y < 0)
			angle = -angle;
		return angle;
	}
}