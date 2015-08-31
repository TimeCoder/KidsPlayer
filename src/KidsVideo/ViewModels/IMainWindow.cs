using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KidsVideo.Models;

namespace KidsVideo.ViewModels
{
    public interface IMainWindow
    {
        void StartShow(List<Movie> movies, int startIndex);
        void StopShow();
    }
}
