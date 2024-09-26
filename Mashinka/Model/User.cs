using System;
using System.Collections.Generic;

namespace WpfApp17.Model;

public partial class User
{
    public int IdUsers { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? RoleId { get; set; }

    public string? Salt { get; set; }

    public string? Token { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
