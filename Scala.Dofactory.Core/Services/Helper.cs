using System;
using System.Collections.Generic;
using System.Text;

namespace Scala.Dofactory.Core.Services
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            return @"Data Source=(local)\SQLEXPRESS;Initial Catalog=dofactory; Integrated security=true;";
        }
    }
}
