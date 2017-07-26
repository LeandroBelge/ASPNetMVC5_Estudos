using CaelumEstoque.DAO;
using CaelumEstoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        //
        // GET: /Categoria/
        public ActionResult Index()
        {
            var categoriaDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriaDAO.Lista();
            return View(categorias);
        }
	}
}