namespace LoggingSystem.Models
{
    public class KeyValue
    {
        public string Key { get; private set; }
        public string Value { get; private set; }

        public KeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}