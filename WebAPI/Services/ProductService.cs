namespace WebApi.Services;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Products;
using WebAPI.Models;
using WebAPI.Models.Products;
using Newtonsoft;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Business;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Task<IEnumerable<Product>> GetBusinessAllProducts(int businessId);
    //Task<IEnumerable<Cart>> GetUserCart(User user);
    Product GetById(int id);
    void AddProduct(Product model);
    Task<CartItem> AddToCart(CartItem model);

    void Update(int id, ProductUpdateRequest model);
    void Delete(int businessId,int id );
    void RemoveFromCart(CartItem item);




}

public class ProductService : IProductService
{
    // List<string> items = new List<string>();
    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpcontext;

    public ProductService(
        DataContext context,
        IJwtUtils jwtUtils,
        IMapper mapper)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _mapper = mapper;
    }

  


    // get all products
    public IEnumerable<Product> GetAll()
    {

        var products =  _context.Products.ToList();
        foreach (var item in products)
        {
            var bus = _context.businessModels.FirstOrDefault(bus => bus.Id == item.BusinessId);
            if (bus != null)
            {
                item.BusinessModel = bus;
            }
            else
            {
                throw new AppException("product Business Id error");
            }
        }
        return products;
    }

    // get Business all products
    public async Task<IEnumerable<Product>> GetBusinessAllProducts(int businessId)
    {

        var products =  await  _context.Products.Where(s => s.BusinessModel.Id == businessId).ToListAsync();
        foreach (var item in products)
        {
             var bus = _context.businessModels.FirstOrDefault(bus => bus.Id == item.BusinessId);
             if(bus != null)
             {
                item.BusinessModel = bus;
             }
             else
             {
                throw new AppException("product business Id error");
             }
        }
        return products;
    }
    // product detail
    public Product GetById(int id)
    {
        var product = getProduct(id);
        if (product != null)
        {
            var bus = _context.businessModels.FirstOrDefault(bus => bus.Id==product.BusinessId);
            if(bus != null)
            {
                product.BusinessModel = bus;
                return product;
            }
            else
            {
                throw new AppException("Business Id error");
            }
        }
        return null;
    }
    public void AddProduct(Product model)
    {

        // map model to new product object
        var product = _mapper.Map<Product>(model);
        // save product
        _context.Products.Add(product);
        _context.SaveChanges();
    }
    // update product
    public void Update(int id, ProductUpdateRequest model)
    {
        var product = getProduct(id);


        // copy model to product and save
        _mapper.Map(model, product);
        _context.Products.Update(product);
        _context.SaveChanges();
    }
    // delete product
    public void Delete(int businessId, int id)
    {
        var product = getProduct(id);
        if (product.BusinessModel.Id != businessId)
        {
            throw new AppException("you can only delete from your products");
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        
    }

    // helper methods start
    private Product getProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null) throw new KeyNotFoundException("Product not found");
        return product;
    }

    
    private Cart getCartProduct(int id)
    {
        var product = _context.Carts.Find(id);
        if (product == null) throw new KeyNotFoundException("Product not found");
        return product;
    }
    // helper methods end

    // add to cart
    public async Task<CartItem> AddToCart(CartItem model)
    {
        var item = model;
        var product = getProduct(model.ProductId);
        if (product == null) throw new BadHttpRequestException("product not found");
        var cart = await _context.Carts.SingleOrDefaultAsync(x => x.Id == item.CartId);
        if (cart == null) throw new Exception("cart not found");
        await _context.CartItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    
   // remove from cart
    public void RemoveFromCart(CartItem item)
    {
        var car = _context.CartItems.SingleOrDefault(x => x.Id == item.Id);
        if (car == null)
        {
            throw new Exception("Item already removed from the cart" + item.Id);
        }
        _context.CartItems.Remove(car);
        _context.SaveChanges();
    }

    // user cart and check out method
    //  public async Task<IEnumerable<Cart>> GetUserCart(User user)
    //  {
    //      var  cartProducts = await _context.Carts.Where(s => s.User.Id == user.Id).ToListAsync();
    //      return cartProducts;
    //  }
}