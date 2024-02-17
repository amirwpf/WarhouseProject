using System;
using System.Collections.Generic;

public class Container
{
    private readonly Dictionary<Type, Func<object>> registrations = new Dictionary<Type, Func<object>>();

    public void Register<TInterface, TImplementation>() where TImplementation : TInterface, new()
    {
        registrations[typeof(TInterface)] = () => new TImplementation();
    }

    public TInterface Resolve<TInterface>()
    {
        if (registrations.TryGetValue(typeof(TInterface), out var factory))
        {
            return (TInterface)factory.Invoke();
        }

        throw new InvalidOperationException($"No registration found for {typeof(TInterface)}.");
    }
}