using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceMaker.Controllers
{
    public class InvoicesController : BaseController
    {
        public ActionResult Index()
        {
            InvoicesRepository repository = new InvoicesRepository(context);
            List<Invoice> invoices = repository.GetList();
            return View("Index", invoices);
        }

        //GET
        public ActionResult Create()
        {
            CreateInvoiceView model = new CreateInvoiceView();
            model.Clients = new ClientRepository(context).GetClients();

            return View("Create", model);
        }

        //POST
        [HttpPost]
        public ActionResult Create(CreateInvoiceView model)
        {
            try
            {
                Client client = new ClientRepository(context).GetById(model.ClientId);//retrieving these from the DB stores them in the current context which prevents duplicate entries

                Invoice invoice = new Invoice(model.InvoiceNumber, client.Id, client);
                new InvoicesRepository(context).Insert(invoice);
                return RedirectToAction("Index");
            }
            catch { }

            CreateInvoiceView viewmodel = new CreateInvoiceView();
            viewmodel.ClientId = model.ClientId;
            viewmodel.InvoiceNumber = model.InvoiceNumber;
            viewmodel.Status = model.Status;
            return View(model);
        }

        //GET
        public ActionResult Edit (int id)
        {
            ClientRepository repository = new ClientRepository(context);
            List<Client> clients = repository.GetClients();

            Invoice currentInvoice = new InvoicesRepository(context).GetById(id);
            WorkDoneRepository repo = new WorkDoneRepository(context);
            List<WorkDone> workDones = repo.GetWorkDoneListCustom(currentInvoice.ClientId);
            EditInvoiceView model = new EditInvoiceView(workDones);
            model.Clients = clients;
            model.ClientId = currentInvoice.ClientId;
            model.InvoiceNumber = currentInvoice.InvoiceNumber;
            model.Status = currentInvoice.Status;
            model.Id = id;
            return View("Edit",model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(EditInvoiceView model, int id)
        {
            InvoicesRepository repository = new InvoicesRepository(context);
            Client client = new ClientRepository(context).GetById(model.ClientId);

            Invoice currentInvoice = new Invoice(model.InvoiceNumber, model.ClientId, client);
            currentInvoice.Id = id;
            client = null;
            if (model.Status == InvoiceStatus.Finalized)
            {
                currentInvoice.FinalizeInvoice();
            }
            if (model.Status == InvoiceStatus.Closed)
            {
                currentInvoice.CloseInvoice();
            }

            List<WorkDone> workDones = new WorkDoneRepository(context).GetWorkDoneListCustom(currentInvoice.ClientId);
            EditInvoiceView viewModel = new EditInvoiceView(workDones);
            ClientRepository repo = new ClientRepository(context);
            List<Client> clients = repo.GetClients();
            viewModel.Clients = clients;
            viewModel.Status = model.Status;
            viewModel.ClientId = model.ClientId;
            viewModel.InvoiceNumber = model.InvoiceNumber;

            repository.Update(currentInvoice);

            return View("Edit", viewModel);
        }

        public ActionResult LineItem (int id, int workdoneid)
        {
            WorkDone workDone = new WorkDoneRepository(context).GetById(workdoneid);
            WorkLineItem item = new WorkLineItem(workDone);
            InvoicesRepository repo = new InvoicesRepository(context);
            Invoice invoice =repo.GetById(id);
            invoice.LineItems.Add(item);
            repo.Update(invoice);
            
            return View("Index");
        }

    }
}