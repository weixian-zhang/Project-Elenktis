using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Elenktis.Configuration
{
    public class TestSecretHydrator : ISecretHydrator
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
                    configObjectProps[i].SetValue(configObject, "442dcbee-62da-4462-b847-32a8003343f2");
                if(prop.Name == "ClientSecret")
                    configObjectProps[i].SetValue(configObject, "A4ATErF:/cbv*-EAr9TdJhMAtpt1Kku2");
                if(prop.Name == "EtcdHost")
                    configObjectProps[i].SetValue(configObject, "localhost");
                if(prop.Name == "EtcdPort")
                    configObjectProps[i].SetValue(configObject, 2379);
                
                i++;
            }

            return configObject;

        }
    }
}