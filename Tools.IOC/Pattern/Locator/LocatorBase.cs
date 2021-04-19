using System;
using System.Collections.Generic;
using System.Text;
using Tools.Pattern.IOC;

namespace Tools.Pattern.Locator
{
    public abstract class LocatorBase
    {
        protected ISimpleIOC Container { get; private set; }

        protected LocatorBase() : this(new SimpleIOC())
        {

        }

        protected LocatorBase(ISimpleIOC container)
        {
            Container = container;
            ConfigureServices();
        }

        protected abstract void ConfigureServices();
    }
}
