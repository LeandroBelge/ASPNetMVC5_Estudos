using CaelumEstoque.DAO;
using CaelumEstoque.Filtros;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        //
        // GET: /Produto/
        [Route("produtos", Name="ListaProdutos")]
        public ActionResult Index()
        {
            var produtosDAO = new ProdutosDAO();
            IList<Produto> produtos = produtosDAO.Lista();
            return View(produtos);
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
        [ValidateAntiForgeryToken]
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

        [Route("produtos/{Id}", Name="VisualizaProdutos")]
        public ActionResult Visualiza(int Id)
        {
            var DAO = new ProdutosDAO();
            Produto produto = DAO.BuscaPorId(Id);
            return View(produto);
        }

        [HttpPost]
        public ActionResult DecrementaQtd(int Id)
        {
            var DAO = new ProdutosDAO();
            Produto produto = DAO.BuscaPorId(Id);
            produto.Quantidade--;
            DAO.Atualiza(produto);
            return Json(produto);
        }
	}
}