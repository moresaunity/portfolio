using Domain.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.Brands.Commands.Edit
{
    public class EditBrandCommand : IRequest<EditBrandResponseDto>
    {
        public EditBrandDto BrandDto { get; set; }
        public EditBrandCommand(EditBrandDto contactDto)
        {
            BrandDto = contactDto;
        }

    }

}
