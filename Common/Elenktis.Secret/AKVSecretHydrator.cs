using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elenktis.Secret
{
    public class AKVSecretHydrator : ISecretHydrator
    {
        const string AKVVolumeMountPath = "/akv"; //path must match flexvolume mountpath

        public T Hydrate<T>() where T : class
        {
            T configObject = (T)Activator.CreateInstance(typeof(T));
            var configObjectProps = configObject.GetType().GetProperties().ToArray();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            int i = 0;

            foreach (var prop in configTypeProperties)
            {
                string secretFilePath =
                    Path.Combine($"{AKVVolumeMountPath}{Path.AltDirectorySeparatorChar}{prop.Name}");

                string secret = File.ReadAllText(secretFilePath);

                configObjectProps[i].SetValue(configObject, secret);

                i++;
            }

            return configObject;
        }

        public bool UsePropertyNameAsSecretName { get; set; }
    }
}
