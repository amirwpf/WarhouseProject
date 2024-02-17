
using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using WarehouseTest;

public class ProxyFactory
{
    private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();
    private readonly Dictionary<Type, object> _registeredInstances = new Dictionary<Type, object>();

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        if (!_registeredInstances.ContainsKey(typeof(TInterface)))
        {
            _registeredInstances[typeof(TInterface)] = new TImplementation();
        }
    }

    public TInterface Resolve<TInterface>()
    {
        if (_registeredInstances.TryGetValue(typeof(TInterface), out var instance))
        {
            return (TInterface)instance;
        }

        var interceptor = new Interceptor();
        return _proxyGenerator.CreateInterfaceProxyWithTarget<TInterface>(Activator.CreateInstance<TInterface>(), interceptor);
    }
}
