﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using WorkshopGroup.Models;

namespace ContactWebModels
{
  public class Supply
  {
    public int Id { get; set; }
    public int SupplyId { get; set; }
    //public int ReviewId { get; set; }
    public string Name { get; set; }
    public string? UserId { get; set; }
    public DateTime? DateCreated { get; set; }
    //public ICollection<CategorySupply> CategorySupplies { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<SupplyOwner> SupplyOwners { get; set; }
    public ICollection<SupplyCategory> SupplyCategories { get; set; }
    //public Review Review { get; set; }

    public Supply()
    {
      DateCreated = DateTime.Now;
    }



  }
}