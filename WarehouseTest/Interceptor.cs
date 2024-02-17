using System;
using Castle.Core.Interceptor;

namespace WarehouseTest
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"Before target call {invocation.Method.Name}");
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Target exception {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine($"After target call {invocation.Method.Name}");
            }
        }
    }
}
