using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFactsList : UIList<Fact> {
	public UIFactItem UIFactItemPrefab;

	void Start () {
		Items = Game.instance.KnownFacts;
		Game.instance.FactsChanged.AddListener (Refresh);
		Refresh ();
	}
		
	protected override void Render (Fact item, UIListItem listItem)
	{
		var t = Instantiate<UIFactItem> (UIFactItemPrefab, listItem.transform);
		(listItem.transform as RectTransform).SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, 50);
		t.Fact = item;
		listItem.GetComponent<Button> ().onClick.AddListener (() => {
			UIBrain.instance.AddItem(t);
		});
	}
}
