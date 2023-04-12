using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Core.MongoDb.Context
{
	public interface IMongoDbContext
	{
		IMongoDatabase GetDatabase();
	}
}
