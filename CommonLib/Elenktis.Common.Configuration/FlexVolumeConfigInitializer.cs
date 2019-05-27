﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elenktis.Common.Configuration
{
    public class FlexVolumeConfigInitializer : IConfigInitializer
    {
        const string AKVVolumeMountPath = "/akvsecrets";

        public T Initialize<T>() where T : class
        {
            T configObject = (T)Activator.CreateInstance(typeof(T));
            var configObjectProps = configObject.GetType().GetProperties().ToArray();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            int i = 0;

            foreach (var prop in configTypeProperties)
            {
                

                string secretPath = Path.Combine(AKVVolumeMountPath, prop.Name);

                string secret = File.ReadAllText(secretPath);

                configObjectProps[i].SetValue(configObject, secret);

                i++;
            }

            return configObject;
        }

        public bool UsePropertyNameAsSecretName { get; set; }
    }
}
