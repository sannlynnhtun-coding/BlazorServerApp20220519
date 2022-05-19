using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp20220519.Services
{
    public class SqlQueryList
    {
        public static string DeleteStudent(int id)
        {
            return $"delete from tblstudent where studentid = {id}";
        }
    }
}
