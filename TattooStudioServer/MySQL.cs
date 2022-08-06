using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TattooStudioServer
{
    class MySQLHandler : BaseScript
    {
        public static MySQLHandler Instance { get; private set; }

        public MySQLHandler()
        {
            Instance = this;
        }

        public void FetchAll(string query, string[] pars, Action<List<dynamic>> action)
        {
            Exports["oxmysql"].execute(query, pars, action);
        }

        public void Execute(string query, string[] pars, Action<dynamic> action)
        {
            Exports["oxmysql"].execute(query, pars, action);
        }
    }
}