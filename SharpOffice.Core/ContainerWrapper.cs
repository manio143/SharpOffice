namespace SharpOffice.Core
{
    public static class ContainerWrapper
    {
        private static DryIoc.Container _container;
        public static void Initialize()
        {
            _container = new DryIoc.Container();
        }

        public static DryIoc.Container GetContainer()
        {
            return _container;
        }

        public static void Dispose()
        {
            _container.Dispose();
        }
    }
}
