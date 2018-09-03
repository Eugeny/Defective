using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
	public static Game instance;
	public List<Fact> KnownFacts = new List<Fact> ();
	public List<Hypothesis> CurrentHypotheses = new List<Hypothesis> ();
	public List<Hypothesis> PastHypotheses = new List<Hypothesis> ();
	public UnityEvent FactsChanged = new UnityEvent ();
	public UnityEvent HypothesesChanged = new UnityEvent ();


	public void DiscoverFact (Fact f)
	{
		KnownFacts.Insert (0, f);
		FactsChanged.Invoke ();
		UILog.instance.Log ("<i>[New fact]</i>: " + f.Title);
		OnFactDiscovered (f);
	}

	public void DiscoverHypothesis (Hypothesis h)
	{
		CurrentHypotheses.Insert (0, h);
		HypothesesChanged.Invoke ();
		UILog.instance.Log ("<i>[New theory]</i>: " + h.Title);
	}

	public void AcceptHypothesis (Hypothesis h)
	{
		if (h.ResultingFact != null) {
			DiscoverFact (h.ResultingFact);
		}
		CurrentHypotheses.Remove (h);
		HypothesesChanged.Invoke ();
		PastHypotheses.Add (h);
		UILog.instance.Log ("<i>[Confirmed]</i>: " + h.Title);

		OnHypothesisAccepted (h);
	}

	public void RejectHypothesis (Hypothesis h)
	{
		CurrentHypotheses.Remove (h);
		HypothesesChanged.Invoke ();
		PastHypotheses.Add (h);
		UILog.instance.Log ("<i>[Theory rejected]</i>: " + h.Title);

		OnHypothesisRejected (h);
	}

	void Start ()
	{
		instance = this;

		StartCoroutine (Plot ());
	}


	public bool EnableTutorial = true, EnableRoom1 = true, EnableRoom2 = true;
	public ActionObject StreetNoiseAO;
	public ActionObject WalletGoneAO;
	public ActionObject PhoneGoneAO;
	public ActionObject DoorAO;
	public ActionObject WalkAO;
	public ActionObject ConfettiAO;
	public ActionObject BodyAO;
	public ActionObject ManAO;
	public ActionObject CakeAO;
	public ActionObject FridgeAO;
	public ActionObject BoozeAO;
	public ActionObject KnifeAO;
	public ActionObject CaseAO;
	public ActionObject ChipsAO;
	public ActionObject NewsAO;
	public ActionObject CopsAO;
	public TV TV;
	public AudioSource CopsSound;
	public AudioSource Breakin, Music;

	IEnumerator Plot ()
	{
		StreetNoiseAO.gameObject.SetActive (false);
		PhoneGoneAO.gameObject.SetActive (false);
		WalletGoneAO.gameObject.SetActive (false);
		DoorAO.gameObject.SetActive (false);
		WalkAO.gameObject.SetActive (false);
		ConfettiAO.gameObject.SetActive (false);
		BodyAO.gameObject.SetActive (false);
		ManAO.gameObject.SetActive (false);
		CakeAO.gameObject.SetActive (false);
		FridgeAO.gameObject.SetActive (false);
		BoozeAO.gameObject.SetActive (false);
		KnifeAO.gameObject.SetActive (false);
		CaseAO.gameObject.SetActive (false);
		ChipsAO.gameObject.SetActive (false);
		NewsAO.gameObject.SetActive (false);
		CopsAO.gameObject.SetActive (false);

		CharacterControls.instance.EnableMotion = false;

		if (EnableTutorial) {
			UIFade.instance.Enable = true;
			yield return new WaitForSeconds (1f);
			UILog.instance.Log ("Ugh...");
			yield return new WaitForSeconds (2f);
			DiscoverFact (Data.AllFacts ["dark"]);
			yield return new WaitForSeconds (2f);
			DiscoverFact (Data.AllFacts ["see nothing"]);

			yield return new WaitForSeconds (2f);

			var msg = UILog.instance.Log ("<i>Press the Brain button to start deducing</i>", 0);
			while (!UIBrainsButton.instance.Visible) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			msg = UILog.instance.Log ("<i>Select both facts and think</i>", 0);
			while (Game.instance.CurrentHypotheses.Count == 0) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);


			UILog.instance.Log ("<b>Dude:</b> Alright, that's the working hypothesis for now");

			msg = UILog.instance.Log ("<i>Press the Brain button again</i>", 0);
			while (UIBrainsButton.instance.Visible) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			yield return new WaitForSeconds (2f);

			// Play sounds
			StreetNoiseAO.gameObject.SetActive (true);

			msg = UILog.instance.Log ("<i>Press Space to interact</i>", 0);
			while (Game.instance.KnownFacts.Count < 3) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);


			yield return new WaitForSeconds (1f);

			msg = UILog.instance.Log ("<i>Press the Brain button again</i>", 0);
			while (!UIBrainsButton.instance.Visible) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			yield return new WaitForSeconds (1f);

			msg = UILog.instance.Log ("<i>Select the new fact and one of the theories and THINK</i>", 0);
			while (Game.instance.CurrentHypotheses.Count == 2) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			yield return new WaitForSeconds (2f);

			msg = UILog.instance.Log ("<i>Repeat for the other theory</i>", 0);
			while (Game.instance.CurrentHypotheses.Count == 1) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			msg = UILog.instance.Log ("<i>Press the Brain button again</i>", 0);
			while (UIBrainsButton.instance.Visible) {
				yield return new WaitForSeconds (1f);
			}
			UILog.instance.Delete (msg);

			yield return new WaitForSeconds (2f);
			UILog.instance.Log ("<b>Dude:</b> Gonna open my blinkers now");
			yield return new WaitForSeconds (1f);

			UIFade.instance.Enable = false;
			CharacterControls.instance.Awake = true;
			yield return new WaitForSeconds (5f);
			CharacterControls.instance.EnableMotion = true;


			yield return new WaitForSeconds (1f);
			UILog.instance.Log ("<i>Use WASD to walk</i>");
			yield return new WaitForSeconds (1f);
			UILog.instance.Log ("<i>Use mouse to rotate and zoom</i>");
			yield return new WaitForSeconds (1f);
			UILog.instance.Log ("<i>Space to interact</i>");

			yield return new WaitForSeconds (3f);

			UILog.instance.Log ("<b>Dude:</b> What the hell?");


			KnownFacts.Remove (Data.AllFacts ["dark"]);
			KnownFacts.Remove (Data.AllFacts ["see nothing"]);
			yield return new WaitForSeconds (5f);

		} else {
			CharacterControls.instance.EnableMotion = true;
			CharacterControls.instance.Awake = true;
			UIFade.instance.Enable = false;
		}

		if (EnableRoom1) {
			PhoneGoneAO.gameObject.SetActive (true);
			WalletGoneAO.gameObject.SetActive (true);
			WalkAO.gameObject.SetActive (true);

			UILog.instance.Log ("<b>Dude:</b> Ugh, my head...");
			yield return new WaitForSeconds (2f);
			DiscoverFact (Data.AllFacts ["head hurts"]);
			yield return new WaitForSeconds (5f);

			UILog.instance.Log ("<b>Dude:</b> Ouch!");
			yield return new WaitForSeconds (2f);
			DiscoverFact (Data.AllFacts ["bruises"]);
			yield return new WaitForSeconds (5f);

			UILog.instance.Log ("<b>Dude:</b> ...");
			yield return new WaitForSeconds (2f);
			DiscoverFact (Data.AllFacts ["dry mouth"]);
			yield return new WaitForSeconds (5f);

			while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["hungover"])) {
				yield return new WaitForSeconds (1f);
			}
			yield return new WaitForSeconds (2f);
			UILog.instance.Log ("<b>Dude:</b> Gotta check the other room");
		} else {
			DiscoverFact (Data.AllFacts ["hungover"]);
		}

		Coroutine cops2;
		DoorAO.gameObject.SetActive (true);

		if (EnableRoom2) {
			BodyAO.gameObject.SetActive (true);
			KnifeAO.gameObject.SetActive (true);
			FridgeAO.gameObject.SetActive (true);
			ConfettiAO.gameObject.SetActive (true);
			BoozeAO.gameObject.SetActive (true);
			CaseAO.gameObject.SetActive (true);

			while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["body"])) {
				yield return new WaitForSeconds (1f);
			}
			while (!Game.instance.CurrentHypotheses.Contains (Data.AllHypotheses ["murder"])) {
				yield return new WaitForSeconds (1f);
			}
			yield return new WaitForSeconds (15f);

			ManAO.gameObject.SetActive (true);
			CopsAO.gameObject.SetActive (true);
			ChipsAO.gameObject.SetActive (true);

			var cops = StartCoroutine (CopsShouting ());
			CopsSound.Play ();

			while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["casino"])) {
				yield return new WaitForSeconds (1f);
			}

			yield return new WaitForSeconds (5f);
			TV.Enable ();
			var tv = StartCoroutine (TVBroadcast ());
			NewsAO.gameObject.SetActive (true);

			while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["news"])) {
				yield return new WaitForSeconds (1f);
			}
			StopCoroutine (tv);

			while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["robbed"])) {
				yield return new WaitForSeconds (1f);
			}

			StopCoroutine (cops);
			cops2 = StartCoroutine (CopsShouting2 ());
			yield return new WaitForSeconds (5f);
		} else {
			CopsSound.Play ();
			cops2 = StartCoroutine (CopsShouting2 ());
			DiscoverFact (Data.AllFacts ["robbed"]);
			DiscoverFact (Data.AllFacts ["cops"]);
		}

		DiscoverHypothesis (Data.AllHypotheses ["door dead"]);

		while (!Game.instance.KnownFacts.Contains (Data.AllFacts ["door dead"])) {
			yield return new WaitForSeconds (1f);
		}

		yield return new WaitForSeconds (5f);
		DiscoverFact (Data.AllFacts ["get out"]);

		UILog.instance.Log ("<b>Dude:</b> I");
		UILog.instance.Log ("<b>Dude:</b> NEED TO");
		UILog.instance.Log ("<b>Dude:</b> GET OUT");

		yield return new WaitForSeconds (10f);

		UIFade.instance.Enable = true;
		yield return new WaitForSeconds (3f);
		StopCoroutine (cops2);
		CopsSound.Stop ();
		Music.Stop ();
		Breakin.Play ();
		CharacterControls.instance.EnableMotion = false;
		yield return new WaitForSeconds (9f);
		DiscoverFact (Data.AllFacts ["dead"]);
		yield return new WaitForSeconds (5f);
		Application.Quit ();
	}

	IEnumerator CopsShouting ()
	{
		var lines = new string[] {
			"Open up!",
			"Open the door!",
			"Open it right now!",
			"Hey you!",
			"We know you're inside!",
			"Open now!",
		};

		while (true) {
			UILog.instance.Log ("<b>[Behind the door]:</b> " + lines [Random.Range (0, lines.Length)]);
			yield return new WaitForSeconds (Random.Range (10, 20));
		}
	}

	IEnumerator CopsShouting2 ()
	{
		var lines = new string[] {
			"Open the fucking door!",
			"We know what you did!",
			"You're gonna pay!",
			"You're a dead man!",
			"We'll find you one way or another!",
			"Open up!",
		};

		while (true) {
			yield return new WaitForSeconds (Random.Range (10, 20));
			UILog.instance.Log ("<b>[Behind the door]:</b> " + lines [Random.Range (0, lines.Length)]);
		}
	}

	IEnumerator TVBroadcast ()
	{
		var lines = new string[] {
			"Last night, the Miracle Casino \nwas robbed by two suspects.",
			"The attackers got away with \na case of cash and high-value chips.",
			"Police have been unable \nto determine their identity so far.",
			"Miracle Casino is conducting \nits own investigation of this crime.",
			"We'll continue reporting \non the situation as it unfolds.",
		};

		while (true) {
			foreach (string line in lines) {
				UILog.instance.Log ("<b>[TV]:</b> " + line);
				yield return new WaitForSeconds (Random.Range (5, 10));
			}
		}
	}

	void OnHypothesisAccepted (Hypothesis h)
	{
		if (h == Data.AllHypotheses ["eyes"]) {
			UILog.instance.Log ("<b>Dude:</b> Makes sense");
		}
		if (h == Data.AllHypotheses ["hungover"]) {
			UILog.instance.Log ("<b>Dude:</b> Sure feels like it");
		}
	}

	void OnHypothesisRejected (Hypothesis h)
	{
		if (h == Data.AllHypotheses ["night"]) {
			UILog.instance.Log ("<b>Dude:</b> Yeah, bloody nonsense");
		}
	}

	void OnFactDiscovered (Fact f)
	{
		if (f == Data.AllFacts ["phone gone"]) {
			UILog.instance.Log ("<b>Dude:</b> Shit, my phone is gone");
		}
		if (f == Data.AllFacts ["body"]) {
			UILog.instance.Log ("<b>Dude:</b> Fuck, fuck, FUCK! Is he dead?!");
		}
		if (f == Data.AllFacts ["case"]) {
			UILog.instance.Log ("<b>Dude:</b> That is at least a million...");
		}
		if (f == Data.AllFacts ["news"]) {
			UILog.instance.Log ("<b>Dude:</b> Oh no");
		}
		if (f == Data.AllFacts ["cops"]) {
			UILog.instance.Log ("<b>Dude:</b> What the hell do they want?");
		}
		if (f == Data.AllFacts ["man"]) {
			UILog.instance.Log ("<b>Sleeping man:</b> Aye fuck off mate...");
			KnownFacts.Remove (Data.AllFacts ["body"]);
			FactsChanged.Invoke ();
		}
	}

}
