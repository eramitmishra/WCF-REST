using System;
using MongoDB.Bson;
using MongoDB.Driver;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ServiceModel;

namespace studentapiusingmongoDB
{
   
    /// <summary>
    /// www.codeproject.com/Articles/254714/Implement-CRUD-operations-using-RESTful-WCF-Service
    //www.abhisheksur.com/2010/10/building-crud-in-restful-services-of.html
    //www.codeproject.com/Articles/115054/Restful-Crud-Operation-on-a-WCF-Service
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "studentapi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select studentapi.svc or studentapi.svc.cs at the Solution Explorer and start debugging.
    public class studentapi : Istudentapi
    {
       
        public async Task<string> getstudents()
        {
            //throw new NotImplementedException();
            try
            {

                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");
                //var datalist = collection.Find(new BsonDocument()).ToList();
                var datalist = await collection.FindAsync(new BsonDocument());
                var StudentCollection = datalist.ToList();
                return StudentCollection.ToJson();
            }
            catch (Exception ex)
            {                
                throw new FaultException("sorry cannot connected");                
            }
        }

        public async Task<string> insertStudent(Student std)
        {
            //throw new NotImplementedException();

            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");
                var bsonStudent = new BsonDocument{
                                { "Name", std.Name },
                                { "RollNo", new BsonInt32(Convert.ToInt32(std.RollNo)) },
                                { "Address", std.Address},
                                { "Class" , std.Class}
                            };

                await collection.InsertOneAsync(bsonStudent);
                /*find*/
                var filter = Builders<BsonDocument>.Filter.Eq("RollNo", new BsonInt32(Convert.ToInt32(std.RollNo)));
                var document = collection.Find(filter).First();
                return document.ToJson();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        





    }
}
