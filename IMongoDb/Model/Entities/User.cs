using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities;

[BsonDiscriminator("User")]
public class User
{
	public static User GetFakeUser()
	{
		string email = Faker.Internet.Email();
		BsonDateTime birthDate = new(Faker.Identification.DateOfBirth());
		string gender = Faker.Boolean.Random() ? "female" : "male";
		string country = Faker.Address.Country();

		return new User(email, birthDate, gender, country);
	}

	[BsonId]
	public string Email { get; }
	
	private User(string email, BsonDateTime birthDate, string gender, string country)
	{
		this.Email = email;
		this.birthDate = birthDate;
		this.gender = gender;
		this.country = country;
	}

	[BsonElement]
	private BsonDateTime birthDate;
	
	[BsonElement]
	private string gender;
	
	[BsonElement]
	private string country;
}

