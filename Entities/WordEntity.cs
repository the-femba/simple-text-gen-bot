using Lotos.Abstractions.Database;

namespace FemboyDev.TextGenBot.Entities;

internal sealed class WordEntity : Entity<WordEntity>
{
	public WordEntity(string value, List<Guid>? dependencies = null)
	{
		Value = value;
		Dependencies = dependencies ?? new List<Guid>();
	}

	public string Value { get; set; }
	
	public List<Guid> Dependencies { get; set; }
}