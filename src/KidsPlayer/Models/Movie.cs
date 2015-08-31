using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KidsPlayer.Models
{
    public class Movie
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                Name = System.IO.Path.GetFileNameWithoutExtension(_path);
            }
        }

        public string Name { get; set; }
    }
}
