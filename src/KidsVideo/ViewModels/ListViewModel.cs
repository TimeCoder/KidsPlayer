using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Catel.Data;
using Catel.Services;
using KidsVideo.Models;
using NReco.VideoConverter;

namespace KidsVideo.ViewModels
{
    using Catel.MVVM;
    using System.Threading.Tasks;

    public class ListViewModel : ViewModelBase
    {
        private readonly IMainWindow _mainWindow;

        private readonly string _appFolder;
        private readonly string _filePath;


        public List<Movie> Movies
        {
            get { return GetValue<List<Movie>>(MoviesProperty); }
            set { SetValue(MoviesProperty, value); }
        }
        public static readonly PropertyData MoviesProperty = RegisterProperty("Movies", typeof(List<Movie>), null);


        public int SelectedMovieIndex
        {
            get { return GetValue<int>(SelectedMovieIndexProperty); }
            set
            {
                SetValue(SelectedMovieIndexProperty, value); 
                _mainWindow.StartShow(Movies, value);
            }
        }
        public static readonly PropertyData SelectedMovieIndexProperty = RegisterProperty("SelectedMovieIndex", typeof(int));


        public ListViewModel(IMainWindow mainWindow, ISelectDirectoryService selectDirectoryService)
        {
            _mainWindow = mainWindow;
            
            _appFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\KidsPlayer\";
            _filePath = _appFolder + "moviesList.xml";

            LoadMoviesList();

            if (Movies == null || !Movies.Any())
            {
                if (selectDirectoryService.DetermineDirectory())
                {
                    Movies = Directory.EnumerateFiles(selectDirectoryService.DirectoryName, "*.mp4")
                        .Select(file => new Movie { Path = file })
                        .ToList();

                    //var ffMpeg = new FFMpegConverter();
                    //Directory.CreateDirectory(_appFolder + "thumbnails");

                    //foreach (var movie in Movies)
                    //{
                    //    ffMpeg.GetVideoThumbnail(movie.Path, _appFolder + @"thumbnails\" + movie.Name + ".jpg", 50);
                    //}
                   

                }
            }
        }

        public override string Title { get { return "View model title"; } }


        protected override async Task Initialize()
        {
            await base.Initialize();
            
         
        }


        protected override async Task Close()
        {
            SaveMoviesList();
            await base.Close();
        }


        private void LoadMoviesList()
        {
            if (File.Exists(_filePath))
            {
                var xs = new XmlSerializer(typeof (List<Movie>));
                using (var fs = new StreamReader(_filePath))
                {
                    Movies = (List<Movie>) xs.Deserialize(fs);
                }
            }
        }

        private void SaveMoviesList()
        {
            if (!Directory.Exists(_appFolder))
            {
                Directory.CreateDirectory(_appFolder);
            }

            var xs = new XmlSerializer(typeof (List<Movie>));
            using (var fs = new StreamWriter(_filePath))
            {
                xs.Serialize(fs, Movies);
            }
        }
    }
}
