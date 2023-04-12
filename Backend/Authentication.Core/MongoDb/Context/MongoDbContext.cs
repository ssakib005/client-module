using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Core.MongoDb.Context
{
	public class MongoDbContext : IMongoDbContext
	{
		private readonly IMongoDatabase _database;

		public MongoDbContext(string connectionString, string databaseName)
		{
			var conventionPack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
			ConventionRegistry.Register("IgnoreExtraElementsConvention", conventionPack, t => true);

			var mongoUrl = new MongoUrl(connectionString);
			var settings = MongoClientSettings.FromUrl(mongoUrl);
			var client = new MongoClient(settings);
			_database = client.GetDatabase(databaseName);
		}

		public IMongoDatabase GetDatabase()
		{
			return _database;
		}
	}
}
