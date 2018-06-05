using LiteDBXF.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseAccess))]
namespace LiteDBXF.UWP
{
    public class DatabaseAccess : IDataBaseAccess
    {
        public string DatabasePath()
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, Constants.OFFLINE_DATABASE_NAME);
        }
    }
}
