using Microsoft.AspNetCore.Mvc;
using EcommerceApi.Services;
using System;
using EcommerceApi.DTOs;
using EcommerceApi.Entidades;

namespace EcommerceApi.Controller
{
    [ApiController, Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        public ClienteController()
        {
            _clienteService = new ClienteService();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_clienteService.GetAll());
        }
        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_clienteService.Get(id));
        }
        [HttpPost]
        public IActionResult CadastrarCliente(ClienteDTO cliD)
        {
            cliD.Validar();
            if (!cliD.Valido) return BadRequest("As informaçoes do cliente estão inválidas ");

            var cliente = new Cliente
                (
                id: Guid.NewGuid(),
                nome: cliD.Nome,
                sobrenome: cliD.Sobrenome,
                documento: cliD.Documento,
                idade: cliD.Idade,
                e: cliD.TipoPessoa
                );

            return Created("", _clienteService.Cadastrar(cliente));
        }
        [HttpPut,Route("{id}")]
        public IActionResult Atualizar(Guid id, ClienteDTO cliDTO)
        {
            cliDTO.Validar();
            if (!cliDTO.Valido) return BadRequest("As informações do cliente estao invalidas");

            var cliente = new Cliente(
                id: Guid.NewGuid(),
                nome: cliDTO.Nome,
                sobrenome: cliDTO.Sobrenome,
                idade: cliDTO.Idade,
                documento: cliDTO.Documento,
                e: cliDTO.TipoPessoa);


            return Ok(_clienteService.Atualizar(id, cliente));
        }
        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (_clienteService.Deletar(id))
                return NoContent();

            return BadRequest();
        }
    }
}
