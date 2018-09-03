using UnityEngine;
using System.Collections;

public class SmoothFloat {
	protected float speed, value, target;

	public float Time;
	public float Smooth {
		get{
			return value;
		}
		set{
			target = value;
		}
	}

	public SmoothFloat(float time) {
		Time = time;
	}

	public virtual void Update () {
		value = Mathf.SmoothDamp (value, target, ref speed, Time);
	}

	public void Force(float v) {
		value = v;
		target = v;
	}
}
