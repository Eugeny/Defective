using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHypothesisItem : BaseBrainItem{
	public Hypothesis Hypothesis;
	public Text Title, Facts;

	public override void CopyTo (BaseBrainItem other)
	{
		(other as UIHypothesisItem).Hypothesis = Hypothesis;
	}

	public override bool Equals (object other)
	{
		if (other is UIHypothesisItem) {
			return (other as UIHypothesisItem).Hypothesis.Equals (Hypothesis);
		}
		return false;
	}

	void Update ()
	{
		string text = "";
		foreach (var fact in Hypothesis.FoundFacts) {
			text += "* " + fact.Title + '\n';
		}
		for (int i = 0; i < Hypothesis.CompatibleFacts.Count - Hypothesis.FoundFacts.Count; i++) {
			text += "* ??? \n";
		}
		Title.text = Hypothesis.Title;
		Facts.text = text;
	}
}
