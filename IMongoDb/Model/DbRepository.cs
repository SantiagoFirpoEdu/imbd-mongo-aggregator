using IMongoDb.Collections;
using IMongoDb.Model.Collections;

namespace IMongoDb.Model;

public class DbRepository
{
    public Actors Actors { get; } = new();
    public Characters Characters { get; } = new();
    public CrewMembers CrewMembers { get; } = new();
    public Directors Directors { get; } = new();
    public Episodes Episodes { get; } = new();
    public Genres Genres { get; } = new();
    public Jobs Jobs { get; } = new();
    public Movies Movies { get; } = new();
    public Principals Principals { get; } = new();
    public Shows Shows { get; } = new();
    public Titles Titles { get; } = new();
    public UserRatings UserRatings { get; } = new();
    public Users Users { get; } = new();
}