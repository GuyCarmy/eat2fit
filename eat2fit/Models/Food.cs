using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace eat2fitDesktop.Models
{
    public class Food
    {
		[BsonId(IdGenerator = typeof(CombGuidGenerator))]
		public Guid Id { get; set; }

		private string name;
		[BsonElement("Name")]
		public string Name {
			get
			{
				return name;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
				{
					name = value;
				}
			}
		}

		private string details;
		[BsonElement("Details")]
		public string Details {
			get
			{
				return details;
			}
		}
		private string detailsForMeal;
		[BsonElement("DetailsForMeal")]
		public string DetailsForMeal
		{
			get
			{
				return detailsForMeal;
			}
		}

		private int calories;
		[BsonElement("Calories")]
		public int Calories {
			get
			{
				return calories;
			}
			set
			{
				if (value > 0 && value <= 900)
				{ 
					calories = value;
					details = "Calories: " + value;
				}
			} 
		}
		private int amount;
		[BsonElement("Amount")]
		public int Amount
		{ 
			get
			{
				return amount;
			}
			set
			{
				if (value >= 0 && value <= 5000)
				{
					amount = value;
					detailsForMeal = details+" Amount: " + value;
				}
				else {
					System.Diagnostics.Debug.WriteLine("Wrong Food Amount"); } //todo raise exception
			}
		}



	}
}
