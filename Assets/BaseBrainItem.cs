using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBrainItem : MonoBehaviour
{
	public abstract void CopyTo (BaseBrainItem other) ;
}
