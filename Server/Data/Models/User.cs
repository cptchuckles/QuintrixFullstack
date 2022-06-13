using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuintrixFullstack.Server.Models;

public class User
{
    [Key]
    [Editable(false)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Username { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    [Editable(false)]
    [DisplayFormat(DataFormatString = "{0:D}")]
    public DateTime Created { get; set; } = DateTime.Now;
}