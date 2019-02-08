using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ClientCommon.Contract;
using Microsoft.AspNetCore.Mvc;


namespace ClientCommon.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomerApiController : Controller
    {
        protected readonly IMapper _mapper;
       

        public CustomerApiController(IMapper mapper)
        {
            _mapper = mapper;
            
        }
        [HttpPut("[action]")]
        public bool CreateCustomer([FromBody]CustomerRequestVM request)
        {
            
           
            
            
            
            return true;
        }
        [HttpGet("[action]")]
        [Route("api/CustomerApi/GetTopCustomer/{topCustomer}", Name = "GetTopCustomer")]
        public async Task<IEnumerable<CustomerDetailVM>>GetTopCustomer()
        {
            return null;
        }
    }
}