using KiDSisMvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Entity
{
    public class DataInitializer:DropCreateDatabaseIfModelChanges<DataContext>
    { protected override void Seed(DataContext context)
        {

            List<BooksCategory> KitapKategori = new List<BooksCategory>
            {
                new BooksCategory()
                {
                    Name="Ortaokul"
                }
            };
            base.Seed(context);
    }}
}