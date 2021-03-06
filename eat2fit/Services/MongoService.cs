﻿using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using eat2fit.Models;
using System.Security.Authentication;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace eat2fit.Services
{
    public class MongoService
    {
		string dbName = "eat2fit";

		//ToDo: remove duplication of the setup of settings and db for each collection.

		IMongoCollection<Customer> customersCollection;
		IMongoCollection<Customer> CustomersCollection
		{
			get
			{
				if (customersCollection == null)
				{
					MongoClientSettings settings = MongoClientSettings.FromUrl(
					  new MongoUrl(Config.ConnectionString)
					);

					settings.SslSettings =
						new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

					// Initialize the client
					var mongoClient = new MongoClient(settings);

					// This will create or get the database
					var db = mongoClient.GetDatabase(dbName);

					// This will create or get the collection
					var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
					customersCollection = db.GetCollection<Customer>("Customers", collectionSettings);
				}
				return customersCollection;
			}
		}

		IMongoCollection<Food> foodsCollection;
		IMongoCollection<Food> FoodsCollection
		{
			get
			{
				if (foodsCollection == null)
				{
					MongoClientSettings settings = MongoClientSettings.FromUrl(
					  new MongoUrl(Config.ConnectionString)
					);

					settings.SslSettings =
						new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

					// Initialize the client
					var mongoClient = new MongoClient(settings);

					// This will create or get the database
					var db = mongoClient.GetDatabase(dbName);

					// This will create or get the collection
					var collectionSettings = new MongoCollectionSettings { ReadPreference = ReadPreference.Nearest };
					foodsCollection = db.GetCollection<Food>("Foods", collectionSettings);
				}
				return foodsCollection;
			}
		}

		public async Task<Customer> GetCustomerIfPasswordVerified(string name, string pass)
		{
			try
			{
				var v = await CustomersCollection.Find(x => x.Name == name).FirstAsync() ;
				Customer customer;
				if (v is Customer)
				{
					customer = v as Customer;
					if (customer.Password == pass)
					{
						return customer;
					}
				}
				else { System.Diagnostics.Debug.WriteLine("error, returned not customer in GetCustomer in MongoService"); }
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return null;
		}

		public async Task<List<Customer>> GetAllCustomers()
		{
			try
			{
				var allCustomers = await CustomersCollection
					.Find(new BsonDocument())
					.ToListAsync();
				return allCustomers;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return null;
		}
		public async Task<List<Food>> GetAllFoods()
		{
			try
			{
				var allFoods = await FoodsCollection
					.Find(new BsonDocument())
					.ToListAsync();
				return allFoods;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return null;
		}
		public async Task CreateCustomer(Customer customer)
		{
			await CustomersCollection.InsertOneAsync(customer);
		}
		public async Task CreateFood(Food food)
		{
			await FoodsCollection.InsertOneAsync(food);
		}

		
		public async Task EditCustomer(Customer customer)
		{
			await CustomersCollection.ReplaceOneAsync(t => t.Id.Equals(customer.Id), customer);
		}

	}
}
