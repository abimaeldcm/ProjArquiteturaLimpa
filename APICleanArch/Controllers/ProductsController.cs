using CleanArch.Application.Interfaces;
using CleanArch.Application.ViewModels;
using CleanArch.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IValidator<ProductViewModel> _productValidator;

        public ProductsController(IProductService productService,
            IValidator<ProductViewModel> productValidator)
        {
            _productService = productService;
            _productValidator = productValidator;
        }
        /// <summary>
        /// Buscar todos os produtos
        /// </summary>
        /// <remarks>
        /// Busca todos os produtos cadastrados no Banco de Dados
        /// </remarks>
        /// <param name="">""</param>
        /// <returns></returns>
        /// <response code="200">Produtos carregados com sucesso</response>
        /// <response code="400">Erro de validação  </response>
        /// <response code="401">Usuário não autorizado  </response>
        /// <response code="500">Erro no banco</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetAll()
        {
            try
            {
                var products = await _productService.GetProducts();

                return Ok(products);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        /// <summary>
        /// Buscar produto por Id
        /// </summary>
        /// <remarks>
        /// Ao enviar o Id do produto desejado o sistema retornará o produto.
        /// </remarks>
        /// <param name="id">"Id do item"</param>
        /// <returns></returns>
        /// <response code="200">Produtos localizado com sucesso</response>
        /// <response code="400">Erro de validação  </response>
        /// <response code="401">Usuário não autorizado  </response>
        /// <response code="500">Erro no banco</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ProductViewModel> BuscarPorId(int id)
        {
            ProductViewModel produto = _productService.GetById(id);
            return Ok(produto);
        }

        /// <summary>
        /// Adicionar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, um novo produto será adicionado ao Banco de Dados. 
        /// Obs: Não é necessário preencher o campo "id", pois ele é gerado automaticamente.
        /// </remarks>
        /// <param name="productDTO">"Produto a ser adicionado"</param>
        /// <returns></returns>
        /// <response code="200">Produto adicioado com sucesso</response>
        /// <response code="400">Erro de validação  </response>
        /// <response code="401">Usuário não autorizado. Apenas Manager podem realizar a ação.  </response>
        /// <response code="500">Erro no banco</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Authorize(Roles = "manager")]
        public ActionResult<ProductViewModel> Adicionar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                var validationResult = _productValidator.Validate(productDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                _productService.Add(productDTO);
                return Ok(productDTO);
            }
            catch (Exception erro)
            {

                return BadRequest(new Exception("Não foi possivel adicionar o produto: " + erro.Message));
            }
        }

        /// <summary>
        /// Editar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, será retornado um produto caso ele esteja no Banco de Dados. 
        /// </remarks>
        /// <param name="productDTO">"Produto a ser editado"</param>
        /// <returns></returns>
        /// <response code="200">Produto alterado com sucesso</response>
        /// <response code="400">Erro de validação  </response>
        /// <response code="401">Usuário não autorizado. Apenas Manager podem realizar a ação.  </response>
        /// <response code="500">Erro no banco</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Authorize(Roles = "manager")]
        public ActionResult<dynamic> Editar([FromBody] ProductViewModel productDTO)
        {
            try
            {
                var validationResult = _productValidator.Validate(productDTO);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }
                _productService.Update(productDTO);
                return Ok(new
                {
                    message = "Produto alterado com sucesso.",
                    productDTO                    
                });
            }
            catch (Exception erro)
            {
                return BadRequest(new Exception("Não foi possivel realizar o update o produto: " + erro.Message));
            }
        }
        /// <summary>
        /// Deletar um produto
        /// </summary>
        /// <remarks>
        /// Ao adicionar as informações, um novo produto será adicionado ao Banco de Dados. 
        /// </remarks>
        /// <param name="id">"Id do produto a ser deletado"</param>
        /// <returns></returns>
        /// <response code="200">Produto deletado com sucesso</response>
        /// <response code="400">Erro de validação  </response>
        /// <response code="401">Usuário não autorizado. Apenas Manager podem realizar a ação.  </response>
        /// <response code="500">Erro no banco</response>
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public ActionResult<ProductViewModel> Deletar(int id)
        {
            try
            {
                _productService.Delete(id);
                return Ok("Produto Deletado");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
            
        }
    }
}
