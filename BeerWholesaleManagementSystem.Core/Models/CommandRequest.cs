using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerWholesaleManagementSystem.Core.Models;

public class CommandRequest
{
    public class CommandeRequest
    {
        public int CommandRequestId { get; set; }
        public int GrossisteId { get; set; }
        public DateTime DateCommande { get; set; }
        public required string Statuts { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
        public virtual required Wholesaler Wholesaler { get; set; }
        public virtual required List<Beer> BeerCommandees { get; set; }
    }
}
