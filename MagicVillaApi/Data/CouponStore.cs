using MagicVillaApi.Models;

namespace MagicVillaApi.Data;

public static class CouponStore
{
    public static List<Coupon> CouponList { get; set; } = new ()
    {
        new() { Id = 1, Name = "Adidas", Percent = 20, IsActive = true, CreatedDate = DateTime.Parse("2022-05-12"), UpdatedDate = DateTime.Now},
        new () { Id = 2, Name = "Nike", Percent = 10, IsActive = false }
    };

}