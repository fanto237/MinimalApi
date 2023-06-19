using MagicVillaApi.Data;
using MagicVillaApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// add services to the DI Container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configure the HTTP Request Pipeline
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints implementation

// GET /api/v1/coupons
app.MapGet("/api/v1/coupons", () =>
{
    var response = new Response()
    {
        Status = "success",
        Data = CouponStore.CouponList,
    };
    return Results.Ok(response);
}).WithName("GetCoupons").Produces<IEnumerable<Coupon>>();

// GET /api/v1/coupons/{id}
app.MapGet("/api/v1/coupons/{id:int}", (int id) =>
{
    var response = new Response();
    if (id > CouponStore.CouponList.Count)
    {
        response.Status = "error";
        response.Message = "Invalid Coupon ID";
        return Results.BadRequest(response);
    }

    response.Status = "success";
    response.Data = CouponStore.CouponList.FirstOrDefault(entry => entry.Id == id);
    return Results.Ok(response);
}).WithName("GetCoupon").Produces<Response>().Produces(400);

// POST /api/v1/coupons
app.MapPost("/api/v1/coupons", ([FromBody] Coupon coupon) =>
{
    var response = new Response();
    if (coupon.Id != 0 || string.IsNullOrWhiteSpace(coupon.Name))
    {
        response.Status = "error";
        response.Message = "Invalid Coupon Id or Name";
        return Results.BadRequest(response);
    }
    
    if(coupon.Name == CouponStore.CouponList.FirstOrDefault(entry => entry.Name == coupon.Name)?.Name)
    {
        response.Status = "error";
        response.Message = "Coupon Name already exists";
        return Results.BadRequest(response);
    }
    coupon.Id = CouponStore.CouponList.MaxBy(entry => entry.Id)!.Id + 1;
    coupon.CreatedDate = DateTime.Now;
    coupon.UpdatedDate = DateTime.Now;
    response.Status = "success";
    response.Data = coupon;
    CouponStore.CouponList.Add(coupon);
    response.Message = "Coupon Added Successfully";
    // return Results.Created($"/api/v1/coupons/{coupon.Id}", response);
    return Results.CreatedAtRoute("GetCoupon",new { id = coupon.Id }, response);
}).WithName("CreateCoupon").Produces(400).Produces<Response>(201);

app.Run();