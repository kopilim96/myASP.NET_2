using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryServices
{
	public class DataHelpers
	{
		public static List<string> readableHourMethod(IEnumerable<BranchHour> branchHours) {
			//Create new List to store every date and day
			var hour = new List<string>();
			foreach (var time in branchHours) {
				var day = readableDay(time.dayOfWeek);
				var openTime = readableTime(time.openTime);
				var closeTime = readableTime(time.closeTime);

				var timeEntry = $"{day} {openTime} to {closeTime}";
				//Add the combined time to list
				hour.Add(timeEntry);
			}
			return hour;
		}

		public static string readableDay(int number) {
			return Enum.GetName(typeof(DayOfWeek), number -1);
		}
		public static string readableTime(int time) {
			return TimeSpan.FromHours(time).ToString("hh':'mm");
		}
	}
}
