﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.User
{
    public class UserGetByIdReq
    {
        public Guid Id { get; set; }
        public int pagenumber { get; set; }
        public int pagesize { get; set; }
    }
}
