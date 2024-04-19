using Api.EndPoint.Models.Dtos;
using Api.EndPoint.Models.Dtos.Product.Favourite;
using Aplication.Services.Products.Favourites.Commands.Delete;
using Aplication.Services.Products.Favourites.Commands.Edit;
using Application.Services.Products.Favourites.Commands.Create;
using Application.Services.Products.Favourites.Queries;
using AutoMapper;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.EndPoint.Controllers.V1.Product
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/Product/Favourite/")]
    [ApiController]
    public class ProductFavouriteController : ControllerBase
    {
        private readonly IMemoryCache cache;
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly string CacheKey = "Favourites";

        public ProductFavouriteController(IMemoryCache cache, IMediator mediator, IMapper mapper)
        {
            this.cache = cache;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        // GET: api/<ProductFavouriteController>
        /// <summary>
        /// Get All Favourites
        /// </summary>
        /// <param name="page">Page Index</param>
        /// <param name="pageSize">Page Size</param>
        /// <returns>All Favourites</returns>
        [HttpGet("{page}, {pageSize}")]
        public IActionResult Get(int page = 1, int pageSize = 10)
        {
            BaseDto<PaginatedItemsDto<GetFavouriteResultDto>> result;
            if (cache == null || !cache.TryGetValue(CacheKey, out result))
            {
                GetFavouriteCommand command = new GetFavouriteCommand() { Page = page, PageSize = pageSize };
                GetFavouriteRequest request = new GetFavouriteRequest(command);
                BaseDto<PaginatedItemsDto<GetFavouriteResponseDto>> MediateResult = mediator.Send(request).Result;
                List<GetFavouriteResultDto> FavouriteList = mapper.Map<List<GetFavouriteResultDto>>(MediateResult.Data.Data);
                foreach (var item in FavouriteList) 
                {
                    item.ProductItem = mapper.Map<GetFavouriteProductItemResultDto>(MediateResult.Data.Data.First(p => p.Id == item.Id).Product);
                    item.ProductItem.ProductItemFavourites = null;
                }
                result = new BaseDto<PaginatedItemsDto<GetFavouriteResultDto>>(true, MediateResult.Message, new PaginatedItemsDto<GetFavouriteResultDto>(page, pageSize, MediateResult.Data.Count, FavouriteList));
                if (cache != null) foreach (var item in result.Data.Data) item.Links = GenerateLink(item.Id);
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .RegisterPostEvictionCallback(CacheCallBack, this)
                    .SetSize(1);
                if (cache != null) cache.Set(CacheKey, result, cacheOptions);
            }
            else result = (BaseDto<PaginatedItemsDto<GetFavouriteResultDto>>)cache.Get(CacheKey);

            return Ok(result);
        }

        // GET api/<ProductFavouriteController>/5
        /// <summary>
        /// Get Favorite By Id
        /// </summary>
        /// <param name="id">Favorite Id</param>
        /// <returns>Favorite</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            GetFavouriteByIdCommand command = new GetFavouriteByIdCommand() { Id = id };
            GetFavouriteByIdRequest request = new GetFavouriteByIdRequest(command);
            BaseDto<GetFavouriteByIdResponseDto> MediateResult = mediator.Send(request).Result;
            GetFavouriteResultDto FavouriteList = mapper.Map<GetFavouriteResultDto>(MediateResult.Data);
            FavouriteList.ProductItem = mapper.Map<GetFavouriteProductItemResultDto>(MediateResult.Data.Product);
            BaseDto<GetFavouriteResultDto> result = new BaseDto<GetFavouriteResultDto>(true, MediateResult.Message, FavouriteList);
            result.Data.Links = GenerateLink(result.Data.Id);

            return Ok(result);
        }

        // POST api/<ProductFavouriteController>
        /// <summary>
        /// Add New Favorite
        /// </summary>
        /// <param name="ProductId">Product Id</param>
        /// <param name="UserId">User Id</param>
        /// <returns>Favorite Information</returns>
        [HttpPost]
        public IActionResult Post([FromBody] int ProductId, string UserId)
        {
            CreateFavouriteDto request = new CreateFavouriteDto()
            {
                ProductItemId = ProductId,
                UserId = UserId
            };
            CreateFavouriteCommand command = new CreateFavouriteCommand(request);
            BaseDto<CreateFavouriteResponseDto> MediateResult = mediator.Send(command).Result;
            if (!MediateResult.IsSuccess) return BadRequest(MediateResult);
            AddNewFavouriteResultDto result = new AddNewFavouriteResultDto
            {
                Id = MediateResult.Data.Id,
                Links = GenerateLink(MediateResult.Data.Id)
            };
            return Ok(new BaseDto<AddNewFavouriteResultDto>(true, MediateResult.Message, result));
        }

        // PUT api/<ProductFavouriteController>/5
        /// <summary>
        /// Edit Favorite
        /// </summary>
        /// <param name="id">Favorite Id</param>
        /// <param name="ProductId">Product Id</param>
        /// <param name="UserId">User Id</param>
        /// <returns>Favorite Information</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] int ProductId, string UserId)
        {
            EditFavouriteDto request = new EditFavouriteDto { UserId = UserId, ProductId = ProductId };
            EditFavouriteCommand command = new EditFavouriteCommand(request ,id);
            BaseDto<EditFavouriteResponseDto> MediateResult = mediator.Send(command).Result;
            if(!MediateResult.IsSuccess) return NotFound(MediateResult);
            EditFavouriteResultDto result = mapper.Map<EditFavouriteResultDto>(MediateResult.Data);
            result.Links = GenerateLink(result.Id);
            cache.Remove(CacheKey);
            return Ok(new BaseDto<EditFavouriteResultDto>(true, MediateResult.Message, result));
        }

        // DELETE api/<ProductFavouriteController>/5
        /// <summary>
        /// Delete Favorite
        /// </summary>
        /// <param name="id">Favorite Id</param>
        /// <returns>Favorite Information</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteFavouriteCommand command = new DeleteFavouriteCommand(id);
            BaseDto result = mediator.Send(command).Result;
            if (!result.IsSuccess) return NotFound(result);
            cache.Remove(CacheKey);
            return Ok(result);
        }
        private List<Links> GenerateLink(int id)
        {
            return new List<Links>
                {
                    new Links
                    {
                        Href = Url.Action(nameof(Get), "ProductFavourite", new { id }, Request.Scheme),
                        Rel = "Self",
                        Method = "Get"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Delete), "ProductFavourite", new { id }, Request.Scheme),
                        Rel = "Delete",
                        Method = "Delete"
                    },
                    new Links
                    {
                        Href = Url.Action(nameof(Put), "ProductFavourite", new { id }, Request.Scheme),
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
