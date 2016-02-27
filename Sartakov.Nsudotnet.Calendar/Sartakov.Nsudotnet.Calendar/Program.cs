using System;
using System.Globalization;
using System.Text;

namespace Sartakov.Nsudotnet.Calendar
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string[] abbreviations = new string[7];
			Array.Copy (CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames, 1, abbreviations, 0, 6);
			abbreviations [6] = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames [0];
			string daysNames = String.Join("\t", abbreviations);

			Console.WriteLine ("Enter date");
			string dateString = Console.ReadLine ();
			DateTime dateValue;
			if (DateTime.TryParse (dateString, out dateValue)) 
			{
				Console.WriteLine (daysNames);

				int daysNum = DateTime.DaysInMonth (dateValue.Year, dateValue.Month);
				int holidays = 0;
				DateTime firstDayOfMonth = new DateTime(dateValue.Year, dateValue.Month, 1);
				DateTime today = DateTime.Now;
				int dayNum = (int)firstDayOfMonth.DayOfWeek;
				ConsoleColor currentBackground = Console.BackgroundColor;
				ConsoleColor currentForeground = Console.ForegroundColor;
				String tabs = new String ('\t', (int) (firstDayOfMonth.DayOfWeek - 1) % 7);
				Console.Write (tabs);
				for (int i = 0; i < daysNum; i++) 
				{
					currentBackground = ConsoleColor.Black;
					currentForeground = ConsoleColor.White;
					if ((i + 1) == dateValue.Day)
					{
						currentBackground = ConsoleColor.Blue;
					} 
					else if ((i + 1) == DateTime.Now.Day && firstDayOfMonth.Month == today.Month && firstDayOfMonth.Year == today.Year) 
					{
						currentBackground = ConsoleColor.Gray;
					}
					if (dayNum == (int)DayOfWeek.Saturday || dayNum == (int)DayOfWeek.Sunday) 
					{
						holidays++;
						currentForeground = ConsoleColor.Red;
					}
					Console.BackgroundColor = currentBackground;
					Console.ForegroundColor = currentForeground;
					Console.Write ("{0}\t", i + 1);
					if (dayNum == (int)DayOfWeek.Sunday)
						Console.Write ("\n");
					dayNum = (dayNum + 1) % 7;
				}
				Console.Write ("\n{0} holidays.\n", holidays);	
			} 
			else 
			{
				Console.WriteLine ("Sorry, it's not a date!");
			}
				
		}
	}
}
