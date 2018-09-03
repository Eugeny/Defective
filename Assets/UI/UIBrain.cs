using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBrain : MonoBehaviour
{
	public static UIBrain instance;
	public RectTransform Item1Slot, Item2Slot;
	private BaseBrainItem Item1, Item2;
	public Color Red, Green;
	public Button OKButton;

	void Start ()
	{
		instance = this;
	}

	public void AddItem (BaseBrainItem item)
	{
		if ((Item1 != null && Item1.Equals (item)) || (Item2 != null && Item2.Equals (item))) {
			return;
		} else if (item is UIHypothesisItem && Item2 is UIHypothesisItem) {
			SetItem2 (item);
		} else if (item is UIHypothesisItem && Item1 is UIHypothesisItem) {
			SetItem1 (item);
		} else if (Item1 == null) {
			SetItem1 (item);
		} else if (Item2 == null) {
			SetItem2 (item);
		} else if (item is UIFactItem && Item2 is UIHypothesisItem) {
			SetItem1 (item);
		} else if (item is UIFactItem && Item1 is UIHypothesisItem) {
			SetItem2 (item);
		} else {
			SetItem2 (item);
		}
		Refresh ();
	}

	void SetItem1 (BaseBrainItem i)
	{
		RemoveItem1 ();
		var c = Instantiate<BaseBrainItem> (i, Item1Slot);
		i.CopyTo (c);
		Item1 = c;
		Item1Slot.GetComponent<Button> ().enabled = true;
	}

	void SetItem2 (BaseBrainItem i)
	{
		RemoveItem2 ();
		var c = Instantiate<BaseBrainItem> (i, Item2Slot);
		i.CopyTo (c);
		Item2 = c;
		Item2Slot.GetComponent<Button> ().enabled = true;
	}

	public void RemoveItem1 ()
	{
		if (!Item1) {
			return;
		}
		Destroy (Item1.gameObject);
		Item1 = null;
		Item1Slot.GetComponent<Button> ().enabled = false;
		Refresh ();
	}

	public 	void RemoveItem2 ()
	{
		if (!Item2) {
			return;
		}
		Destroy (Item2.gameObject);
		Item2 = null;
		Item2Slot.GetComponent<Button> ().enabled = false;
		Refresh ();
	}

	void Refresh ()
	{
		if (Item1 != null && Item2 != null) {
			OKButton.GetComponent<Image> ().color = HasResult () ? Green : Red;
		} else {
			OKButton.GetComponent<Image> ().color = Color.white;
		}
	}

	bool HasResult ()
	{
		if ((Item1 is UIHypothesisItem && Item2 is UIFactItem) || (Item1 is UIFactItem && Item2 is UIHypothesisItem)) {
			Hypothesis h = (Item1 as UIHypothesisItem ?? Item2 as UIHypothesisItem).Hypothesis;
			Fact f = (Item1 as UIFactItem ?? Item2 as UIFactItem).Fact;

			if (h.CompatibleFacts.Contains (f) && !h.FoundFacts.Contains (f)) {
				return true;
			}
			if (h.IncompatibleFacts.Contains (f)) {
				return true;
			}
		}

		if (Item1 is UIFactItem && Item2 is UIFactItem) {
			Fact f1 = (Item1 as UIFactItem).Fact;
			Fact f2 = (Item2 as UIFactItem).Fact;
			foreach (var hyp in Data.AllHypotheses.Values) {
				if (!Game.instance.CurrentHypotheses.Contains (hyp) && !Game.instance.PastHypotheses.Contains (hyp) && hyp.CompatibleFacts.Contains (f1) && hyp.CompatibleFacts.Contains (f2)) {
					return true;
				}
			}
		}
		return false;
	}

	public void Combine ()
	{
		if ((Item1 is UIHypothesisItem && Item2 is UIFactItem) || (Item1 is UIFactItem && Item2 is UIHypothesisItem)) {
			Hypothesis h = (Item1 as UIHypothesisItem ?? Item2 as UIHypothesisItem).Hypothesis;
			Fact f = (Item1 as UIFactItem ?? Item2 as UIFactItem).Fact;
			if (h.CompatibleFacts.Contains (f) && !h.FoundFacts.Contains (f)) {
				h.FoundFacts.Add (f);
				if (h.FoundFacts.Count == h.CompatibleFacts.Count) {
					Game.instance.AcceptHypothesis (h);
				}
				RemoveItem1 ();
				RemoveItem2 ();
			}
			if (h.IncompatibleFacts.Contains (f)) {
				Game.instance.RejectHypothesis (h);
				RemoveItem1 ();
				RemoveItem2 ();
			}
		}

		if (Item1 is UIFactItem && Item2 is UIFactItem) {
			Fact f1 = (Item1 as UIFactItem).Fact;
			Fact f2 = (Item2 as UIFactItem).Fact;
			foreach (var hyp in Data.AllHypotheses.Values) {
				if (!Game.instance.CurrentHypotheses.Contains (hyp) && !Game.instance.PastHypotheses.Contains (hyp) && hyp.CompatibleFacts.Contains (f1) && hyp.CompatibleFacts.Contains (f2)) {
					hyp.FoundFacts.Add (f1);
					hyp.FoundFacts.Add (f2);
					Game.instance.DiscoverHypothesis (hyp);
					RemoveItem1 ();
					RemoveItem2 ();
				}
			}
		}
	}
}
