using System.ComponentModel.DataAnnotations.Schema;

namespace BeerWholesaleManagementSystem.Core.Models;


    public class CommandRequest
    {
        public int CommandRequestId { get; set; }
        public int WholesalerId { get; set; }
        public DateTime DateCommande { get; set; }
        public required string Statuts { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPrice { get; set; }
        public virtual required Wholesaler Wholesaler { get; set; }
        public virtual required List<Beer> BeerCommandees { get; set; }
    }

