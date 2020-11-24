using Autofac;

using Interfaces;

namespace ImplOne
{
    public sealed class FullSizeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FullSizeHello>().As<IHello>();
        }
    }
}
