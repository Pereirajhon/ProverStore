/*
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    public class CarrinhoRepository: IShoppingCartRepository
    {
        private readonly DbSet<ShoppingCartItem> _items;
        private readonly AppDbContext Db;
      //  private readonly DbSet<ShoppingCart> _carrinho;
        public CarrinhoRepository(AppDbContext db) 
        {
           // _carrinho = carrinho;
            _items = db.Set<ShoppingCartItem>();
            Db = db;
        } 

        public async Task<IEnumerable<ShoppingCartItem>> ObterListaCarrinho()
        {
            return await _items.ToListAsync();

        }

        public async Task AddAoCarrinho(Guid id)
        {
            var produto = await Db.Produtos.FindAsync(id);

            var carrinhoItem = new ShoppingCartItem()
            {               
                Item = produto,
                ProdutoId = produto.ProdutoId,
                Quantidade = 1,
            };
        
            var carrinhoDb = await _items.FindAsync(carrinhoItem);

            if(produto.ProdutoId == carrinhoDb.ProdutoId)
            {
                carrinhoDb.Quantidade += 1;

                produto.Estoque -= carrinhoDb.Quantidade;

                await Db.SaveChangesAsync();
              
            }

            produto.Estoque -= carrinhoItem.Quantidade;

            await _items.AddAsync(carrinhoItem);

            await Db.SaveChangesAsync();

        }

        public async Task RemoverDoCarrinho(Guid carrinhoId)
        {

            var item = await _items.FindAsync(new ShoppingCartItem { Id = carrinhoId });

            if(item != null) _items.Remove(new ShoppingCartItem { Id = carrinhoId });

            await Db.SaveChangesAsync();
           
        }

        public async Task EsvaziarCarrinho()
        {
            _items.RemoveRange(await ObterListaCarrinho());
            await Db.SaveChangesAsync();
        }

        public async Task Decrementar(Guid carrinhoId)
        {
            var carrinho = await _items.SingleOrDefaultAsync(c => c.Id == carrinhoId);
            if (carrinho != null)
            {
                if (carrinho.Quantidade > 1)
                {           
                    carrinho.Quantidade -= 1;

                    await Db.SaveChangesAsync();
                }
                else
                {
                    await RemoverDoCarrinho(carrinhoId);
                }

            }

        }       

        private async Task<bool> CarrinhoExist(ShoppingCartItem carrinho)
        {
           
            return await _items.AnyAsync(c => c.Id == carrinho.Id);
            

        }
    }
}
*/