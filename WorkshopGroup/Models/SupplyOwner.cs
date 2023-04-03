using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopGroup.Models;

namespace WorkshopGroup.Models
{
  public class SupplyOwner
  {
    public int Id { get; set; }
    public int SupplyOwnerId { get; set; }
    public int OwnerId { get; set; }
    public int SupplyId { get; set; }

    public Supply? Supply { get; set; }
    public Owner? Owner { get; set; }
    public ICollection<Supply>? Supplies { get; set; }
    public ICollection<Owner>? Owners { get; set; }


  }
}
