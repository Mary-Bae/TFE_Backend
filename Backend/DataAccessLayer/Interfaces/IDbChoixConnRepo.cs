﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDbChoixConnRepo
    {
        IDbConnection CreateConnection(string name);

    }
}
