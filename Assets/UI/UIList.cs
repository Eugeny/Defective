using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIList<T> : MonoBehaviour
{
	public List<T> Items;
	public UIListItem ListItemPrefab;

	public void Refresh ()
	{
		while (transform.childCount>0) {
			DestroyImmediate(transform.GetChild (0).gameObject);
		}
		foreach (T item in Items) {
			Transform t = Instantiate (ListItemPrefab.transform, transform) as Transform;
			Render (item, t.GetComponent<UIListItem> ());
		}
	}

	protected abstract void Render (T item, UIListItem listItem);
}
