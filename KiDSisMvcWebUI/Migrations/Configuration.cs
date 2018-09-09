namespace KiDSisMvcWebUI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KiDSisMvcWebUI.Entity.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Tablolar Dolu olsada iþlem yapar
            AutomaticMigrationDataLossAllowed = false;

        }

        protected override void Seed(KiDSisMvcWebUI.Entity.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
