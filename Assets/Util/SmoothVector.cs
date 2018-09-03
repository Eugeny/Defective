using UnityEngine;
using System.Collections;

public class SmoothVector
{
	private SmoothFloat x, y, z;
	private Vector3 target;
	public float Time;

	public SmoothVector (float time)
	{
		Time = time;
		x = new SmoothFloat (time);
		y = new SmoothFloat (time);
		z = new SmoothFloat (time);
	}

	public Vector3 Smooth {
		get { 
			return new Vector3 (x.Smooth, y.Smooth, z.Smooth);
		}
		set { 
			x.Smooth = value.x;
			y.Smooth = value.y;
			z.Smooth = value.z;
		}
	}

	public void Update ()
	{
		x.Time = Time;
		y.Time = Time;
		z.Time = Time;
		x.Update ();
		y.Update ();
		z.Update ();
	}

	public void Force (Vector3 v)
	{
		x.Force (v.x);
		y.Force (v.y);
		z.Force (v.z);
	}
}
