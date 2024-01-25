using MessagePack;
using MasterMemory;

public enum Gender
{
    Male, Female, Unknown
}

// table definition marked by MemoryTableAttribute.
// database-table must be serializable by MessagePack-CSsharp
[MemoryTable("person"), MessagePackObject(true)]
public class Person
{
    // index definition by attributes.
    [PrimaryKey]
    public int PersonId { get; }

    // secondary index can add multiple(discriminated by index-number).
    [SecondaryKey(0), NonUnique]
    [SecondaryKey(1, keyOrder: 1), NonUnique]
    public int Age { get; }

    [SecondaryKey(2), NonUnique]
    [SecondaryKey(1, keyOrder: 0), NonUnique]
    public Gender Gender { get; }

    public string Name { get; }

    public Person(int personId, int age, Gender gender, string name)
    {
        PersonId = personId;
        Age = age;
        Gender = gender;
        Name = name;
    }
}

// ※ コンストラクターの変数はプロパティ名と一致させる必要がある (小文字でもよい)