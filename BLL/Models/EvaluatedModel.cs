using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class EvaluatedModel
    {
        public Evaluated Record { get; set; }
        public string Name => Record.Name;

        public string Surname => Record.Surname;

        [DisplayName("Full Name")]
        public string FullName => Record.Name + " " + Record.Surname;

        
    }
}
