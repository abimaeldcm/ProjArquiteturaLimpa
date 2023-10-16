﻿using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sistema_escolar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            var products = await _productService.GetProducts();

            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductViewModel>> BuscarPorId(int id)
        {
            ProductViewModel produto = await _productService.GetById(id);
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult<ProductViewModel> Adicionar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                _productService.Add(productDTO);
                return Ok();
            }
            catch (Exception erro)
            {

                return BadRequest(new Exception("Não foi possivel adicionar o produto: " + erro.Message));
            }
        }
        [HttpPut]
        public ActionResult<ProductViewModel> Editar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                _productService.Update(productDTO);
                return Ok("Produto alterado com suceso.");
            }
            catch (Exception erro)
            {
                return BadRequest(new Exception("Não foi possivel realizar o update o produto: " + erro.Message));
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<ProductViewModel> Deletar(int id)
        {
            _productService.Delete(id);
            return Ok("Produto Deletado");
        }
    }
}
