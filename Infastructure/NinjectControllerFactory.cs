namespace RateMyTeam.Infastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel NinjectKernel;

        public NinjectControllerFactory()
        {
            NinjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController) NinjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            NinjectKernel.Bind<IStudent>().To<EFStudent>();
            //Add other bindings here
        }
    }
}