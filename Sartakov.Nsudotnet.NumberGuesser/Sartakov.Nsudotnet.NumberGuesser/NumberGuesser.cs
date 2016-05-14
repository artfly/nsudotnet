using System;
using System.Text;
using System.Collections.Generic;

namespace NumberGuesser
{
	public class NumberGuesser
	{
		private static readonly string[] _swearings = 
			new[] {"Hahahahaha. Oh wait, {0}, you’re serious. Let me laugh even harder.",
			"{0}, compare your live to mine and the kill yourself",
			"{0}, some day we'll be able to look back on this whole thing and laugh...AH HA HA HA!!!",
			"Bite my shiny metal ass, {0}!", " Hehe, sucker {0}", "{0}, I'm so embarassed, I wish everyone was dead except me!"
		};
		private List<int> _attempts = new List<int>();

		private int _correct;
		private DateTime _start;

		public NumberGuesser ()
		{
		}

		private void ShowHistory()
		{
			StringBuilder builder = new StringBuilder();
			TimeSpan duration = DateTime.Now - _start;
			Console.WriteLine ("You've made {0} attempts and spent {1} minutes of your life", _attempts.Count, duration.Minutes);
			foreach (var attempt in _attempts) 
			{
				builder.AppendLine (string.Format("{0} - {1}", attempt, (attempt < _correct? "smaller" : "bigger")));
			}
			Console.Write (builder);
		}

		public void Start()
		{
			Console.WriteLine ("Please introduce yourself");
			string username = Console.ReadLine ();
			Console.WriteLine ("I'm thinking about a number between 1 and 100. Any ideas?");

			Random rand = new Random ();
			_correct = rand.Next(0, 100);
			string input = "";
			bool rightAnswer = false;
			int guess;
			_start = DateTime.Now;
			while (!rightAnswer) 
			{
				input = Console.ReadLine ();
				if (int.TryParse (input, out guess)) 
				{
					if (guess == _correct) 
					{
						ShowHistory();
						rightAnswer = true;
					}
					else
					{
						_attempts.Add(guess);
						if (_attempts.Count % 4 == 0)
							Console.WriteLine(_swearings[rand.Next(0, _swearings.Length)], username);
					}
				}
				else if (input == "q") 
				{
					Console.WriteLine ("Oops! Sorry for inconvenience.");
					return;
				}
				else 
					Console.WriteLine ("It's not even a number. Try one more time");	
			}
		}
	}
}

