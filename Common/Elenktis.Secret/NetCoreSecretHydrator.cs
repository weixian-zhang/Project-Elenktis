using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace Elenktis.Secret
{
    //reads secrets from ASP.NET Core Secret Manager
    public class NetCoreSecretHydrator : ISecretHydrator
    {
        const string AKVVolumeMountPath = "/akv"; //path must match flexvolume mountpath
        public static IConfigurationRoot Configuration { get; set; }

        public NetCoreSecretHydrator()
        {
        }

        public T Hydrate<T>() where T : class
        {
            T configObject = (T)Activator.CreateInstance(typeof(T));
            //var configObjectProps = configObject.GetType().GetProperties().ToArray();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            //get from ASP.NET Core Secret Manager
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(Assembly.GetCallingAssembly());
            Configuration = builder.Build();

            foreach(var config in Configuration.AsEnumerable())
            {
                var secretProp = configTypeProperties.FirstOrDefault(p => p.Name == config.Key);
                
                if(config.Value.GetType() == typeof(int))
                    secretProp.SetValue(configObject, Convert.ToInt32(config.Value));
                else if(config.Value.GetType() == typeof(bool))
                    secretProp.SetValue(configObject, Convert.ToBoolean(config.Value));
                else
                    secretProp.SetValue(configObject, config.Value);
            }

            

            // foreach (var prop in configTypeProperties)
            // {
            //     object secret = Configuration[prop.Name];

            //     configObjectProps[i].SetValue(configObject, secret);

            //     i++;
            // }

            return configObject;
        }

        private Assembly _assembly;
    }
}
