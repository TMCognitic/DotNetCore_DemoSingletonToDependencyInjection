using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Pattern.Locator
{
    public abstract class LocatorBase
    {
        protected IServiceProvider Container { get; private set; }

        protected LocatorBase()
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            Container = services.BuildServiceProvider();
        }


        protected abstract void ConfigureServices(IServiceCollection services);
    }
}
