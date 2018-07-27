using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace eat2fitDesktop.Models
{
    public class Customer
    {
		[BsonId(IdGenerator = typeof(CombGuidGenerator))]
		public Guid Id { get; set; }
		private string name;
		[BsonElement("Name")]
		public string Name { get { return name; } set { name = value; } }
		private string details;
		[BsonElement("Details")]
		public string Details { get => details; }
		private int age;
		[BsonElement("Age")]
		public int Age { get { return age; } set
			{
				age = value;
				details = "Age: " + value;
			}
		}
		private List<Meal> suggestedDiet;
		[BsonElement("SuggestedDeit")]
		public List<Meal> SuggestedDiet { get { return suggestedDiet; } set { suggestedDiet = value; } }
		private List<Meal> eatedDiet;
		[BsonElement("EatedDeit")]
		public List<Meal> EatedDiet { get { return eatedDiet; }set { eatedDiet = value; } }

		public void AddSuggestedMeal(Meal meal)
		{
					suggestedDiet.Add(meal);
		}

		public Customer()
		{
			suggestedDiet = new List<Meal>();
			eatedDiet = new List<Meal>();
		}
		public override string ToString()
		{
			return this.Name;
		}
	}
}
