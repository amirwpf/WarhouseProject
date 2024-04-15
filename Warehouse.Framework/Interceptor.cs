using System;
using System.Diagnostics;
using Castle.Core.Interceptor;

namespace App.Framework
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine($"Before target call {invocation.Method.Name}");
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Target exception {ex.Message}");
                throw;
            }
            finally
            {
                Debug.WriteLine($"After target call {invocation.Method.Name}");
            }
        }
    }
}
