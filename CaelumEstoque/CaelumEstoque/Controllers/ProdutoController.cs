using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/
        public ActionResult Index()
        {
            var produtosDAO = new ProdutosDAO();
            IList<Produto> produtos = produtosDAO.Lista();
            ViewBag.Produtos = produtos;
            return View();
        }

        public ActionResult Form()
        {
            var categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        public ActionResult Adiciona(Produto produto)
        {
            this.Validar(produto);

            if (ModelState.IsValid)
            {
                var Dao = new ProdutosDAO();
                Dao.Adiciona(produto);
                return RedirectToAction("Index");
            }
            else
            {
                var categoriasDAO = new CategoriasDAO();
                IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
                ViewBag.Categorias = categorias;
                ViewBag.Produto = produto;
                return View("Form");
            }
        }

        private void Validar(Produto produto)
        {
            if (produto.CategoriaId == 1 && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido", "O preço do produto não pode ser menor que R$ 100,00");
            }
        }
	}
}