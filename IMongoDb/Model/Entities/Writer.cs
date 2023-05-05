using System.Text.Json.Serialization;
using IMongoDb.Collections;

namespace IMongoDb.Entities
{
	public class Writer : Principal
	{
		private Principal principal;

		[JsonIgnore]
		private Writers writers;

	}

}

