using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV2.Category
{
    public enum Menu
    {
        AuthorAdd = 1,
        AuthorGetAll,
        AuthorFindByName,
        AuthorGetById,
        AuthorEdit,
        AuthorRemove,

        BookAdd,
        BookGetAll,
        BookFindByName,
        BookGetById,
        BookEdit,
        BookRemove,

        SAVEandEXIT
    }
}
