using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace App.Framework
{
    public class ServiceFactory
    {
        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();
        private readonly Dictionary<Type, Lazy<object>> _registeredInstances = new Dictionary<Type, Lazy<object>>();

        public ServiceFactory()
        {
            Type baseInterfaceType = typeof(IBaseService);
            List<Type> implementationInterfaceTypes = FindInterfaceType(baseInterfaceType);

            foreach (var interfaceType in implementationInterfaceTypes)
            {
                Register(interfaceType);
            }
        }

        private void Register(Type tInterface)
        {
            Type implementationType = FindImplementationType(tInterface);

            if (!_registeredInstances.ContainsKey(tInterface) && implementationType != null)
            {
                _registeredInstances[tInterface] = new Lazy<object>(() => Activator.CreateInstance(implementationType));
            }
        }

        public TInterface Resolve<TInterface>()
        {
            Type interfaceType = typeof(TInterface);

            if (_registeredInstances.TryGetValue(interfaceType, out var lazyInstance))
            {
                return (TInterface)lazyInstance.Value;
            }

            Type implementationType = FindImplementationType(interfaceType);

            if (implementationType != null)
            {
                var interceptor = new Interceptor();
                return (TInterface)_proxyGenerator.CreateInterfaceProxyWithoutTarget(
                    interfaceType, interceptor);
            }

            throw new InvalidOperationException($"No implementation found for interface {interfaceType.FullName}");
        }

        private Type FindImplementationType(Type interfaceType)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var dllFiles = Directory.GetFiles(path, "*.dll");

            foreach (var dllFile in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);
                    var types = assembly.GetTypes();

                    var filteredTypes = types
                        .Where(t => t.IsClass && !t.IsAbstract && interfaceType.IsAssignableFrom(t))
                        .ToList();

                    if (filteredTypes.Count > 0)
                    {
                        return filteredTypes.First();
                    }
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }

        private List<Type> FindInterfaceType(Type baseInterfaceType)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var dllFiles = Directory.GetFiles(path, "*.dll");
            List<Type> result = new List<Type>();

            foreach (var dllFile in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dllFile);
                    var types = assembly.GetTypes();
                    var filteredTypes = types
                        .Where(t => t.IsInterface && baseInterfaceType.IsAssignableFrom(t) && t.Name != "IBaseService")
                        .ToList();

                    result.AddRange(filteredTypes);
                }
                catch
                {
                    continue;
                }
            }

            return result;
        }
    }
}
