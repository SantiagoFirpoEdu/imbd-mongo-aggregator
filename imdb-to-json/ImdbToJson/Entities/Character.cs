using System.Collections.ObjectModel;
using ImdbToJson.Collections;

namespace ImdbToJson.Entities;

public class Character
{
	private string _id;
	private string name;
	private CrewMember crewMember;
	private Characters characters;
	private Collection<Role> role;
}
