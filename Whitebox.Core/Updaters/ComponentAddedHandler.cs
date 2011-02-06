﻿using System;
using System.Linq;
using Whitebox.Core.Application;
using Whitebox.Messages;

namespace Whitebox.Core.Updaters
{
    class ComponentAddedHandler : IUpdateHandler<ComponentAddedMessage>
    {
        readonly IActiveItemRepository<Component> _components;
        readonly IActiveItemRepository<TypeData> _types;

        public ComponentAddedHandler(IActiveItemRepository<Component> components, IActiveItemRepository<TypeData> types)
        {
            if (components == null) throw new ArgumentNullException("components");
            if (types == null) throw new ArgumentNullException("types");
            _components = components;
            _types = types;
        }

        public void UpdateFrom(ComponentAddedMessage message)
        {
            if (message == null) throw new ArgumentNullException("message");

            TypeData limitType;
            if (!_types.TryGetItem(message.Component.LimitTypeId, out limitType))
                throw new InvalidOperationException("The component depends on an unknown type.");

            var component = new Component(
                message.Component.Id,
                limitType,
                message.Component.Services.Select(svc => {
                    TypeData serviceType;
                    if (!_types.TryGetItem(svc.ServiceTypeId, out serviceType))
                        throw new InvalidOperationException("The service provides an unknown type.");
                    return new Service(svc.Description, serviceType, svc.Key);
                }),
                message.Component.Ownership);
            _components.Add(component);
        }
    }
}
