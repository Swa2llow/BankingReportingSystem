using Ninject.Modules;
using ReportingSystem.DAL.Interfaces;
using ReportingSystem.DAL.Repositories;

namespace ReportingSystem.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection) => connectionString = connection;

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
