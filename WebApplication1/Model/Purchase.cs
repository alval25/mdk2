using System;
using System.Collections.Generic;

namespace WebApplication1.Model;

public partial class Purchase
{
    public int Idpurchase { get; set; }

    public int? IdUser { get; set; }

    public DateTime? Datetime { get; set; }

    public virtual User? IdUserNavigation { get; set; }
}
