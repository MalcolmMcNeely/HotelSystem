using DryIoc;
using Prism.Ioc;

namespace HotelSystem.Infrastructure.DryIoc
{
    public static class PrismIocExtensions
    {
        public static IContainer GetContainer(this IContainerProvider containerProvider)
        {
            return ((IContainerExtension<IContainer>)containerProvider).Instance;
        }

        public static IContainer GetContainer(this IContainerRegistry containerRegistry)
        {
            return ((IContainerExtension<IContainer>)containerRegistry).Instance;
        }
    }
}
