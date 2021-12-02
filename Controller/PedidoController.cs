using EcommerceApi.DTOs;
using EcommerceApi.Entidades;
using EcommerceApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EcommerceApi.Controller
{
    [ApiController,Route("[controller]")]
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

            var guid = Guid.NewGuid();
            var ped = new Pedido(
                id: guid,
                name: pedidoDTO.Nome,
                cliente: pedidoDTO.Cliente,
                formaPagamento: pedidoDTO.FormaPagamento
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
            if (!pedidoDTO.Valido) return BadRequest();

            var guid = Guid.NewGuid();
            var ped = new Pedido(
                id: guid,
                name: pedidoDTO.Nome,
                cliente: pedidoDTO.Cliente,
                formaPagamento:pedidoDTO.FormaPagamento
                );

            return Created("", _pedidoService.Atualizar(id, ped));
        }
        [HttpPost, Route("{id}/ItemPedido")]
        public IActionResult AdicionarItemPedido(Guid idPedido,ItemPedidoDTO item)
        {
            if (!item.Valido) return BadRequest("As informações do pedido não estão corretas");
            var guid = Guid.NewGuid();
            var itemPedido = new ItemPedido(
                id: guid,
                pedido: item.Pedido,
                produto: item.Produto,
                qtd: item.QtdProdutos);
            var pedido=_pedidoService.Get(idPedido);
            pedido.ItensPedido.Add(itemPedido);
            CalcularValor(pedido);

            return Ok(_pedidoService.Atualizar(idPedido,pedido));
        }
        [HttpGet, Route("{id}/ItemPedido/{id}")]
        public IActionResult BuscarItensPed(Guid idPedido)
        {
            return Ok(_pedidoService.BuscarItensPedidos(idPedido));
        }
        [HttpDelete,Route("{id}/ItemPedido")]
        public IActionResult RemoverTodosPedidos(Guid idPedido)
        {
            if (_pedidoService.EsvaziarCarrinho(idPedido))
                return NoContent();
            else
                return BadRequest("Não foi possivel executar a ação ");
        }
        [HttpDelete, Route("{id}/ItemPedido/{Id}")]
        public IActionResult RemoverPed(ItemPedidoDTO item,Guid idPedido)
        {
            var guid = Guid.NewGuid();
            var itemPedido = new ItemPedido(
                id: guid,
                pedido: item.Pedido,
                produto: item.Produto,
                qtd: item.QtdProdutos);
            _pedidoService.RemoverItemPedido(itemPedido, idPedido);
            CalcularValor(_pedidoService.Get(idPedido));
            return Ok(_pedidoService.Atualizar());
        }
        [HttpPut, Route("{id}/")]
        public IActionResult AtualizarItemPed(ItemPedidoDTO item,Guid idPedido)
        {
            var guid = Guid.NewGuid();
            var itemPedido = new ItemPedido(
                id: guid,
                pedido: item.Pedido,
                produto: item.Produto,
                qtd: item.QtdProdutos);
            _pedidoService.AtualizarItemPedido(idPedido, itemPedido);
            var pedido = _pedidoService.Get(idPedido);
            CalcularValor(pedido);
            return Ok(_pedidoService.Atualizar(idPedido,pedido));
        }

        public static Pedido CalcularValor(Pedido pe)
        {
            pe.ValorTotal = 0;
            foreach (var item in pe.ItensPedido)
            {
                pe.ValorTotal += item.Produto.Preco * item.QtdProdutos;
            }
            return pe;
        }

    }
}


