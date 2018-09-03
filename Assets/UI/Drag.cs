using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag  : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
	public static GameObject CurrentlyDragging;
	private RectTransform originalParent;
	private int originalSiblingIndex;

	public void OnBeginDrag(PointerEventData eventData) {
		if (transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup> () != null) {
			transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup> ().enabled = false;
		}
		originalParent = transform.parent as RectTransform;
		originalSiblingIndex = transform.GetSiblingIndex ();
		transform.SetParent (GetComponentInParent<Canvas> ().transform);
		CurrentlyDragging = gameObject;
	}

	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");

		this.transform.position = eventData.position;

	}

	public void OnEndDrag(PointerEventData eventData) {
		Debug.Log ("OnEndDrag");
		transform.SetParent (originalParent);
		transform.SetSiblingIndex (originalSiblingIndex);
		if (transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup> () != null) {
			transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup> ().enabled = true;
			transform.parent.GetComponent<HorizontalOrVerticalLayoutGroup> ().SetLayoutVertical ();
		}
	}

	public void OnDrop (PointerEventData eventData)
	{
		Debug.Log ("Drop on ", this);
		CurrentlyDragging = null;
	}
}