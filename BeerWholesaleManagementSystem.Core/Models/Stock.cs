﻿namespace BeerWholesaleManagementSystem.Core.Models;

public class Stock
{
    public int StockId { get; set; }
    public int WholesalerId { get; set; }
    public int BeerId { get; set; }
    public int QuantityStock { get; set; }
    public virtual Wholesaler wholesaler { get; set; }
    public virtual Beer Beer { get; set; }
}
