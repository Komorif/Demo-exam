using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Фамилия.DataBase;

namespace Фамилия.Helper
{
    internal class Helpers
    {
        public static ФамилияEntities DB = new ФамилияEntities();
        public static Label label;
        public static Frame frame;
        public static Users CurrentUser;
    }
}
