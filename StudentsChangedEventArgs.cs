namespace lab3sh
{
    public class StudentsChangedEventArgs<TKey>: System.EventArgs
    {
        public string CollectionName { get; set; }
        public Action ActionType { get; set; }
        public string PropertyName { get; set; }
        public TKey ChangedElementKey { get; set; }

        public StudentsChangedEventArgs(string collectionName, Action actionType, string propertyName,
            TKey changedElementKey)
        {
            CollectionName = collectionName;
            ActionType = actionType;
            PropertyName = propertyName;
            ChangedElementKey = changedElementKey;
        }

        public override string ToString()
        {
            return $"Collection: {CollectionName}\n" +
                   $"Action{ActionType}\n" +
                   $"Property:{PropertyName}\n" +
                   $"Key of changed element: {ChangedElementKey}";
        }
    }
}