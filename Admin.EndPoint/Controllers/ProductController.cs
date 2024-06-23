using Admin.EndPoint.Models.Dtos;
using Admin.EndPoint.Models.Dtos.Product.Brand.Get;
using Admin.EndPoint.Models.Dtos.Product.Item;
using Admin.EndPoint.Models.Dtos.Product.Type;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RestSharp;

namespace Admin.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult Index(ProductItemGetRequestDto? requestDto)
        {
            RestClient client = new RestClient("http://localhost:5168");
            RestRequest request = new RestRequest("/api/v1/Product/GetProducts", Method.Post);
            request.AddJsonBody(requestDto);
            Models.Dtos.BaseDto<List<ProductItemGetResultDto>>? result = client.Post<Models.Dtos.BaseDto<List<ProductItemGetResultDto>>>(request);

            return View(result?.Data);
        }
        [HttpGet]
        public IActionResult Type(int? parentId = null, int page = 1, int pageSize = 10)
        {
            RestClient client = new RestClient("http://localhost:5168");
            RestRequest request = new RestRequest("/api/v1/Product/Type?parentId="+parentId+"&page=1&pageSize=20", Method.Get);
            Models.Dtos.BaseDto<PaginatedItemsDto<GetProductTypeResultDto>>? result = client.Get<Models.Dtos.BaseDto<PaginatedItemsDto<GetProductTypeResultDto>>>(request);

            return View(result?.Data.Data);
        }
        [HttpGet]
        public IActionResult Brand()
        {
            RestClient client = new RestClient("http://localhost:5168");
            RestRequest request = new RestRequest("/api/v1/Product/Brand", Method.Get);
            Models.Dtos.BaseDto<List<ProductBrandGetResultDto>>? result = client.Get<Models.Dtos.BaseDto<List<ProductBrandGetResultDto>>>(request);

            return View(result?.Data);
        }
    }
}
