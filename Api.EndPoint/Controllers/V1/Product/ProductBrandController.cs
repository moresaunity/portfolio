using Api.EndPoint.Models.Dtos;
using Api.EndPoint.Models.Dtos.Product.Brand.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Aplication.Services.Brands.Commands.Delete;
using Aplication.Services.Brands.Commands.Edit;
using Aplication.Services.Products.Brands.Commands.Send;
using Application.Services.Products.Brands.Queries.Get;
using Application.Services.Products.Brands.Queries.GetById;
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
                if (cache != null) foreach (var item in result) item.Links = GenerateLink(item.Id);

				var cacheOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(1))
					.RegisterPostEvictionCallback(CacheCallBack, this)
					.SetSize(1);
                if (cache != null) cache.Set(CacheKey, result, cacheOptions);
			}
			else result = (List<ProductBrandGetResultDto>)cache.Get(CacheKey);

			return Ok(new BaseDto<List<ProductBrandGetResultDto>>(true, new List<string> { "Get Brands Is Success" }, result));
		}
		/// <summary>
		/// Get By Id Brand
		/// </summary>
		/// <param name="id">Brand Id</param>
		/// <returns>Brand</returns>
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			FindByIdBrandRequest request = new FindByIdBrandRequest(id);
			BaseDto<FindByIdBrandDto> mediateResult = mediator.Send(request).Result;
			if(!mediateResult.IsSuccess) return NotFound(mediateResult);
			ProductBrandGetResultDto result = mapper.Map<ProductBrandGetResultDto>(mediateResult.Data);
            if (cache != null) result.Links = GenerateLink(result.Id);
			return Ok(new BaseDto<ProductBrandGetResultDto>(true, mediateResult.Message, result));
		}
		/// <summary>
		/// Add a New Brand
		/// </summary>
		/// <param name="brand">Brand Name</param>
		/// <returns>Brand</returns>
		[HttpPost]
		public IActionResult Post([FromBody] string brand)
		{
			SendBrandDto sendBrandDto = new SendBrandDto() { Brand = brand };
			SendBrandCommand request = new SendBrandCommand(sendBrandDto);
			BaseDto<SendBrandResponseDto> mediateResult = mediator.Send(request).Result;
			if (!mediateResult.IsSuccess) return BadRequest(mediateResult);
			ProductBrandGetResultDto result = mapper.Map<ProductBrandGetResultDto>(mediateResult.Data);
            if (cache != null) result.Links = GenerateLink(result.Id);
            if (cache != null) cache.Remove(CacheKey);
			return Ok(new BaseDto<ProductBrandGetResultDto>(true, mediateResult.Message, result));
		}
		/// <summary>
		/// Edit By Id Brand
		/// </summary>
		/// <param name="id">Brand Id</param>
		/// <param name="brand">Brand Name</param>
		/// <returns>Brand</returns>
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] string brand)
		{
			EditBrandDto editBrandDto = new EditBrandDto() { Brand = brand };
			EditBrandCommand request = new EditBrandCommand(editBrandDto, id);
			BaseDto<EditBrandResponseDto> mediateResult = mediator.Send(request).Result;
			if (!mediateResult.IsSuccess) return NotFound(mediateResult);
			ProductBrandGetResultDto result = mapper.Map<ProductBrandGetResultDto>(mediateResult.Data);
            if (cache != null) result.Links = GenerateLink(result.Id);
            if (cache != null) cache.Remove(CacheKey);
			return Ok(new BaseDto<ProductBrandGetResultDto>(true, mediateResult.Message, result));
		}
		/// <summary>
		/// Delete Brand By Id
		/// </summary>
		/// <param name="id">Brand Id</param>
		/// <returns>Brand</returns>
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			DeleteBrandCommand request = new DeleteBrandCommand(id);
			BaseDto<DeleteBrandResponseDto> mediateResult = mediator.Send(request).Result;
			if (!mediateResult.IsSuccess) return NotFound(mediateResult);
			ProductBrandGetResultDto result = mapper.Map<ProductBrandGetResultDto>(mediateResult.Data);
            if (cache != null) cache.Remove(CacheKey);
			return Ok(new BaseDto<ProductBrandGetResultDto>(true, mediateResult.Message, result));
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
