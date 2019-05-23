using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Elenktis.Common.Configuration
{
    public class YamlConfigLoader
    {
        /// <summary>
        /// Gets the default file name 'config.yaml' from executing directory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>()
        {
            string yamlConfigStr = File.ReadAllText("config.yaml");

            var deserializer = new DeserializerBuilder().Build();

            return deserializer.Deserialize<T>(yamlConfigStr);
        }
    }
}
