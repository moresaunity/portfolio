using Api.EndPoint.Models.Dtos;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Application.Services.Products.Brands.Queries.Get;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;

namespace Api.EndPoint.Controllers.V1.Product
{
	[ApiVersion("1")]
	[Route("api/v{version:apiVersion}/Product/Brand/")]
	[ApiController]
	public class ProductBrandController : ControllerBase
	{
		private readonly IMemoryCache cache;
		private readonly IMediator mediator;
		private readonly IMapper mapper;
		private readonly string CacheKey = "Brands";

		public ProductBrandController(IMediator mediator, IMapper mapper, IMemoryCache cache)
		{
			this.mediator = mediator;
			this.mapper = mapper;
			this.cache = cache;
		}
		/// <summary>
		/// Get All Brands
		/// </summary>
		/// <returns>Brands</returns>
		[HttpGet]
		public IActionResult Get()
		{
			List<ProductBrandGetResultDto> result;
			if (cache == null || !cache.TryGetValue(CacheKey, out result))
			{

				GetBrandRequest MediateRequest = new GetBrandRequest();
				BaseDto<List<GetBrandDto>> MediateResult = mediator.Send(MediateRequest).Result;
				if (MediateResult == null || !MediateResult.IsSuccess) return BadRequest(new BaseDto<List<ProductBrandGetResultDto>>(false, MediateResult.Message, null));

				result = mapper.Map<List<ProductBrandGetResultDto>>(MediateResult.Data);
				foreach (var item in result) item.Links = GenerateLink(item.Id);

				var cacheOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(1))
					.RegisterPostEvictionCallback(CacheCallBack, this)
					.SetSize(1);
				cache.Set(CacheKey, result, cacheOptions);
			}
			else result = (List<ProductBrandGetResultDto>)cache.Get(CacheKey);

			return Ok(new BaseDto<List<ProductBrandGetResultDto>>(true, new List<string> { "Get Brands Is Success" }, result));
		}
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			return Ok();
		}
		[HttpPost]
		public IActionResult Post()
		{
			return Ok();
		}
		[HttpPut("{id}")]
		public IActionResult Put(int id)
		{
			return Ok();
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			return Ok();
		}
		/// <summary>
		/// Generate Link for this Controller Methodes
		/// </summary>
		/// <param name="Id">Brand Id</param>
		/// <returns>List Links</returns>
		private List<Links> GenerateLink(int Id)
		{
			return new List<Links>
				{
					new Links
					{
						Href = Url.Action(nameof(Get), "ProductBrand", new { Id }, Request.Scheme),
						Rel = "Self",
						Method = "Get"
					},
					new Links
					{
						Href = Url.Action(nameof(Delete), "ProductBrand", new { Id }, Request.Scheme),
						Rel = "Delete",
						Method = "Delete"
					},
					new Links
					{
						Href = Url.Action(nameof(Put), "ProductBrand", new { Id }, Request.Scheme),
						Rel = "Update",
						Method = "Put"
					}
				};
		}
		private static void CacheCallBack(object Key, object value, EvictionReason reason, object state)
		{

		}
	}
}
