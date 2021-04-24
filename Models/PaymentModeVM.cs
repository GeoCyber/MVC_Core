using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Models
{
    public class PaymentModeVM : PaymentMode
    {
        public List<int> StatusId { get; set; }
    }
}
