﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class BenhNhan
{
    public int Id { get; set; }

    public string Ho { get; set; }

    public string Ten { get; set; }

    public string Cmnd { get; set; }

    public DateTime? NgaySinh { get; set; }

    public bool? GioiTinh { get; set; }

    public string DiaChi { get; set; }

    public string Email { get; set; }

    public virtual ICollection<ToaThuoc> ToaThuoc { get; set; } = new List<ToaThuoc>();
}