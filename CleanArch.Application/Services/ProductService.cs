﻿using AutoMapper;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
    public class ProductService : IProductService
    {
        protected private IProductRepository _repository;
        protected private IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var Products = await _repository.GetProducts();
            return _mapper.Map<IEnumerable<ProductViewModel>>(Products);
        }
        public ProductViewModel GetById(int? id)
        {

            Product product = _repository.GetById(id);
            return _mapper.Map<ProductViewModel>(product);
        }
        public void Add(ProductViewModel product)
        {
            Product productMap = _mapper.Map<Product>(product);
            _repository.Add(productMap);
        }
        public void Update(ProductViewModel product)
        {
            Product productMap = _mapper.Map<Product>(product);
            _repository.Update(productMap);
        }
        public void Delete(int id)
        {
            Product product = _repository.GetById(id);
            _repository.Delete(product);
        }
    }
}
