using Models;

namespace Services;

public interface IUserSettingsService
{
    UserDescription GetUserDescription();
}