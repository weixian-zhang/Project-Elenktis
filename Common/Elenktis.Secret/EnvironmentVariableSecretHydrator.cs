using System;
using System.ComponentModel;

namespace Elenktis.Secret
{
    public class EnvironmentVariableSecretHydrator : ISecretHydrator
    {
        public T Hydrate<T>() where T : class
        {
           object secretObj = Activator.CreateInstance(typeof(T));
           PropertyDescriptorCollection props = TypeDescriptor.GetProperties(secretObj);

           foreach (PropertyDescriptor pd in props)
           {
               string envSecret = Environment.GetEnvironmentVariable(pd.Name);

                pd.SetValue(secretObj, envSecret);
           };
           
           return (T)secretObj;
        }
    }
}