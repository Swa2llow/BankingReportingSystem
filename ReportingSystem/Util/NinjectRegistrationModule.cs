using Ninject.Modules;
using ReportingSystem.BLL.Interfaces;
using ReportingSystem.BLL.Services;
using System;
using System.Collections.Generic;

namespace ReportingSystem.Util
{
    public class NinjectRegistrationModule : NinjectModule
    {
        public interface IDependencyResolver : IDependencyScope, IDisposable
        {
            IDependencyScope BeginScope();
        }

        public interface IDependencyScope : IDisposable
        {
            object GetService(Type serviceType);
            IEnumerable<object> GetServices(Type serviceType);
        }

        public override void Load()
        {
            Bind<ICustomerService>().To<CustomerService>();
            Bind<ICreditCardService>().To<CreditCardService>();
            Bind<ITransactionService>().To<TransactionService>();
        }
    }
}