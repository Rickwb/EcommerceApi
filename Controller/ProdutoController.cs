using EcommerceApi.DTOs;
using EcommerceApi.Entidades;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceApi.Controller
{
    [ApiController, Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService _produtoSer)
        {
            _produtoService = _produtoSer;
        }
        [HttpPost]
        public IActionResult Cadastrar(ProdutoDTO produtoDTO)
        {
            if (!produtoDTO.Valido) return BadRequest();

            var guid = Guid.NewGuid();
            var prod = new Produto(
                id: guid,
                nome: produtoDTO.Nome,
                descricao: produtoDTO.Descricao,
                preco: produtoDTO.Preco
                );

            return Created("", _produtoService.Cadastrar(prod));
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_produtoService.GetAll()) ;
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_produtoService.GetProduto(id));
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _produtoService.Deletar(id);
            return NoContent();

            return BadRequest();
        }

        [HttpPut,Route("{id}")]
        public IActionResult Atualizar(Guid id, ProdutoDTO produtoDTO)
        {
            if (!produtoDTO.Valido) return BadRequest();

            var guid = Guid.NewGuid();
            var prod = new Produto(
                id: guid,
                nome: produtoDTO.Nome,
                descricao: produtoDTO.Descricao,
                preco: produtoDTO.Preco
                );

            return Created("", _produtoService.Atualizar(id,prod));
        }
    }
}
