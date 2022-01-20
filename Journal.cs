using System;
using System.Collections.Generic;
using System.Text;

namespace lab3sh
{
    public class Journal
    {
        private List<JournalEntry> journalEntries = new();
        
        public void OnStudentChange(object subject, EventArgs e)
        {
            var it = e as StudentsChangedEventArgs<string>;
            journalEntries.Add(new JournalEntry(it.CollectionName, it.ActionType, it.PropertyName, it.ChangedElementKey));
        }
        
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (var change in journalEntries)
            {
                str.Append(change + "\n\n");
            }

            return str.ToString();
        }
    }
}