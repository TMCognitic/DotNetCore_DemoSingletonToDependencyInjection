using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Pattern.IOC
{
    public interface ISimpleIOC
    {
        void Register<TResource>();
        void Register<TResource>(Func<TResource> builder);
        void Register<TInterface, TResource>()
              where TResource : TInterface;
        void Register<TInterface, TResource>(Func<TInterface> builder)
            where TResource : TInterface;

        TResource GetResource<TResource>();
    }
}
