using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Pattern.IOC
{
    public class SimpleIOC : ISimpleIOC
    {
        private IDictionary<Type, object> _instances;
        private IDictionary<Type, Type> _mappers;
        private IDictionary<Type, Func<object>> _builders;

        public SimpleIOC()
        {
            _instances = new Dictionary<Type, object>();
            _mappers = new Dictionary<Type, Type>();
            _builders = new Dictionary<Type, Func<object>>();
        }

        public void Register<TResource>()
        {
            _instances.Add(typeof(TResource), null);
        }

        public void Register<TResource>(Func<TResource> builder)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            Register<TResource>();
            _builders.Add(typeof(TResource), () => builder());
        }

        public void Register<TInterface, TResource>()
            where TResource : TInterface
        {
            Type interfaceType = typeof(TInterface);
            
            if (!interfaceType.IsInterface)
                throw new InvalidOperationException("TInterface must be an interface!!");

            Register<TInterface>();
            _mappers.Add(interfaceType, typeof(TResource));
        }

        public void Register<TInterface, TResource>(Func<TInterface> builder)
            where TResource : TInterface
        {
            Type interfaceType = typeof(TInterface);

            if (!interfaceType.IsInterface)
                throw new InvalidOperationException("TInterface must be an interface!!");

            Register<TInterface>();
            _builders.Add(typeof(TInterface), () => builder());
        }

        public TResource GetResource<TResource>()
        {
            Type type = typeof(TResource);            

            if (!(_instances[type] is null))
                return (TResource)_instances[type];

            if (_builders.ContainsKey(type))
            {
                _instances[type] = _builders[type].Invoke();
                return (TResource)_instances[type];
            }
            else
            {
                Type concreteType = type;
                if(_mappers.ContainsKey(type))
                {
                    concreteType = _mappers[type];
                }
                _instances[type] = Activator.CreateInstance(concreteType);
                return (TResource)_instances[type];
            }
        }

        //private object Resolve(Type type)
        //{
                            
        //}
    }
}
