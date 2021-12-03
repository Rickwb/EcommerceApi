using EcommerceApi.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcommerceApi.Enums.Enums;

namespace EcommerceApi.Services
{
    public class PedidoService
    {
        private readonly List<Pedido> _pedidoService;
        private readonly ClienteService _clienteService;
        public PedidoService()
        {
            _pedidoService ??= new List<Pedido>();
        }
        public Entidades.Pedido Get(Guid id) => _pedidoService.Where(c => c.ID == id).SingleOrDefault();
        public IEnumerable<Pedido> GetAll() => _pedidoService;

        public Pedido Cadastrar(Pedido cli)
        {
            _pedidoService.Add(cli);
            return cli;
        }

        public bool Deletar(Guid id)
        {
            var cliente = _pedidoService.SingleOrDefault(c => c.ID == id);
            if (cliente == null) return false;
            _pedidoService.Remove(cliente);
            return true;
        }

        public Pedido Atualizar(Guid id, Pedido pedido)
        {
            var cliente = _pedidoService.SingleOrDefault(c => c.ID == id);
            int index = _pedidoService.IndexOf(cliente);

            _pedidoService.RemoveAt(index);
            _pedidoService.Insert(index, pedido);

            return cliente;
        }
        public ItemPedido AdicionarItem(Guid idPedido, ItemPedido item)
        {
            var pedido = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            pedido.AdicionarItemPedido(item);
            CalcularValor(pedido);
            return item;
        }
        public IEnumerable<ItemPedido> BuscarItensPedidos(Guid idPedido)
        {
            var pe = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            if (pe is not null) return pe.ItensPedido;
            return null;
        }
        public bool RemoverItemPedido(ItemPedido item, Guid idPedido)
        {
            var pe = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            int qtd = pe.ItensPedido.Count();
            if (pe is not null)
            {
                pe.Removerpedido(item);
                CalcularValor(pe);
                return qtd > pe.ItensPedido.Count() ? true : false;
            }
            return false;
        }
        public bool EsvaziarCarrinho(Guid idPedido)
        {
            var pedido = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            int index = _pedidoService.IndexOf(pedido);
            pedido.RemoverTodosItens();
            if (pedido.ItensPedido.Count == 0)
            {
                _pedidoService[index] = pedido;
                return true;
            }
            else
            {
                return false;
            }
        }

        public ItemPedido AtualizarItemPedido(Guid id,Guid IdItem, ItemPedido itemPedido)
        {
            
            var item = itemPedido.Pedido.ItensPedido.SingleOrDefault(p => p.ID == IdItem);
            var pedido = _pedidoService.SingleOrDefault(p => p.ID == id);
            pedido.AtualizarItemPedido(IdItem, itemPedido);

            CalcularValor(pedido);
            return itemPedido;
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

        public bool FinalizarPedido(Guid idPedido, FormaPagamento formaPagamento)
        {
            var pedido = _pedidoService.SingleOrDefault(p => p.ID == idPedido);
            //ValidarFormaDePagamento(formaPagamento);
            pedido.FormaPagamento = formaPagamento;

            pedido.DefinirPago();
            return true;
        }
        public bool ValidarFormaDePagamento(FormaPagamento formaPagamento)
        {
            switch (formaPagamento.TipoPagamento)
            {
                case Enums.Enums.Epagamento.Pix:
                    ValidarPix((Pix)formaPagamento);
                    return true;
                    break;
                case Enums.Enums.Epagamento.Boleto:
                    ValidarBoleto((Boleto)formaPagamento);
                    return true;
                    break;
                case Enums.Enums.Epagamento.CartaoDebito:
                    ValidarCartaoDebito((CartaoDebito)formaPagamento);
                    return true;
                    break;
                case Enums.Enums.Epagamento.CartaoCredito:
                    ValidarCartaoCredito((CartaoCredito)formaPagamento);
                    return true;
                    break;
            }
            return false;
        }

        private bool ValidarCartaoCredito(CartaoCredito cartaoCredito)
        {
            try
            {
                if (string.IsNullOrEmpty(cartaoCredito.NumBanco) || string.IsNullOrEmpty(cartaoCredito.Agencia) || string.IsNullOrEmpty(cartaoCredito.Conta) || cartaoCredito.CVV == 0 || cartaoCredito.Limite == 0)
                {
                    throw new ArgumentNullException("Campos");
                }
                if (cartaoCredito.Agencia.Length != 5 || cartaoCredito.Conta.Length != 6 || cartaoCredito.DataValidade < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException("Valores invalidos");
                }
                if ((cartaoCredito.Valor != 0 && cartaoCredito.Valor > cartaoCredito.Limite) || cartaoCredito.Valor == 0)
                {
                    throw new ArgumentOutOfRangeException("VALOR Maior que o limite");
                }
                cartaoCredito.Valido = true;
                return true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                cartaoCredito.Valido = true;
                return false;
            }
            catch (ArgumentNullException e)
            {
                cartaoCredito.Valido = true;
                return false;

            }
        }

        private bool ValidarCartaoDebito(CartaoDebito cartaoDebito)
        {
            try
            {
                if (string.IsNullOrEmpty(cartaoDebito.NumBanco) || string.IsNullOrEmpty(cartaoDebito.Agencia) || string.IsNullOrEmpty(cartaoDebito.Conta) || cartaoDebito.CVV == 0 || cartaoDebito.Limite == 0)
                {
                    throw new ArgumentNullException("Campos");
                }
                if (cartaoDebito.Agencia.Length != 5 || cartaoDebito.Conta.Length != 6 || cartaoDebito.DataValidade < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException("Valores invalidos");
                }
                if ((cartaoDebito.Valor != 0 && cartaoDebito.Valor > cartaoDebito.Limite) || cartaoDebito.Valor == 0)
                {
                    throw new ArgumentOutOfRangeException("VALOR Maior que o limite");
                }
                cartaoDebito.Valido = true;
                return true;
            }
            catch (ArgumentOutOfRangeException a)
            {
                cartaoDebito.Valido = true;
                return false;
            }
            catch (ArgumentNullException e)
            {
                cartaoDebito.Valido = true;
                return false;

            }
        }

        private bool ValidarBoleto(Boleto boleto)
        {
            try
            {
                if (string.IsNullOrEmpty(boleto.NumBanco) || string.IsNullOrEmpty(boleto.Agencia) || string.IsNullOrEmpty(boleto.Benefinciario)
                    || boleto.Vencimento == DateTime.MinValue || String.IsNullOrEmpty(boleto.NumDocumento) || boleto.Valor == 0)
                {
                    throw new ArgumentNullException("Campos");
                }
                if (boleto.Vencimento < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException("A não é possivel criar um boleto com o vencimento anterior ao dia atual");
                }
                boleto.Valido = true;
                return true;
            }
            catch (ArgumentNullException a)
            {
                boleto.Valido = false;
                return false;
            }
            catch (ArgumentOutOfRangeException a)
            {
                boleto.Valido = false;
                return false;
            }
        }

        private bool ValidarPix(Pix pix)
        {
            try
            {

                if (Enum.IsDefined(pix.TipoChave) || String.IsNullOrEmpty(pix.Chave))
                {
                    throw new ArgumentNullException();
                }
                if (pix.TipoChave == PixType.Telefone && pix.Chave.Any(x => char.IsLetter(x)))
                {
                    throw new ArgumentException("O telfone não pode conter letras");
                }
                if (pix.TipoChave == PixType.Email && !pix.Chave.Contains("@"))
                {
                    throw new ArgumentException("O email não foi informado corretamente");
                }
                if (pix.TipoChave == PixType.Cpf)
                {
                    pix.Chave = pix.Chave.Trim().Replace(".", "").Replace("-", "");
                    if (pix.Chave.Length != 11)
                        throw new ArgumentException("O Cpf foi informado incorretamente");
                }
                pix.Valido = true;
                return true;
            }
            catch (ArgumentNullException a)
            {
                pix.Valido = false;
                return false;
            }
            catch (ArgumentException x)
            {
                pix.Valido = false;
                return false;
            }
        }
    }
}
