using MbaBlog.Mvc.Helpers;

namespace MbaBlog.Mvc.Extensions;

public static class DbMigrationHelperExtension
{
    public static void UseDbMigrationHelper(this WebApplication app)
    {
        DbMigrationHelps.EnsureSeedData(app).Wait();
    }
}
