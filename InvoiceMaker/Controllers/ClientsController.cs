using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class ClientsController : Controller 
    {
        //get
        public ActionResult Index()
        {
            ClientRepository repo = new ClientRepository();
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
            ClientRepository repo = new ClientRepository();
            try
            {
                Client newClient = new Client(0, client.Name, client.IsActivated);
                repo.Insert(newClient);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
            return View("Create", client);
        }

        //GET
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ClientRepository repository = new ClientRepository();
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
            ClientRepository repository = new ClientRepository();
            try
            {
                Client newClient = new Client(id, client.Name, client.IsActivated);
                repository.Update(newClient);
                return RedirectToAction("Index");
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                {
                    ModelState.AddModelError("Name", "That name is already taken.");
                }
            }
            return View("Edit", client);
        }
    }
}