using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MooCleanCode.Infrastructure.Dtos;

public class PlayerDto
{
    [BsonId] 
    public ObjectId Id { get; set; }

    [BsonElement("name")] 
    public string Name { get; set; }

    [BsonElement("numberOfGuesses")] 
    public int NumberOfGuesses { get; set; }
}