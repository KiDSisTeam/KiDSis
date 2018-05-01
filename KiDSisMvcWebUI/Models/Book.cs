using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        //count:adet sayı
        public string BookCount { get; set; }
        //Kitap kategorileri kitap tablosuna eklenmiş oldu.
     
        public BooksCategory BooksCategorys { get; set; }
        //Kullanıcı Bilgileri tabloya eklendi
           
        public User Users { get; set; }









    }
}