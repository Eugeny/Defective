using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHypothesesList: UIList<Hypothesis> {
	public UIHypothesisItem UIHypothesisItemPrefab;

	void Start () {
		Items = Game.instance.CurrentHypotheses;
		Game.instance.HypothesesChanged.AddListener (Refresh);
		Refresh ();
	}

	protected override void Render (Hypothesis item, UIListItem listItem)
	{
		var t = Instantiate<UIHypothesisItem> (UIHypothesisItemPrefab, listItem.transform);
		(listItem.transform as RectTransform).SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 130);
		t.GetComponent<UIHypothesisItem> ().Hypothesis = item;
		listItem.GetComponent<Button> ().onClick.AddListener (() => {
			UIBrain.instance.AddItem(t);
		});
	}
}

