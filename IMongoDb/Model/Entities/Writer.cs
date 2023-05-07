using IMongoDb.Model.TsvRecords;
using IMongoDb.Monads;
using MongoDB.Bson.Serialization.Attributes;

namespace IMongoDb.Model.Entities
{
	[BsonDiscriminator("Writer")]
	public class Writer
	{
		public static Result<Writer, EWriterConversionError> FromPrincipal(TitlePrincipal principal)
		{
			if (!principal.IsWriter())
			{
				return Result<Writer, EWriterConversionError>.Error(EWriterConversionError.NotAWriter);
			}

			Writer result = new(principal.tconst);
			return Result<Writer, EWriterConversionError>.Ok(result);

		}

		private Writer(string id)
		{
			Id = id;
		}

		[BsonId]
		public string Id { get; }
	}

	public enum EWriterConversionError
	{
		NotAWriter
	}
}

