using Api.EndPoint.Models.Dtos;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Get;
using Api.EndPoint.Models.Dtos.Product.ProductItem.GetById;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Post;
using Api.EndPoint.Models.Dtos.Product.ProductItem.Put;
using Aplication.Services.Products.ProductItem.Commands.Create;
using Aplication.Services.Products.ProductItem.Commands.Delete;
using Aplication.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.ProductItem.Commands.Edit;
using Application.Services.Products.Queries;
using AutoMapper;
using Azure.Core;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.EndPoint.Controllers.V1.Product
{
    [ApiVersion("1")]
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[Authorize(Roles = "Admin,Operator")]
	public class ProductController : ControllerBase
	{
		private readonly IMemoryCache cache;
		private readonly IMapper mapper;
		private readonly IMediator mediator;

		private readonly string CacheKey = "Products";
		public ProductController(IMapper mapper, IMediator mediator, IMemoryCache cache)
		{
			this.mapper = mapper;
			this.mediator = mediator;
			this.cache = cache;
		}
		// GET: api/<ProductController>
		/// <summary>
		/// Get All Products
		/// </summary>
		/// <param name="request">Information</param>
		/// <returns>Products</returns>
		[HttpGet]
		public IActionResult Get([FromHeader] ProductItemGetRequestDto request)
		{
			BaseDto<List<ProductItemGetResultDto>> result;
			if (cache == null || !cache.TryGetValue(CacheKey, out result))
			{
				GetProductItemRequestDto Dto = mapper.Map<GetProductItemRequestDto>(request);
				GetProductItemRequest SendData = new GetProductItemRequest(Dto);

				BaseDto<List<GetProductItemDto>> mediateResult = mediator.Send(SendData).Result;
				if (!mediateResult.IsSuccess)
					return BadRequest(new BaseDto(false, mediateResult.Message));

				var resultDto = mapper.Map<List<ProductItemGetResultDto>>(mediateResult.Data);
				result = new BaseDto<List<ProductItemGetResultDto>>(true, mediateResult.Message, resultDto);

				var cacheOptions = new MemoryCacheEntryOptions()
					.SetSlidingExpiration(TimeSpan.FromMinutes(1))
					.RegisterPostEvictionCallback(CacheCallBack, this)
					.SetSize(1);

				if (result.Data.Count != 0 && result.Data != null && cache != null)
				{
					cache.Set(CacheKey, result, cacheOptions);
				}
			}
			else result = (BaseDto<List<ProductItemGetResultDto>>)cache.Get(CacheKey);

			return Ok(result);
		}

		// GET api/<ProductController>/5
		/// <summary>
		/// Get By Id Product
		/// </summary>
		/// <param name="id">Product Id</param>
		/// <returns>Product</returns>
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			GetByIdProductItemRequest SendData = new GetByIdProductItemRequest(id);

			BaseDto<GetByIdProductItemDto> mediateResult = mediator.Send(SendData).Result;
			if (!mediateResult.IsSuccess)
				return NotFound(new BaseDto(false, mediateResult.Message));

			var resultDto = mapper.Map<ProductItemGetByIdResultDto>(mediateResult.Data);
			var result = new BaseDto<ProductItemGetByIdResultDto>(true, mediateResult.Message, resultDto);

			return Ok(result);
		}

		// POST api/<ProductController>
		/// <summary>
		/// Add New Product
		/// </summary>
		/// <param name="request">Product Information</param>
		/// <returns>Information</returns>
		[HttpPost]
		public IActionResult Post([FromBody] ProductItemPostRequestDto request)
		{
			CreateProductItemDto productMedateDto = mapper.Map<CreateProductItemDto>(request);
			CreateProductItemCommand SendData = new CreateProductItemCommand(productMedateDto);
			BaseDto<int> mediateResult = mediator.Send(SendData).Result;
			if (!mediateResult.IsSuccess)
				return BadRequest(new BaseDto<int>(false, mediateResult.Message, 0));

			cache.Remove(CacheKey);
			return Ok(mediateResult);
		}

		// PUT api/<ProductController>/5
		/// <summary>
		/// Edit Product
		/// </summary>
		/// <param name="id">Porduct Id</param>
		/// <param name="request">Porduct Information</param>
		/// <returns>Product</returns>
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] ProductItemPutRequestDto request)
		{
			EditProductItemDto productDto = mapper.Map<EditProductItemDto>(request);
			EditProductItemCommand command = new EditProductItemCommand(productDto, id);
			BaseDto<EditProductItemResultDto> mediateResult = mediator.Send(command).Result;
			if (!mediateResult.IsSuccess) return BadRequest(new BaseDto<ProductItemPutResultDto>(false, mediateResult.Message, null));

			BaseDto<ProductItemPutResultDto> result = new BaseDto<ProductItemPutResultDto>(true, new List<string> { "Edit Product Is Success" }, mapper.Map<ProductItemPutResultDto>(mediateResult.Data));
			result.Data.Links = GenerateLink(result.Data.Id);

			cache.Remove(CacheKey);
			return Ok(result);
		}

		// DELETE api/<ProductController>/5
		/// <summary>
		/// Delete Product By Id
		/// </summary>
		/// <param name="id">Product Id</param>
		/// <returns>Information</returns>
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			DeleteProductItemCommand command = new DeleteProductItemCommand(id);
			BaseDto result = mediator.Send(command).Result;
			if (!result.IsSuccess) return BadRequest(new BaseDto(false, result.Message));

			cache.Remove(CacheKey);
			return Ok(new BaseDto(true, result.Message));
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
						Href = Url.Action(nameof(Get), "Product", new { Id }, Request.Scheme),
						Rel = "Self",
						Method = "Get"
					},
					new Links
					{
						Href = Url.Action(nameof(Delete), "Product", new { Id }, Request.Scheme),
						Rel = "Delete",
						Method = "Delete"
					},
					new Links
					{
						Href = Url.Action(nameof(Put), "Product", new { Id }, Request.Scheme),
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
