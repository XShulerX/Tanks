using System.IO;
using UnityEngine;

namespace MVC
{
    public class JsonData<T> : IData<T>
    {
        public void Save(T data, string path = null)
        {
            var saveString = JsonUtility.ToJson(data);
            File.WriteAllText(path, saveString);
        }

        public T Load(string path = null)
        {
            var loadString = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(loadString);
        }
    }
}