namespace lab3sh
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public Action ActionType { get; set; }
        public string PropertyName { get; set; }
        public string ChangedElementString { get; set; }

        public JournalEntry(string collectionName, Action actionType, string propertyName,
            string changedElementString)
        {
            CollectionName = collectionName;
            ActionType = actionType;
            PropertyName = propertyName;
            ChangedElementString = changedElementString;
        }
        
        public override string ToString()
        {
            return $"Collection: {CollectionName}\n" +
                   $"Action{ActionType}\n" +
                   $"Property caused elements changes:{PropertyName}\n" +
                   $"Key of changed element: {ChangedElementString}";
        }
    }
}