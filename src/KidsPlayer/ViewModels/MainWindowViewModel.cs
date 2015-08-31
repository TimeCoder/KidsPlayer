using System.Collections.Generic;
using System.Linq;
using Catel.Data;
using Catel.IoC;
using Catel.Services;
using KidsPlayer.Models;

namespace KidsPlayer.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class MainWindowViewModel : ViewModelBase, IMainWindow
    {
        private readonly ListViewModel _listScreen;
        private readonly PlayerViewModel _playerScreen;

        public override string Title { get { return "KidsPlayer"; } }
        
        public IViewModel CurrentScreen
        {
            get { return GetValue<IViewModel>(CurrentScreenProperty); }
            set { SetValue(CurrentScreenProperty, value); }
        }
        public static readonly PropertyData CurrentScreenProperty = RegisterProperty("CurrentScreen", typeof(IViewModel));


        public MainWindowViewModel(ISelectDirectoryService selectDirectoryService)
        {
            _listScreen = new ListViewModel(this, selectDirectoryService);
            _playerScreen = new PlayerViewModel(this);

            CurrentScreen = _listScreen;
        }


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


        public void StartShow(List<Movie> movies, int startIndex)
        {
            CurrentScreen = _playerScreen;
            _playerScreen.StartShow(movies, startIndex);
            
        }

        public void StopShow()
        {
            CurrentScreen = _listScreen;
        }
    }
}
