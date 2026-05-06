using System;
using System.Collections.Generic;

namespace Lab8
{
    public sealed record HistoryEntry(string Player, string Game, string Result, DateTime Time);

    public static class GameHistory
    {
        private static readonly List<HistoryEntry> _entries = new();

        public static event EventHandler<HistoryEntry>? EntryAdded;

        public static IReadOnlyList<HistoryEntry> Entries
        {
            get
            {
                lock (_entries)
                {
                    return _entries.AsReadOnly();
                }
            }
        }

        public static void AddEntry(HistoryEntry entry)
        {
            if (entry is null) throw new ArgumentNullException(nameof(entry));
            lock (_entries)
            {
                _entries.Add(entry);
            }

            EntryAdded?.Invoke(null, entry);
        }
    }
}