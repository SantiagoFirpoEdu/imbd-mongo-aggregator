using System.Text.Json.Serialization;
using ImdbToJson.Collections;

namespace ImdbToJson.Entities
{
	public class Writer : Principal
	{
		private Principal principal;

		[JsonIgnore]
		private Writers writers;

	}

}

