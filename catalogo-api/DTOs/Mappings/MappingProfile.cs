﻿using AutoMapper;
using catalogo_api.Models;

namespace catalogo_api.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        //pacotes 
        // AutoMapper
        // AutoMapper.Extensions.Mycrosoft.DependencyInjection
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
