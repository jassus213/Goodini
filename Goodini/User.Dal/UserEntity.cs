﻿using System.ComponentModel.DataAnnotations.Schema;

namespace User.Dal;

[Table("Users")]
public class UserEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}