using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class ExampleDTO
    {
        public string Value { get; set; }
    }

    public class DBDTO : ExampleDTO
    {
        public int ID { get; set; }
    }

    public class AddNewTableDTO
    {
        public string TblName { get; set; }
        public List<string> Column { get; set; }
        public List<string> DataType { get; set; }
    }


}
