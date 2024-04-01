using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DevExercise.Util
{
    public class JsonHandler : IJsonHandler
    {
        private readonly ILogger _logger;

        public JsonHandler(ILogger logger)
        {
            _logger = logger;
        }

        private string ReadJsonFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error reading file.");
                throw;
            }
        }

        private void WriteJsonFile(string filePath, string json)
        {
            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating file.");
                throw;
            }
        }

        public T DeserializeObject<T>(string filePath)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(ReadJsonFile(filePath));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error parsing JSON file.");
                throw;
            }
        }

        public void SerializeObject<T>(string filePath, T obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                WriteJsonFile(filePath, json);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error serializing object.");
                throw;
            }
        }
    }
}
