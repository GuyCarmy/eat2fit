using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace eat2fitDesktop.Models
{
    public class Meal
    {
		private string details;
		[BsonElement("Details")]
		public string Details { get => details; }

		private string name;
		[BsonElement("Name")]
		public string Name { get => name; }

		[BsonElement("Foods")]
		public List<Food> Foods { get; set; }

		public Meal()
		{
			Foods = new List<Food>();
		}

		private int time;
		[BsonElement("Time")]
		public int Time
		{
			get { return time; }
			set
			{
				time = value;
				// time is kept in minutes sense midnight. either TimeSpan or DateTime fits right for this usage, so I went for the simplest solution.
				int mealMin = value % 60;
				int mealHr = value / 60;
				int cal = 0;
				foreach (Food f in Foods)
					cal += f.Calories*f.Amount;
				cal /= 100;
				name = "Meal Time: " + mealHr + ":" + mealMin;
				details = "Meal Calories: " + cal; //todo will include protein, carbs and fat for the meal 
			}
		}
	}
}
