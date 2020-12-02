[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Sahika.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Sahika.App_Start.NinjectWebCommon), "Stop")]

namespace Sahika.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Sahika.DataAccess.Abstract;
    using Sahika.DataAccess.Concrete;
    using Sahika.Helper.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IPostRepository>().To<EfPostRepository>();
            kernel.Bind<ICategoryRepository>().To<EfCategoryRepository>();
            kernel.Bind<ISubCategoryRepository>().To<EfSubCategoryRepository>();
            kernel.Bind<ICommentRepository>().To<EfCommentRepository>();
            kernel.Bind<IFavouritePostRepository>().To<EfFavouritePostRepository>();
            kernel.Bind<IPostStatRepository>().To<EfPostStatRepository>();
            kernel.Bind<IMessageRepository>().To<EfMessageRepository>();
           


        }
    }
}