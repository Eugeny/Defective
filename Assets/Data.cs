using System;
using System.Collections.Generic;

public class BaseItem
{
	public int id;
	static int lastID = 0;

	public BaseItem ()
	{
		id = ++lastID;
	}

	public override bool Equals (object obj)
	{
		if (obj is BaseItem) {
			return id == (obj as BaseItem).id;
		}
		return false;
	}
}

public class Fact : BaseItem
{
	public string Title;
}

public class Hypothesis : BaseItem
{
	public string Title;
	public List<Fact> FoundFacts = new List<Fact> ();
	public List<Fact> CompatibleFacts = new List<Fact> ();
	public List<Fact> IncompatibleFacts = new List<Fact> ();
	public Fact ResultingFact;
}

public class Data
{
	public static Dictionary<string, Fact> AllFacts = new Dictionary<string, Fact> {
		{ "", new Fact { Title = "MISSING" } },

		{ "dark",  new Fact { Title = "It's dark" } },
		{ "see nothing",  new Fact { Title = "I see nothing" } },
		{ "traffic",  new Fact { Title = "Traffic sounds" } },

		{ "head hurts",  new Fact { Title = "My head hurts" } },
		{ "cant move", new Fact { Title = "I can barely move" } },
		{ "dry mouth", new Fact { Title = "My mouth is dry" } },
		{ "bruises", new Fact { Title = "I've got bruises" } },
		{ "wallet gone", new Fact { Title = "My wallet is gone" } },
		{ "phone gone", new Fact { Title = "My phone is gone" } },

		{ "hungover", new Fact { Title = "I am badly hungover" } },

		{ "cops", new Fact { Title = "Someone is banging on the door!" } },
		{ "cake", new Fact { Title = "There's a cake" } },
		{ "confetti", new Fact { Title = "Confetti" } },
		{ "knife", new Fact { Title = "Knife" } },
		{ "body", new Fact { Title = "Body" } },
		{ "man", new Fact { Title = "Sleeping man" } },
		{ "case", new Fact { Title = "Case full of cash" } },
		{ "chips", new Fact { Title = "Casino chips" } },
		{ "news", new Fact { Title = "Casino robbery on the TV" } },
		{ "booze", new Fact { Title = "Booze everywhere" } },

		{ "birthday", new Fact { Title = "Birthday party" } },
		{ "casino", new Fact { Title = "We've been in a casino" } },
		{ "robbed", new Fact { Title = "We've robbed a casino" } },

		{ "door dead", new Fact { Title = "If they get in, I'm dead" } },
		{ "get out", new Fact { Title = "Must grab the cash and get out" } },

		{ "dead", new Fact { Title = "I am dead." } },
	};

	public static Dictionary<string, Hypothesis> AllHypotheses = new Dictionary<string, Hypothesis> { {
			"night", 
			new Hypothesis {
				Title = "It's night",
				CompatibleFacts = { 
					AllFacts ["see nothing"],
					AllFacts ["dark"],
					AllFacts [""],
				},
				IncompatibleFacts = { 
					AllFacts ["traffic"],
				},
			}
		}, {
			"eyes", 
			new Hypothesis {
				Title = "My eyes are closed",
				CompatibleFacts = { 
					AllFacts ["see nothing"],
					AllFacts ["dark"],
					AllFacts ["traffic"],
				},
			}
		}, {
			"hungover", 
			new Hypothesis {
				Title = "I am badly hungover",
				CompatibleFacts = { 
					AllFacts ["head hurts"],
					AllFacts ["cant move"],
					AllFacts ["dry mouth"],
				},
				ResultingFact = AllFacts ["hungover"],
			}
		}, {
			"mugged", 
			new Hypothesis {
				Title = "I've been mugged",
				CompatibleFacts = { 
					AllFacts ["head hurts"],
					AllFacts ["wallet gone"],
					AllFacts ["phone gone"],
					AllFacts ["bruises"],
					AllFacts [""],
				},
			}
		}, {
			"birthday", 
			new Hypothesis {
				Title = "It was a birthday party",
				CompatibleFacts = { 
					AllFacts ["cake"],
					AllFacts ["hungover"],
					AllFacts ["confetti"],
					AllFacts ["booze"],
				},
				ResultingFact = AllFacts ["birthday"],
			}
		}, {
			"ritual", 
			new Hypothesis {
				Title = "It was an occult ritual",
				CompatibleFacts = { 
					AllFacts ["knife"],
					AllFacts ["body"],
					AllFacts [""],
					AllFacts [""],
				},
				IncompatibleFacts = { 
					AllFacts ["man"],
					AllFacts ["birthday"],
				},
			}
		}, {
			"cook", 
			new Hypothesis {
				Title = "I'm a pastry cook",
				CompatibleFacts = { 
					AllFacts ["cake"],
					AllFacts ["knife"],
					AllFacts [""],
				},
			}
		}, {
			"murder", 
			new Hypothesis {
				Title = "It's a murder scene",
				CompatibleFacts = { 
					AllFacts ["knife"],
					AllFacts ["body"],
					AllFacts ["case"],
					AllFacts [""],
				},
				IncompatibleFacts = { 
					AllFacts ["man"],
				},
			}
		}, {
			"casino", 
			new Hypothesis {
				Title = "We've been to a casino",
				CompatibleFacts = { 
					AllFacts ["chips"],
					AllFacts ["birthday"],
					AllFacts ["case"],
				},
				ResultingFact = AllFacts ["casino"],
			}
		},  {
			"lotter", 
			new Hypothesis {
				Title = "I won a lottery",
				CompatibleFacts = { 
					AllFacts ["booze"],
					AllFacts ["cake"],
					AllFacts ["case"],
					AllFacts [""],
				},
			}
		}, {
			"robbed", 
			new Hypothesis {
				Title = "We've robbed a casino",
				CompatibleFacts = { 
					AllFacts ["casino"],
					AllFacts ["news"],
					AllFacts ["case"],
					AllFacts ["cops"],
				},
				ResultingFact = AllFacts ["robbed"],
			}
		}, {
			"door dead", 
			new Hypothesis {
				Title = "If I open the door, I'm dead",
				CompatibleFacts = { 
					AllFacts ["robbed"],
					AllFacts ["cops"],
				},
				ResultingFact = AllFacts ["door dead"],
			}
		},
	};
}

