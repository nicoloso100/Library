﻿using LibraryDTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public interface IAuthServices
    {
        Task<SecurityToken> AuthUser(DTOLogin auth);
    }
}