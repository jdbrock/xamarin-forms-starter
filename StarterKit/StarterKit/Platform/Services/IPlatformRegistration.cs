﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarterKit.Services
{
    public interface IPlatformRegistration
    {
        void Register(ContainerBuilder builder);
    }
}
