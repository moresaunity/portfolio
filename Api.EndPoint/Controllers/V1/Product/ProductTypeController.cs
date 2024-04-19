using Api.EndPoint.Models.Dtos;
using Api.EndPoint.Models.Dtos.Product.Type.AddNew;
using Api.EndPoint.Models.Dtos.Product.Type.Edit;
using Api.EndPoint.Models.Dtos.Product.Type.GetAll;
using Aplication.Services.Products.ProductType.Commands.Edit;
using Aplication.Services.Products.ProductType.Commands.Send;
using Aplication.Services.ProductType.Commands.Delete;
using Application.Services.Products.ProductType.Queries.Get;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.EndPoint.Controllers.V1.Product
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Product/Type/")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly IMemoryCache cache;
        private readonly string cacheKey = "ProductTypes";

        public ProductTypeController(IMapper mapper, IMediator mediator, IMemoryCache cache)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.cache = cache;
        }
        // GET: api/<ProductTypeController>
        /// <summary>
        /// Get All Product Types
        /// </summary>
        /// <param name="page">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>Product Types</returns>
        [HttpGet("{page}, {pageSize}")]
        public IActionResult Get(int page = 1, int pageSize = 10)
        {
            BaseDto<PaginatedItemsDto<GetProductTypeResultDto>> data;
            if (cache == null || !cache.TryGetValue(cacheKey, out data))
            {
                GetProductTypeRequest request = new GetProductTypeRequest(new GetProductTypeRequestDto { Page = page, PageSize = pageSize });
                BaseDto<PaginatedItemsDto<GetProductTypeDto>> MediateResult = mediator.Send(request).Result;
                if (!MediateResult.IsSuccess) return BadRequest(MediateResult);
                List<GetProductTypeResultDto> result = mapper.Map<List<GetProductTypeResultDto>>(MediateResult.Data.Data);
                foreach (var item in result) item.Links = GenerateLink(item.Id);
                data = new BaseDto<PaginatedItemsDto<GetProductTypeResultDto>>(true, MediateResult.Message, new PaginatedItemsDto<GetProductTypeResultDto>(page, pageSize, result.Count, result));
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .RegisterPostEvictionCallback(CacheCallBack, this)
                    .SetSize(1);
                if (cache != null) cache.Set(cacheKey, data, cacheOptions);
            }
            else data = (BaseDto<PaginatedItemsDto<GetProductTypeResultDto>>)cache.Get(cacheKey);
            return Ok(data);
        }

        // GET api/<ProductTypeController>/5
        /// <summary>
        /// Get Product Type By Id
        /// </summary>
        /// <param name="id">Product Type Id</param>
        /// <returns>Product Type</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetProductTypeByIdRequest request = new GetProductTypeByIdRequest(id);
            BaseDto<GetProductTypeByIdDto> MediateResult = mediator.Send(request).Result;
            if (!MediateResult.IsSuccess) return NotFound(MediateResult);
            GetProductTypeResultDto result = mapper.Map<GetProductTypeResultDto>(MediateResult.Data);
            result.Links = GenerateLink(result.Id);
            return Ok(result);
        }

        // POST api/<ProductTypeController>
        /// <summary>
        /// Add New Product Type
        /// </summary>
        /// <param name="request">Product Type Information</param>
        /// <returns>Product Type</returns>
        [HttpPost]
        public IActionResult Post([FromBody] AddNewProductTypeRequestDto request)
        {
            SendProductTypeCommand command = new SendProductTypeCommand(mapper.Map<SendProductTypeDto>(request));
            BaseDto<SendProductTypeResponseDto> MediateResult = mediator.Send(command).Result;
            if (!MediateResult.IsSuccess) return BadRequest(MediateResult);
            AddNewProductTypeResultDto result = mapper.Map<AddNewProductTypeResultDto>(MediateResult.Data);
            result.Links = GenerateLink(result.Id);
            cache.Remove(cacheKey);
            return Ok(new BaseDto<AddNewProductTypeResultDto>(true, MediateResult.Message, result));
        }

        // PUT api/<ProductTypeController>/5
        /// <summary>
        /// Edit Poduct Type
        /// </summary>
        /// <param name="id">Product Type Id</param>
        /// <param name="request">Product Type Information</param>
        /// <returns>Product Type</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditProductTypeRequestDto request)
        {
            EditProductTypeDto requestDto = mapper.Map<EditProductTypeDto>(request);
            EditProductTypeCommand command = new EditProductTypeCommand(requestDto, id);
            BaseDto<EditProductTypeResponseDto> MediateResult = mediator.Send(command).Result;
            if (!MediateResult.IsSuccess) return NotFound(MediateResult);
            EditProductTypeResultDto result = mapper.Map<EditProductTypeResultDto>(MediateResult.Data);
            result.Links = GenerateLink(result.Id);
            cache.Remove(cacheKey);
            return Ok(new BaseDto<EditProductTypeResultDto>(true, MediateResult.Message, result));
        }

        // DELETE api/<ProductTypeController>/5
        /// <summary>
        /// Delete Product Type
        /// </summary>
        /// <param name="id">Product Type Id</param>
        /// <returns>Information</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteProductTypeCommand command = new DeleteProductTypeCommand(id);
            BaseDto result = mediator.Send(command).Result;
            if (!result.IsSuccess) return NotFound(result);
            cache.Remove(cacheKey);
            return Ok(result);
        }
        /// <summary>
        /// Generate Link for this Controller Methodes
        /// </summary>
        /// <param name="Id">Product Type Id</param>
        /// <returns>List Links</returns>
        private List<Links> GenerateLink(int Id)
        {
            return new List<Links>
                {
                    new Links
                    {
                        Href = Url.Action(nameof(Get), "ProductType", new { Id }, Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Delete), "ProductType", new { Id }, Request.Scheme),
                        Rel = "Delete",
                        Method = "Delete"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Put), "ProductType", new { Id }, Request.Scheme),
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
