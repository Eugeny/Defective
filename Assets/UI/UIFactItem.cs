using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFactItem : BaseBrainItem {
	public Fact Fact;

	public override void CopyTo (BaseBrainItem other)
	{
		(other as UIFactItem).Fact = Fact;
	}

	public override bool Equals (object other)
	{
		if (other is UIFactItem) {
			return (other as UIFactItem).Fact.Equals(Fact);
		}
		return false;
	}

	void Update () {
		GetComponentInChildren<UnityEngine.UI.Text> ().text = Fact.Title;
	}
}
