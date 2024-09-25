using MbaBlog.WebApi.Helpers;

namespace MbaBlog.WebApi.Extensions;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelps.EnsureSeedData(app).Wait();
    }
}
