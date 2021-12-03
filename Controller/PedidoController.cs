using EcommerceApi.DTOs;
using EcommerceApi.Entidades;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceApi.Controller
{
    [ApiController, Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _pedidoService;


        public PedidoController(PedidoService _pedidoSer)
        {
            _pedidoService = _pedidoSer;
        }
        [HttpPost]
        public IActionResult Cadastrar(PedidoDTO pedidoDTO)
        {
            if (!pedidoDTO.Valido) return BadRequest();

            pedidoDTO.Validar();
            var ped = new Pedido(
                id: pedidoDTO.Id.Value,
                name: pedidoDTO.Nome,
                cliente: new Cliente(
                    id: pedidoDTO.Cliente.Id.Value,
                    nome: pedidoDTO.Cliente.Nome,
                    sobrenome: pedidoDTO.Cliente.Sobrenome,
                    documento: pedidoDTO.Cliente.Documento,
                    idade: pedidoDTO.Cliente.Idade,
                    e: pedidoDTO.Cliente.TipoPessoa)
                );

            return Created("", _pedidoService.Cadastrar(ped));
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Ok(_pedidoService.GetAll());
        }

        [HttpGet, Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_pedidoService.Get(id));
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            _pedidoService.Deletar(id);
            return NoContent();

            return BadRequest();
        }

        [HttpPut, Route("{id}")]
        public IActionResult Atualizar(Guid id, PedidoDTO pedidoDTO)
        {
            pedidoDTO.Validar();
            if (!pedidoDTO.Valido) return BadRequest();

            var ped = new Pedido(
                id: pedidoDTO.Id.Value,
                name: pedidoDTO.Nome,
                cliente: new Cliente(
                    id: pedidoDTO.Cliente.Id.Value,
                    nome: pedidoDTO.Cliente.Nome,
                    sobrenome: pedidoDTO.Cliente.Sobrenome,
                    documento: pedidoDTO.Cliente.Documento,
                    idade: pedidoDTO.Cliente.Idade,
                    e: pedidoDTO.Cliente.TipoPessoa)
                );

            return Created("", _pedidoService.Atualizar(id, ped));
        }
        [HttpPost, Route("{idPedido}/ItemPedido")]
        public IActionResult AdicionarItemPedido(Guid idPedido, ItemPedidoDTO item)
        {
            item.Validar();
            if (!item.Valido) return BadRequest("As informações do pedido não estão corretas");

            var itemPedido = new ItemPedido(
                id: item.Id.Value,
                
                produto: new Produto(
                    id: item.Produto.Id.Value,
                    nome: item.Produto.Nome,
                    descricao: item.Produto.Descricao,
                    preco: item.Produto.Preco),
                qtd: item.QtdProdutos);

            return Ok(_pedidoService.AdicionarItem(idPedido, itemPedido));
        }
        [HttpGet, Route("{id}/ItemPedido")]
        public IActionResult BuscarItensPed(Guid idPedido)
        {
            return Ok(_pedidoService.BuscarItensPedidos(idPedido));
        }
        [HttpDelete, Route("{id}/ItemPedido")]
        public IActionResult RemoverTodosPedidos(Guid idPedido)
        {
            if (_pedidoService.EsvaziarCarrinho(idPedido))
                return NoContent();
            else
                return BadRequest("Não foi possivel executar a ação ");
        }
        [HttpDelete, Route("{idPedido}/ItemPedido")]
        public IActionResult RemoverPed(ItemPedidoDTO item, Guid idPedido)
        {
            item.Validar();
            var itemPedido = new ItemPedido(
                id: item.Id.Value,
                produto: new Produto(
                    id: item.Produto.Id.Value,
                    nome: item.Produto.Nome,
                    descricao: item.Produto.Descricao,
                    preco: item.Produto.Preco),
                qtd: item.QtdProdutos);

            return Ok(_pedidoService.RemoverItemPedido(itemPedido, idPedido));
        }
        [HttpPut, Route("{idPedido}/ItemPedido")]
        public IActionResult AtualizarItemPed(ItemPedidoDTO item, Guid idPedido)
        {
            item.Validar();
            var itemPedido = new ItemPedido(
                id: item.Id.Value,
                produto: new Produto(
                    id: item.Produto.Id.Value,
                    nome: item.Produto.Nome,
                    descricao: item.Produto.Descricao,
                    preco: item.Produto.Preco),
                qtd: item.QtdProdutos);

            return Ok(_pedidoService.AtualizarItemPedido(idPedido, itemPedido.ID, itemPedido));
        }

        [HttpPost, Route("{idPedido}/Formapagamento")]
        public IActionResult FinalizarPedido(Guid idPedido, FinalizarPagamentoDTO finalizarPagamento)
        {
            if (finalizarPagamento.FormaPagamento== Enums.Enums.Epagamento.Pix)
            {
                var px = new Pix(
                    valor: finalizarPagamento.Valor,
                    chave: finalizarPagamento.Pix.Chave
                    );
                return Ok(_pedidoService.FinalizarPedido(idPedido,px));
            }
            if (finalizarPagamento.FormaPagamento == Enums.Enums.Epagamento.CartaoCredito)
            {
                var cardCredito = new CartaoCredito(
                    valor: finalizarPagamento.Valor,
                    numCartao: finalizarPagamento.CartaoCredito.NumCartao,
                    CVV: finalizarPagamento.CartaoCredito.CVV
                    );
                return Ok(_pedidoService.FinalizarPedido(idPedido, cardCredito));

            }
            if (finalizarPagamento.FormaPagamento == Enums.Enums.Epagamento.CartaoDebito)
            {
                var cartaoDebito = new CartaoDebito(
                    valor: finalizarPagamento.Valor);
                return Ok(_pedidoService.FinalizarPedido(idPedido, cartaoDebito));
            }
            if (finalizarPagamento.FormaPagamento== Enums.Enums.Epagamento.Boleto)
            {
                var Boleto = new Boleto(
                    valor: finalizarPagamento.Valor);
                return Ok(_pedidoService.FinalizarPedido(idPedido, Boleto));
            }

            return BadRequest();
        }

    }
}


