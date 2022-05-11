using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Not_Kayıt__Sistemi
{
    public class StudentViewDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }

        public string Surname { get;set; }
        public int ? ExamOne { get; set; }
        public int ? ExamTwo { get; set; }
        public int ? ExamThree { get; set; }
        public decimal ? Ortalama { get; set; }
        public bool ? Durum { get; set; }



        public override string ToString()
        {
            return $"{Id}, {Name}, {Surname}";
        }
    }
}
