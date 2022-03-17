using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationGloves
{
    public class CustomSupplier : Supplier
    {
        public CustomSupplier(Supplier supplier)
        {
            ID = supplier.ID;
            Title = supplier.Title;         
        }
    }
}
