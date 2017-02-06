using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.ServiceModel;
using System.Reflection;
using MongoDB.Driver.Builders;

namespace studentapiusingmongoDB
{

    /// <summary>
    /// www.codeproject.com/Articles/254714/Implement-CRUD-operations-using-RESTful-WCF-Service
    //https://docs.mongodb.com/getting-started/csharp/query/
    //https://www.google.co.in/webhp?sourceid=chrome-instant&ion=1&espv=2&ie=UTF-8#q=mongodb+query
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "studentapi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select studentapi.svc or studentapi.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior]
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
                return document.ToString();
            }
            catch (Exception ex)
            {

                throw new FaultException("Not Inserted \n" + ex.ToString());
            }
        }

        public async Task<string> getstudentbycategory(string propertyname, string expression)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");

                FilterDefinition<BsonDocument> filter = null;
                if (string.Equals(propertyname,"rollno",StringComparison.OrdinalIgnoreCase))
                {
                    filter = Builders<BsonDocument>.Filter.Eq("RollNo", new BsonInt32(Convert.ToInt32(expression)));
                }
                else
                {
                    //filter = Builders<BsonDocument>.Filter.AnyEq(propertyname, expression);
                    filter = Builders<BsonDocument>.Filter.Eq(propertyname, expression);

                }

                //PropertyInfo propInfo = typeof(Student).GetProperty(propertyname);
                //object bsonValue = Convert.ChangeType(expression, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType);
                //FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq(propInfo.Name, bsonValue);

                //var query = Query.Matches("FirstName", ".*as.*");

                

       
                //collection.FindAsync

                List<BsonDocument> ls = new List<BsonDocument>();
                using (var cursor = await collection.FindAsync(filter))
                {
                    while (await cursor.MoveNextAsync())
                    {
                        var batch = cursor.Current;
                        foreach (var documents in batch)
                        {
                            ls.Add(documents);
                        }
                    }
                }

                if (ls.Count == 0)
                {
                    ls.Add(new BsonDocument { { "Message" , "Record Not Found." } });
                }

                 //return   (ls.Count > 0) ? ls.ToJson() : } ;
               return ls.ToJson();

                //var document = await collection.FindAsync(filter);
                //if (document.ToList().Count == 1) {
                // return document.FirstOrDefault().ToString();
                //}
                //else if (document.ToList().Count > 1)
                //{
                //            List<BsonDocument> ls = new List<BsonDocument>();
                //    using (var cursor = await collection.FindAsync(filter))
                //    {
                //        while (await cursor.MoveNextAsync())
                //        {
                //            var batch = cursor.Current;
                //            foreach (var documents in batch)
                //            {
                //                // process document
                //                //count++;
                //                ls.Add(documents);
                //            }
                //        }
                //    }

                //    return ls.ToJson();
                //}
                //else
                //{
                //    return "not record Found";
                //}
                ////return document.ToListAsync().ToJson();
            }
            catch (Exception ex)
            {
                throw new FaultException("sorry cannot connected");
            }
        }

        public async Task<string> getstudentbyrollnumber(string rollno)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");
                var filter = Builders<BsonDocument>.Filter.Eq("RollNo", new BsonInt32(Convert.ToInt32(rollno)));
                var document = await collection.FindAsync(filter);
                return document.FirstOrDefault().ToString();
            }
            catch (Exception ex)
            {
                throw new FaultException("sorry cannot connected");
            }
        }

        public async Task<string> updatestudentbyrollnumber(Student std)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");
                var filter = Builders<BsonDocument>.Filter.Eq("RollNo", new BsonInt32(Convert.ToInt32(std.RollNo)));

                var update = Builders<BsonDocument>.Update.Set("Name", std.Name)
                    .Set("RollNo", new BsonInt32(Convert.ToInt32(std.RollNo)))
                    .Set("Address", std.Address)
                    .Set("Class", std.Class);

                var document = await collection.UpdateOneAsync(filter, update);

                return string.Format("Number of Update: {1} and Number of Matching Record: {0}.", document.MatchedCount, document.ModifiedCount);


                //return document.ToJson();
            }
            catch (Exception ex)
            {                
                throw new FaultException("Not Update \n" + ex.ToString());
            }
        }

        public async Task<string> deletestudentbyrollnumber(string rollno)
        {
            try
            {
                MongoClient client = new MongoClient("mongodb://localhost:27017");// connect to localhost
                var database = client.GetDatabase("StudentDB");
                var collection = database.GetCollection<BsonDocument>("Student");
                var filter = Builders<BsonDocument>.Filter.Eq("RollNo", new BsonInt32(Convert.ToInt32(rollno)));          
                var document = await collection.DeleteOneAsync(filter);
                return (document.DeletedCount > 0) ? "Record Deleted Successfully" : "Record Not Deleted or It may not be in DataBase";                
            }
            catch (Exception ex)
            {
                throw new FaultException("Not Deleted \n" + ex.ToString());
            }
        }
    }
}
