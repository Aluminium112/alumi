﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Hospital.Models;

public partial class ToaThuoc
{
    public int Id { get; set; }

    public DateTime? Ngay { get; set; }

    public string ChanDoan { get; set; }

    public int? IdBacSi { get; set; }

    public int? IdBenhNhan { get; set; }

    public string TinhTrang { get; set; }

    public virtual NhanVien IdBacSiNavigation { get; set; }

    public virtual BenhNhan IdBenhNhanNavigation { get; set; }

    public virtual ICollection<ToaThuocChiTiet> ToaThuocChiTiet { get; set; } = new List<ToaThuocChiTiet>();
}