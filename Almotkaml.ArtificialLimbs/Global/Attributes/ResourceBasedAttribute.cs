using System;
using System.Resources;

namespace Almotkaml.ArtificialLimbs.Global.Attributes
{
    public abstract class ResourceBasedAttribute : Attribute
    {
        protected ResourceBasedAttribute(Type type)
        {
            ResourceType = new ResourceManager(type);
        }
        protected ResourceManager ResourceType { get; }
        public abstract string Display { get; }
    }
}