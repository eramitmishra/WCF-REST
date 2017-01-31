using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace studentapiusingmongoDB
{

 

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string  GetData()
        {


            MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
            var database = client.GetDatabase("StudentDB");
            var collection = database.GetCollection<BsonDocument>("Student");
            var document = collection.Find(new BsonDocument()).ToList();
            return  document.ToString();

            /*string connectionString = "mongodb://host:27017";
            Console.WriteLine("Connecting MongoDB");
            MongoClient client = new MongoClient(connectionString);            
            IMongoDatabase mongoDatabase = client.GetDatabase("test");
            var collection = mongoDatabase.GetCollection<BsonDocument>("Student"); 
            var query = new QueryDocument("Class", "12th");
            var sgssv = collection.Find(query);
            List<BsonDocument> ls = new List<BsonDocument>();
            await collection.InsertOneAsync(new BsonDocument { { "Id", "25" }, { "Name", "Karan" }, { "Class", "8th" } });
            return ls.ToString();*/
        }


        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
