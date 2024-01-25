// <auto-generated />
#pragma warning disable CS0105
using MasterMemory.Validation;
using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using Example.Tables;

namespace Example
{
   public sealed class ImmutableBuilder : ImmutableBuilderBase
   {
        MemoryDatabase memory;

        public ImmutableBuilder(MemoryDatabase memory)
        {
            this.memory = memory;
        }

        public MemoryDatabase Build()
        {
            return memory;
        }

        public void ReplaceAll(System.Collections.Generic.IList<Person> data)
        {
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

        public void RemovePerson(int[] keys)
        {
            var data = RemoveCore(memory.PersonTable.GetRawDataUnsafe(), keys, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

        public void Diff(Person[] addOrReplaceData)
        {
            var data = DiffCore(memory.PersonTable.GetRawDataUnsafe(), addOrReplaceData, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.PersonId, System.Collections.Generic.Comparer<int>.Default);
            var table = new PersonTable(newData);
            memory = new MemoryDatabase(
                table
            
            );
        }

    }
}