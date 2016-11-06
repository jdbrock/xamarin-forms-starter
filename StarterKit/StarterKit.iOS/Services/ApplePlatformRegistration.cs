using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using StarterKit.Services;
using StarterKit.iOS.Services;

namespace StarterKit.iOS.Services
{
    public class ApplePlatformRegistration : IPlatformRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AppleOrientationService>()
                .As<IOrientationService>()
                .AsSelf()
                .SingleInstance();
        }
    }
}