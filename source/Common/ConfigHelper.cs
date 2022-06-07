using System.Runtime.Serialization;
using System.Text.Json;

namespace Common
{
    public class ConfigHelper
    {
        /// <summary>
        /// Read the application config from json file.
        /// </summary>
        /// <param name="pathToJsonFile">Path to the json file.</param>
        /// <returns>The loaded configuration in a type T object.</returns>
        public static T LoadFromJsonFile<T>(string pathToJsonFile)
        {
            var result = JsonSerializer.Deserialize<T>(File.ReadAllText(pathToJsonFile));

            if (result != null)
            {
                return result;
            }
            throw new SerializationException($"Could not deserialize config file '{pathToJsonFile}'.");
        }
    }
}
