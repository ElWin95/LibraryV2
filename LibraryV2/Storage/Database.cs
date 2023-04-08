using LibraryV2.Library;
using LibraryV2.Storage.IIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Storage
{
    [Serializable]
    public class Database
    {
        public GenericStore<Authors> Author { get; set; }
        public GenericStore<Books> Book { get; set; }
    }
}
