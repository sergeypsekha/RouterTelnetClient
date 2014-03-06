using System.Collections.Generic;
using System.Configuration;

namespace RouterTelnetClient.Configuration
{
    public class GenericConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T>
        where T : ConfigurationElement, new()
    {
        private readonly List<T> elements = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            var element = new T();
            this.elements.Add(element);
            return element;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return this.elements.Find(e => e.Equals(element));
        }
    }
}
