using System;

namespace Lab8
{
    public class Player
    {
        public int Id { get; }
        public string Name { get; }

        public Player(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString() => $"{Name} (ID: {Id})";
    }
}