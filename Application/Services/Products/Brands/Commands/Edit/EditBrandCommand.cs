using Domain.Dtos;
using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Brands.Commands.Edit
{
    public class EditBrandCommand : IRequest<BaseDto<EditBrandResponseDto>>
    {
        public EditBrandDto BrandDto { get; set; }
        public int Id { get; set; }
        public EditBrandCommand(EditBrandDto contactDto, int Id)
        {
            BrandDto = contactDto;
            this.Id = Id;
        }

    }

}
