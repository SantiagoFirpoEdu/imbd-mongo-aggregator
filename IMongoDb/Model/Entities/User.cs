using IMongoDb.Collections;

namespace IMongoDb.Entities;

public class User
{
	private String email;

	private DateOnly birthDate;

	private String gender;

	private String country;

	private UserRating[] userRating;

	private Users users;
}