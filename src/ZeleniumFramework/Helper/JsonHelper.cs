using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZeleniumFramework.Helper
{
    public class JsonHelper
    {
        public static List<T> RetrieveJsonObjectList<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString);
        }

        public static List<T> RetrieveJsonObjectList<T>(string jsonString, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<List<T>>(jsonString, converters);
        }

        public static T RetrieveJsonObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static T RetrieveJsonObject<T>(string jsonString, params JsonConverter[] converters)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                Converters = converters,
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
                {
                    IgnoreSerializableInterface = true
                }
            };
            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        public static T RetrieveJsonSubObject<T>(string jsonMessage, string keys)
        {
            JsonReader reader = new JsonTextReader(new StringReader(jsonMessage));
            JObject jObject = JObject.Load(reader);
            JToken token = jObject.SelectToken(keys);
            return token.ToObject<T>();
        }

        public static dynamic RetrieveJsonObject(string jsonString)
        {
            return JsonConvert.DeserializeObject(jsonString);
        }

        public static string SerializeObject(object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}
