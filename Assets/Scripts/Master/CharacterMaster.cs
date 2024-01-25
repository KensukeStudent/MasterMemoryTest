using MessagePack;
using MasterMemory;

[MemoryTable("character"), MessagePackObject(true)]
public class CharacterMaster
{
    [PrimaryKey]
    public int Id { get; }

    public string Name { get; }

    public int Attack { get; }

    public CharacterMaster(int id, string name, int attack)
    {
        Id = id;
        Name = name;
        Attack = attack;
    }
}
