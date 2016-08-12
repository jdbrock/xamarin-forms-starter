using Autofac;
using StarterKit.Services;
using StarterKit.ViewModels;
using StarterKit.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StarterKit
{
    public static class Bootstrap
    {
        public static void Run(App app)
        {
            // Register platform services.
            var builder = new ContainerBuilder();
            RegisterPlatform(builder);

            // Register app services.
            RegisterApp(builder);

            // Realise container.
            var container = builder.Build();

            // Register views.
            RegisterViews(container);

            // Show initial view.
            ShowView<SampleViewModel>(container, app);
        }

        private static void RegisterPlatform(ContainerBuilder builder)
        {
            builder.RegisterType<ViewService>().As<IViewService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.Register<INavigation>(context => App.Current.MainPage.Navigation).SingleInstance();
        }

        private static void RegisterApp(ContainerBuilder builder)
        {
            // You can add your own app services, view models & view registrations here.
            builder.RegisterType<SampleViewModel>();
            builder.RegisterType<SampleView>();
        }

        private static void RegisterViews(IContainer container)
        {
            var viewService = container.Resolve<IViewService>();

            // You can add your view model -> registrations here.
            viewService.Register<SampleViewModel, SampleView>();
        }

        private static void ShowView<T>(IContainer container, App app)
            where T : class, IViewModel
        {
            var viewService = container.Resolve<IViewService>();
            var view = viewService.Resolve<T>();

            app.MainPage = view;
        }
    }
}
