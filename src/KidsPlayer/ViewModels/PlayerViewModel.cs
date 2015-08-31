using System.Collections.Generic;
using Catel.Data;
using KidsPlayer.Models;

namespace KidsPlayer.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class PlayerViewModel : ViewModelBase
    {
        private List<Movie> _movies;
        private int _currentIndex;

        public override string Title { get { return "View model title"; } }

        public string CurrentVideoSource
        {
            get { return GetValue<string>(CurrentVideoSourceProperty); }
            set { SetValue(CurrentVideoSourceProperty, value); }
        }
        public static readonly PropertyData CurrentVideoSourceProperty = RegisterProperty("CurrentVideoSource", typeof(string));


        public NavigationPanelViewModel NavPanel
        {
            get { return GetValue<NavigationPanelViewModel>(NavPanelProperty); }
            set { SetValue(NavPanelProperty, value); }
        }
        public static readonly PropertyData NavPanelProperty = RegisterProperty("NavPanel", typeof(NavigationPanelViewModel));

        public bool IsNavPanelVisible
        {
            get { return GetValue<bool>(IsNavPanelVisibleProperty); }
            set { SetValue(IsNavPanelVisibleProperty, value); }
        }
        public static readonly PropertyData IsNavPanelVisibleProperty = RegisterProperty("IsNavPanelVisible", typeof(bool));

        public double NavPanelOpacity
        {
            get { return GetValue<double>(NavPanelOpacityProperty); }
            set { SetValue(NavPanelOpacityProperty, value); }
        }
        public static readonly PropertyData NavPanelOpacityProperty = RegisterProperty("NavPanelOpacity", typeof(double));


        public PlayerViewModel(IMainWindow mainWindow)
        {
            NavPanel = new NavigationPanelViewModel(mainWindow);
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
            _movies = movies;
            _currentIndex = startIndex;
            CurrentVideoSource = _movies[startIndex].Path;

            
        }


        private Command _showNavPanel;
        public Command ShowNavPanel
        {
            get
            {
                return _showNavPanel ?? (_showNavPanel = new Command(() =>
                {
                    
                    IsNavPanelVisible = false;
                    IsNavPanelVisible = true;
                    NavPanelOpacity = 1.0;

                }));
            }
        }


        private Command _nextMovie;
        public Command NextMovie
        {
            get
            {
                return _nextMovie ?? (_nextMovie = new Command(() =>
                {
                    if (++_currentIndex >= _movies.Count)
                    {
                        _currentIndex = 0;
                    }

                    CurrentVideoSource = _movies[_currentIndex].Path;
                }));
            }
        }
    }
}
