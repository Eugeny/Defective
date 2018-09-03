using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionObject : MonoBehaviour
{
	public string[] Words;
	public Text TextPrefab;
	public Color color = Color.white;
	public Transform Marker;
	private List<Text> TextObjs = new List<Text> ();
	private RectTransform canvas;
	private float fx = 0;

	public string FactName = "";
	public GameObject DestroysObject;
	public Activable ActivatesObject;
	public AudioSource PlaysSound;

	void Start ()
	{
		if (FactName != "" && !Data.AllFacts.ContainsKey (FactName)) {
			Debug.LogError ("Fact not found: " + FactName);
			return;
		}
		
		canvas = FindObjectOfType<Canvas> ().GetComponent<RectTransform> ();
		for (int i = 0; i < Words.Length; i++) {
			var t = Instantiate<Text> (TextPrefab, canvas);
			t.text = Words [i];
			TextObjs.Add (t);
		}
	}

	void Update ()
	{
		int i = 0;
		float d = (CharacterControls.instance.transform.position - transform.position).magnitude;
		float visibility = Mathf.Max (0, 1 - (d - 1) / 3f);

		foreach (var text in TextObjs) {
			i++;
			var delta = new Vector3 (
				            Mathf.Sin (fx / 4 + i) + Mathf.Cos (fx / 8 + 3 + i),
				            1f,
				            Mathf.Sin (fx / 4 * 2 + 1 + i) + Mathf.Sin (fx / 8 * 2 + 3 + i) 
			            ) * 0.25f;


			Vector2 ViewportPosition = Camera.main.WorldToViewportPoint (transform.position + delta);
			text.rectTransform.anchoredPosition = new Vector2 (
				((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
				((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));
			
			text.color = new Color (color.r, color.g, color.b, visibility * (Mathf.Sin (Time.time / 4 * 2 + 1 + i) + 1));
		}

		Marker.localEulerAngles = new Vector3 (45, fx * 180, 45);

		if (IsInRange ()) {
			fx += Time.deltaTime * 3;
		} else {
			fx += Time.deltaTime;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (IsInRange ()) {
				DoAction ();
			}
		}
	}

	bool IsInRange ()
	{
		return (CharacterControls.instance.transform.position - transform.position).magnitude < 1.5f;
	}

	void OnDestroy ()
	{
		foreach (var text in TextObjs) {
			Destroy (text.gameObject);
		}
	}

	void DoAction ()
	{
		if (FactName != "") {
			Game.instance.DiscoverFact (Data.AllFacts [FactName]);
			Destroy (gameObject);
		}
		if (DestroysObject != null) {
			Destroy (DestroysObject);
			Destroy (gameObject);
		}
		if (ActivatesObject != null) {
			ActivatesObject.ActivateObject ();
			Destroy (gameObject);
		}
		if (PlaysSound != null) {
			PlaysSound.Play ();
			Destroy (gameObject);
		}
	}
}
