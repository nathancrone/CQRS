using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CQRS.AWS.Activity1Console.Shared
{
    class Utils
    {
        public static T DeserializeFromJSON<T>(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T inputs = serializer.Deserialize<T>(json);
            return inputs;
        }

        public static string SerializeToJSON<T>(T inputs)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            StringBuilder builder = new StringBuilder();
            serializer.Serialize(inputs, builder);
            return builder.ToString();
        }
    }
}
