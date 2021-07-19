using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Dto;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Users.Interfaces
{
    public interface IUserBll : ICrudBll<UserDto, User, Guid>
    {
    }
}
