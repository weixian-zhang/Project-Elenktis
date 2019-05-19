using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elenktis.Common.Configuration
{
    public class FlexVolumeConfigInitializer : IConfigInitializer
    {
        const string AKVVolumeMountPath = "/akvmnt";

        public T Initialize<T>() where T : class
        {
            T configObject = (T)Activator.CreateInstance(typeof(T));
            var configObjectProperties = configObject.GetType().GetProperties();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            foreach (var prop in configTypeProperties)
            {
                string secretPath = Path.Combine(AKVVolumeMountPath, prop.Name);

                string secret = File.ReadAllText(secretPath);

                foreach(var objProp in configTypeProperties)
                {
                    objProp.SetValue(configObject, secret);
                }
            }

            return configObject;
        }

        public bool UsePropertyNameAsSecretName { get; set; }
    }
}
