using InvoiceMaker.Data;
using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class ClientsController : Controller 
    {
        private Context context;

        public ClientsController()
        {
            context = new Context();
        }
        //get
        public ActionResult Index()
        {
                ClientRepository repo = new ClientRepository(context);
                List<Client> clients = repo.GetClients();
                return View("Index", clients);
        }


        //Get
        public ActionResult Create()
        {
            CreateClient client = new CreateClient();
            client.IsActivated = true;
            return View("Create", client);
        }

        //POST
        [HttpPost]
        public ActionResult Create(CreateClient client)
        {
            ClientRepository repo = new ClientRepository(context);
            try
            {
                Client newClient = new Client(0, client.Name, client.IsActivated);
                repo.Insert(newClient);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                HandleDbUpdateException(ex);
            }
            return View("Create", client);
        }

        /// <summary>
        /// Just Read The Name Of The Method
        /// </summary>
        /// <param name="ex"></param>
        private void HandleDbUpdateException(DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.InnerException != null)
            {
                SqlException sqlException = ex.InnerException.InnerException as SqlException;
                if (sqlException != null && sqlException.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
        }

        //GET
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClientRepository repository = new ClientRepository(context);
            Client client = repository.GetById(id);
            EditClient model = new EditClient();
            if (client.Name != null)
            {
                model.Id = client.Id;
                model.IsActivated = client.IsActive;
                model.Name = client.Name;
            }
            return View("Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(int id, EditClient client)
        {
            ClientRepository repository = new ClientRepository(context);
            try
            {
                Client newClient = new Client(id, client.Name, client.IsActivated);
                repository.Update(newClient);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException se)
            {
                HandleDbUpdateException(se);
            }
            return View("Edit", client);
        }
        private bool disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (disposed == true)
            {
                return;
            }

            if (disposing)
            {
                context.Dispose();
            }

            disposed = true;

            base.Dispose(disposing);
        }
    }
}