using System.Collections.Generic;

namespace LoggingSystem.Models
{
    public class KeyValueCollection : List<KeyValue>
    {

        public KeyValueCollection( params KeyValue[] keyValues)
        {
            this.AddRange(keyValues);
        }

    }
}