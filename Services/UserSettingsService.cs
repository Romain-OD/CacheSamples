using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;
public class UserSettingsService : IUserSettingsService
{
    public UserDescription UserDescription { get; set; }

    public UserDescription GetUserDescription()
    {
        return new UserDescription()
        {
            CompanyId = Environment.MachineName
        };
    }
}
