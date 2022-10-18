
using Autofac;
using Etkinlik.API.Helper;
using Etkinlik.Core.IUnitOfWork;
using Etkinlik.Core.Repositories;
using Etkinlik.Core.Services;
using Etkinlik.Repository.Context;
using Etkinlik.Repository.Repositories;
using Etkinlik.Repository.UnitOfWork;
using Etkinlik.Service.Mapping;
using Etkinlik.Service.Services;
using System.Reflection;
using Module = Autofac.Module;
namespace Etkinlik.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();
            //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();
        }
    }
}
