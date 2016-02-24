using System;
using System.Text;
using System.Collections.Generic;

namespace NumberGuesser
{
	public class NumberGuesser
	{
		private static readonly string[] _swearings = 
			new[] {"hahahahaha. Oh wait you’re serious. Let me laugh even harder.",
			"compare your live to mine and the kill yourself",
			"some day we''ll be able to look back on this whole thing and laugh...AH HA HA HA!!!",
			"bite my shiny metal ass!", " Hehe, sucker", "I'm so embarassed, I wish everyone was dead except me!"
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
				try 
				{
					input = Console.ReadLine ();
					guess = int.Parse(input);
					if (guess == _correct) 
					{
						ShowHistory();
						rightAnswer = true;
					}
					else
					{
						_attempts.Add(guess);
						if (_attempts.Count % 4 == 0)
							Console.WriteLine("{0}, {1}", username, _swearings[rand.Next(0, _swearings.Length)]);
					}
				}
				catch (FormatException) 
				{
					if (input == "q") {
						Console.WriteLine ("Oops! Sorry for inconvenience.");
						return;
					}
					Console.WriteLine ("It's not even a number. Try one more time");
				}
			}
		}
	}
}

