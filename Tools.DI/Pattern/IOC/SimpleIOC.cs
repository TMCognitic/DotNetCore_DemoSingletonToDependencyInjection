using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Type type = typeof(TResource); //Récupère par réflection la déclaration du type            

            if (!(_instances[type] is null))
                return (TResource)_instances[type];

            if (_builders.ContainsKey(type))
            {
                _instances[type] = _builders[type].Invoke();
                return (TResource)_instances[type];
            }
            else
            {
                _instances[type] = Resolve(type);
                return (TResource)_instances[type];
            }
        }

        private object Resolve(Type resourceType)
        {
            if (!(_instances[resourceType] is null))
            {
                return _instances[resourceType];
            }
            else
            {
                if (_builders.TryGetValue(resourceType, out Func<object> builder))
                {
                    return builder.Invoke();
                }

                Type concreteType = resourceType;
                if (_mappers.ContainsKey(concreteType))
                    concreteType = _mappers[concreteType];

                ConstructorInfo constructorInfo = concreteType.GetConstructors().SingleOrDefault();

                if (!(constructorInfo is null))
                {
                    object[] parameters = constructorInfo.GetParameters()
                                                         .Select(p => Resolve(p.ParameterType))
                                                         .ToArray();

                    return (_instances[resourceType] = constructorInfo.Invoke(parameters));
                }

                PropertyInfo propertyInfo = concreteType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);

                if (propertyInfo != null)
                    return (_instances[resourceType] = propertyInfo.GetMethod.Invoke(null, null));

                FieldInfo fieldInfo = concreteType.GetField("Instance", BindingFlags.Public | BindingFlags.Static);

                if (fieldInfo != null)
                    return (_instances[resourceType] = fieldInfo.GetValue(null));

                throw new InvalidOperationException($"Can't initialize the type {resourceType.Name}");
            }
        }
    }
}
