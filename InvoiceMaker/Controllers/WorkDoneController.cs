using InvoiceMaker.FormModels;
using InvoiceMaker.Models;
using InvoiceMaker.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static InvoiceMaker.FormModels.CreateWorkDone;

namespace InvoiceMaker.Controllers
{
    public class WorkDoneController : Controller
    {
        // GET: WorkDone
        public ActionResult Index()
        {
            WorkDoneRepository repository = new WorkDoneRepository();
            List<WorkDone> worksDone = repository.GetWorkDoneList();
            return View("Index", worksDone);
        }

        //Get
        public ActionResult Create()
        {
            CreateWorkDoneView model = new CreateWorkDoneView();
            model.Clients = new ClientRepository(null).GetClients();
            model.WorkTypes = new WorkTypeRepository(null).GetWorkType();
            return View("Create", model);
        }


        //POST
        [HttpPost]
        public ActionResult Create(CreateWorkDone model)
        {
            try
            {
                // Get the client and work type based on values submitted from
                // the form
                Client client = new ClientRepository(null).GetById(model.ClientId);
                WorkType workType = new WorkTypeRepository(null).GetWorkTypeById(model.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type
                WorkDone workDone = new WorkDone(0, client, workType);
                new WorkDoneRepository().Insert(workDone);
                return RedirectToAction("Index");
            }
            catch { }

            // Create a view model
            CreateWorkDoneView viewModel = new CreateWorkDoneView();

            // Copy over the values from the values submitted
            viewModel.ClientId = model.ClientId;
            viewModel.StartedOn = model.StartedOn;
            viewModel.WorkTypeId = model.WorkTypeId;

            // Go get the value for the drop-downs, again.
            viewModel.Clients = new ClientRepository(null).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(null).GetWorkType();
            return View("Create", viewModel);
        }

        //GET
        public ActionResult Edit(int id)
        {
            WorkDoneRepository repository = new WorkDoneRepository();
            WorkDone workDone = repository.GetById(id);
            EditWorkDoneView model = new EditWorkDoneView();

            if (workDone.EndedOn.HasValue)
            {
                model.EndedOn = workDone.EndedOn;
            }
            model.StartedOn = workDone.StartedOn;
            model.ClientId = workDone.ClientId;
            model.WorkTypeId = workDone.WorkTypeId;
            model.Clients = new ClientRepository(null).GetClients();
            model.WorkTypes = new WorkTypeRepository(null).GetWorkType();
            return View("Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit(int id, EditWorkDoneView model)
        {
            try
            {
                // Get the client and work type based on values submitted from
                // the form
                Client client = new ClientRepository(null).GetById(model.ClientId);
                WorkType workType = new WorkTypeRepository(null).GetWorkTypeById(model.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type

                //if model.endedon contains value
                WorkDone workDone;
                if (model.EndedOn.HasValue && (model.EndedOn > model.StartedOn)) { workDone = new WorkDone(id, client, workType, model.StartedOn, model.EndedOn); }
                else { workDone = new WorkDone(id, client, workType, model.StartedOn); }
                new WorkDoneRepository().Update(workDone);
                return RedirectToAction("Index");
            }
            catch { }

            // Create a view model
            EditWorkDoneView viewModel = new EditWorkDoneView();

            // Copy over the values from the values submitted
            viewModel.ClientId = model.ClientId;
            viewModel.StartedOn = model.StartedOn;
            viewModel.EndedOn = model.EndedOn;
            viewModel.WorkTypeId = model.WorkTypeId;

            // Go get the value for the drop-downs, again.
            viewModel.Clients = new ClientRepository(null).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(null).GetWorkType();
            return View("Edit", viewModel);
        }
    }
}