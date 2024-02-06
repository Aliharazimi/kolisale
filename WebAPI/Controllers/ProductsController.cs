
namespace WebApi.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Helpers;
using WebApi.Services;
using System;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Products;
using WebApi.Entities;
using WebApi.Models.Products;
using Microsoft.AspNetCore.Authorization;


[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private IUserService _userService;
    private IProductService _productService;
    private IMapper _mapper;
    private IBusinessServices _businessServices;
    private readonly AppSettings _appSettings;
    public readonly User _user;

    public BusinessController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings, IProductService productservice, IBusinessServices businessServices)
    {
        _userService = userService;
        _productService = productservice;
        _mapper = mapper;
        _appSettings = appSettings.Value;
        _businessServices = businessServices;
    }


    // shop management controllers

    // get all products in shop
    [HttpGet("shop/{businessId}")]
    public async Task<IActionResult> GetBusinessProducts(int businessId)
    {
        var products = await _productService.GetBusinessAllProducts(businessId);
        return Ok(products);
    }


    // shop details 

    [HttpGet("shop/{businessId}/details")]
    public async Task<IActionResult> GetBusinessDetails(int businessId)
    {
        var bus = await _businessServices.getByIdAsync(businessId);
        return Ok(bus);
    }



    // get all product
    [HttpGet]
    public IActionResult GetAllProducts()
    {
        var products =  _productService.GetAll();
        return Ok(products);
    }
    // product detail
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _productService.GetById(id);
        return Ok(product);
    }
    
    

    // vendor methods: this methods can only be called by vendors

    // add products
    [Authorize(Roles = "vendor")]
    [HttpPost("addproduct")]
    public IActionResult AddProduct(ProductDto model)
    {
        var bus = _businessServices.GetById(model.BusinessId);
        var _product = new Product()
        {
            Name = model.Name,
            Price = model.Price,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            UserId = model.UserId,
            BusinessModel = bus,
            
            //ProductCategory = model.ProductCategoryId,
            ImageUrl = model.ImageUrl
        };
        _productService.AddProduct(_product);
        return Ok(new { message = $"Your new Product Added successful to Your Shop {bus.BusinessName}" });
    }

    // update product
    [Authorize(Roles = "vendor, admin")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, ProductUpdateRequest model)
    {
        _productService.Update(id, model);
        return Ok(new { message = "Product updated successfully" });
    }
    // delete product
    [Authorize(Roles = "vendor")]
    [HttpDelete("{username}/delete/{id}")]
    public IActionResult Delete(int businessId, int id)
    {
        _productService.Delete(businessId, id);
        return Ok(new { message = "Product deleted successfully" });
    }


    // cart methods

    // add to cart
    [AllowAnonymous]
    [HttpPost("addtocart")]
    public async Task<IActionResult> AddToCart(CartItem model)
    {
        var cartItem = new CartItem()
        {
            CartId = model.CartId,
            ProductId = model.ProductId,
            Quantity = model.Quantity
        };
        
        var Item = await _productService.AddToCart(cartItem);
        return Ok(Item);
    }

    // remove from cart
    [AllowAnonymous]
    [HttpPost("removefromcart")]
    public IActionResult RemoveFromCart(CartItem model)
    {
        var item = new CartItem() { Id = model.Id };
        _productService.RemoveFromCart(item);
        return Ok(new { message = "product removed from your cart successfully" });
    }

    // user's cart
   // [AllowAnonymous]
   // [HttpPost("cart")]
   // public async Task<IActionResult> GetUserCart(User user)
   // {
   //     var products = await _productService.GetUserCart(user);
   //     return Ok(products);
   // }
}