using System;

namespace DevExercise.Util
{
    public interface IJsonHandler
    {
        T DeserializeObject<T>(string filePath);

        void SerializeObject<T>(string filePath, T projectDetails);
    }
}
