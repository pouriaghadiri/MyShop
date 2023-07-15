using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Utilities.Extentions.Serialization
{
    public static class SerializationConfig
    {
        public static string SerializeWithCircularReferences(object obj)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // other options as needed
            };

            return JsonSerializer.Serialize(obj, options);
        }

    }
}
