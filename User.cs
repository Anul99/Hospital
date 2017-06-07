using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
     public interface IUser: IComparable<IUser>
    {
        string Name{ get; set; }
        string Surname { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string Possition { get; set; }

    }
}
