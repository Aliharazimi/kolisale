using AutoMapper;
using WebApi.Authorization;
using WebApi.Helpers;
using WebApi.Models.Business;
using WebAPI.Models;
using WebAPI.Models.Products;

namespace WebApi.Services
{
    public interface IBusinessServices
    {
        Task<BusinessModel> CreateShop(BusinessModel bus);
        BusinessModel GetById(int id);
        Task<BusinessModel> getByIdAsync(int id);
        Task<BusinessModel> UpdateShop(int id, BusinessModel bus);
        void deleteShop(int userId, int id);
    }

    public class BusinessServices: IBusinessServices
    {

        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpcontext;

        public BusinessServices(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public async Task<BusinessModel> CreateShop(BusinessModel model)
        {

            // map model to new product object
            var bus = _mapper.Map<BusinessModel>(model);
            // save product
            await _context.businessModels.AddAsync(bus);
            await _context.SaveChangesAsync();

            return bus;
        }


        // update product
        public async Task<BusinessModel> UpdateShop(int id, BusinessModel model)
        {
            var bus = getBusiness(id);


            // copy model to product and save
            _mapper.Map(model, bus);
            _context.businessModels.Update(bus);
            await _context.SaveChangesAsync();
            return bus;
        }
        // delete product
        public void deleteShop(int userId, int id)
        {
            var business = getBusiness(id);
            if (business.UserId != userId)
            {
                throw new AppException("sorry! you can only delete your shop");
            }
            _context.businessModels.Remove(business);
            _context.SaveChanges();

        }

        public BusinessModel GetById(int id)
        {
            var bus = getBusiness(id);
           
            return bus;
        }


        private BusinessModel getBusiness(int id)
        {
            var bus = _context.businessModels.Find(id);
            if (bus == null) throw new KeyNotFoundException("Business not found");
            return bus;
        }


        public async Task<BusinessModel> getByIdAsync(int id)
        {
            var bus = await _context.businessModels.FindAsync(id);
            if (bus == null) throw new KeyNotFoundException("Business not found");
            return bus;
        }
    }
}
