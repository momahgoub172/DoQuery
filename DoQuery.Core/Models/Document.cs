using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoQuery.Core.Models
{
    /// <summary>
    /// represents a document to be indexed and searched
    /// </summary>
    public class Document
    {

        /// <summary>
        /// Unique identifier for the document.
        /// </summary>
        public string Id { get; set; }


        /// <summary>
        /// Fields containing content of the document
        /// </summary>
        public Dictionary<string, object> Fields { get; set; }


        /// <summary>
        /// Creates a new instance of the <see cref="Document"/> class with the specified ID.
        /// </summary>
        /// <param name="id">The unique identifier for the document.</param>
        public Document(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id), "Document ID cannot be null or empty.");
            }

            Id = id;
            Fields = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Adds or updates a field in the document.
        /// </summary>
        /// <param name="name">Field name</param>
        /// <param name="value">Field value</param>
        /// <returns>The document (for method chaining)</returns>
        public Document AddField(string name, object value)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "Field name cannot be null or empty.");
            }

            Fields[name] = value;
            return this;
        }

        /// <summary>
        /// Gets a field value by name.
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns>Field value or null if not found</returns>
        public object GetField(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return null;
            }

            return Fields[name];
        }

        /// <summary>
        /// Gets a field value as a string.
        /// </summary>
        /// <param name="name">Field name</param>
        /// <returns>String value or empty string if not found</returns>
        public string GetFieldAsString(string name)
        {
            var value = GetField(name);
            return value?.ToString() ?? string.Empty;
        }


        /// <summary>
        /// Returns a readable representation of the document.
        /// </summary>
        public override string ToString()
        {
            return $"Document[{Id}] with {Fields.Count} fields";
        }
    }
}
