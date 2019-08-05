using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elenktis.Configuration
{
    public class ManualSecretHydrator : ISecretHydrator
    {
        public T Hydrate<T>() where T : class 
        {
            T configObject = (T)Activator.CreateInstance(typeof(T));
            var configObjectProps = configObject.GetType().GetProperties().ToArray();

            PropertyInfo[] configTypeProperties = typeof(T).GetProperties();

            int i = 0;
            foreach(var prop in configTypeProperties)
            {
                if(prop.Name == "TenantId")
                    configObjectProps[i].SetValue(configObject, "fc418f16-5c93-437d-b743-05e9e2a04d93");
                 if(prop.Name == "ClientId")
                    configObjectProps[i].SetValue(configObject, "");
                if(prop.Name == "ClientSecret")
                    configObjectProps[i].SetValue(configObject, "");
                if(prop.Name == "EtcdHost")
                    configObjectProps[i].SetValue(configObject, "http://127.0.0.1");
                if(prop.Name == "EtcdPort")
                    configObjectProps[i].SetValue(configObject, "2379");
                
                i++;
            }

            return configObject;

        }
    }
}