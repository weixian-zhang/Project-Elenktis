using System;
using System.Collections.Generic;
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
            var configObjectProps = configObject.GetType().GetProperties().ToArray();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            //get from ASP.NET Core Secret Manager
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets(Assembly.GetCallingAssembly());
            Configuration = builder.Build();

            int i = 0;

            foreach (var prop in configTypeProperties)
            {
                string secret = Configuration[prop.Name];

                configObjectProps[i].SetValue(configObject, secret);

                i++;
            }

            return configObject;
        }

        private Assembly _assembly;
    }
}
