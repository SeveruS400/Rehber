﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record ProductDtoForInsertion : ProductsDto
    {
        public bool ConnStatus { get; set; }
    }
}
