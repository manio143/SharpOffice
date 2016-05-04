using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DryIoc;
using SharpOffice.Core.Window;

namespace SharpOffice.Core.Container
{
    public class MenuComposerRegistrationModule : IRegistrationModule
    {
        private readonly List<Type> _composers;

        public MenuComposerRegistrationModule(Assembly[] assemblies)
        {
            _composers = new List<Type>();

            foreach (var assembly in assemblies)
                _composers.AddRange(assembly.GetTypes().Where(t => typeof(IMenuComposer).IsAssignableFrom(t)));
        }

        public void Register(DryIoc.Container container)
        {
            foreach (var composer in _composers)
                container.Register(typeof (IMenuComposer), composer);
        }
    }
}