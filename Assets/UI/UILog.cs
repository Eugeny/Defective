using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogEntry
{
	public string Text;
	public float TTL;
}

public class UILog : MonoBehaviour
{
	public static UILog instance;
	public Text Output;
	private List<LogEntry> entries = new List<LogEntry> ();

	void Start ()
	{
		instance = this;
	}

	void Update ()
	{
		string text = "";
		foreach (var e in entries) {
			text += e.Text + '\n';
		}
		GetComponent<Image> ().enabled = entries.Count > 0;
		Output.text = text.Trim ();
	}

	void Refresh ()
	{
		GetComponent<HorizontalLayoutGroup> ().SetLayoutHorizontal ();
		Output.GetComponent<ContentSizeFitter> ().SetLayoutHorizontal ();
		Output.GetComponent<ContentSizeFitter> ().SetLayoutVertical();
		GetComponent<ContentSizeFitter> ().SetLayoutHorizontal ();
		GetComponent<ContentSizeFitter> ().SetLayoutVertical();
	}

	public LogEntry Log (string text, float ttl = 10)
	{
		var entry = new LogEntry{ Text = text, TTL = ttl };
		entries.Add (entry);
		if (ttl > 0) {
			StartCoroutine (HideAfter (entry, ttl));
		}
		GetComponent<AudioSource> ().Play ();
		Refresh ();
		return entry;
	}

	public void Delete (LogEntry entry)
	{
		entries.Remove (entry);
		Refresh ();
	}

	IEnumerator HideAfter (LogEntry entry, float time)
	{
		yield return new WaitForSeconds (time);
		Delete (entry);
	}
}
