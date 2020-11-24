using Autofac;

using Interfaces;

namespace ImplTwo
{
    public sealed class ConciseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConciseHello>().As<IHello>();
        }
    }
}
