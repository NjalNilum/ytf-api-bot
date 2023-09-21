// See https://aka.ms/new-console-template for more information

using MongoDB.Bson;
using MongoDB.Driver;



var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
IMongoDatabase db = dbClient.GetDatabase("MyAss");

var collection = db.GetCollection<Penis>("penes");
var p = new Penis
        {
            Diameter = "9",
            Length = "18"
        };
collection.InsertOne(p);

var command = new BsonDocument { { "dbstats", 1 } };
var result = db.RunCommand<BsonDocument>(command);
Console.WriteLine(result.ToJson());


var dbList = dbClient.ListDatabases().ToList();

Console.WriteLine("The list of databases are:");

foreach (var item in dbList)
{
    Console.WriteLine(item);
}

Console.ReadLine();

public class Penis
{
    public string Length { get; set; }
    public string Diameter { get; set; }
}
