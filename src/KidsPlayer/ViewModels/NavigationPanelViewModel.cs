namespace KidsPlayer.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class NavigationPanelViewModel : ViewModelBase
    {
        private readonly IMainWindow _mainWindow;

        public NavigationPanelViewModel(IMainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public override string Title { get { return "View model title"; } }

        // TODO: Register models with the vmpropmodel codesnippet
        // TODO: Register view model properties with the vmprop or vmpropviewmodeltomodel codesnippets
        // TODO: Register commands with the vmcommand or vmcommandwithcanexecute codesnippets

        protected override async Task Initialize()
        {
            await base.Initialize();

            // TODO: subscribe to events here
        }

        protected override async Task Close()
        {
            // TODO: unsubscribe from events here

            await base.Close();
        }


        private Command _eject;
        public Command Eject
        {
            get
            {
                return _eject ?? (_eject = new Command(() =>
                {
                    _mainWindow.StopShow();
                    
                }));
            }
        }

    }
}
