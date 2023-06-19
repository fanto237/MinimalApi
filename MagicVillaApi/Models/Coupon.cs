namespace MagicVillaApi.Models;

public class Coupon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Percent { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}