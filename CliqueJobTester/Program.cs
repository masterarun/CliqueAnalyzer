using CliqueService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueJobTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new HashTagService();
            var result = service.GenerateHashTagDetails(3);
        }
    }
}
