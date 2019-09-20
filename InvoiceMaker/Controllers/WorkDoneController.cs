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
    public class WorkDoneController : BaseController
    {
        // GET: WorkDone
        public ActionResult Index()
        {
            WorkDoneRepository repository = new WorkDoneRepository(context);
            List<WorkDone> worksDone = repository.GetWorkDoneList();
            return View("Index", worksDone);
        }

        //Get
        public ActionResult Create()
        {
            CreateWorkDoneView model = new CreateWorkDoneView();
            model.Clients = new ClientRepository(context).GetClients();
            model.WorkTypes = new WorkTypeRepository(context).GetWorkType();
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
                Client client = new ClientRepository(context).GetById(model.ClientId);//retrieving these from the DB stores them in the current context which prevents duplicate entries
                WorkType workType = new WorkTypeRepository(context).GetWorkTypeById(model.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type
                WorkDone workDone = new WorkDone(0, client, workType);
                workDone.Client = null;
                workDone.WorkType = null;
                new WorkDoneRepository(context).Insert(workDone);
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
            viewModel.Clients = new ClientRepository(context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(context).GetWorkType();
            return View("Create", viewModel);
        }

        //GET
        public ActionResult Edit(int id)
        {
            WorkDoneRepository repository = new WorkDoneRepository(context);
            WorkDone workDone = repository.GetById(id);
            EditWorkDoneView model = new EditWorkDoneView();

            if (workDone.EndedOn.HasValue)
            {
                model.EndedOn = workDone.EndedOn;
            }
            model.StartedOn = workDone.StartedOn;
            model.ClientId = workDone.ClientId;
            model.WorkTypeId = workDone.WorkTypeId;
            model.Clients = new ClientRepository(context).GetClients();
            model.WorkTypes = new WorkTypeRepository(context).GetWorkType();
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
                Client client = new ClientRepository(context).GetById(model.ClientId);
                WorkType workType = new WorkTypeRepository(context).GetWorkTypeById(model.WorkTypeId);

                // Create an instance of the work done with the client and work
                // type

                //if model.endedon contains value
                WorkDone workDone;
                if (model.EndedOn.HasValue && (model.EndedOn > model.StartedOn)) { workDone = new WorkDone(id, client, workType, model.StartedOn, model.EndedOn); }
                else { workDone = new WorkDone(id, client, workType, model.StartedOn); }
                new WorkDoneRepository(context).Update(workDone);
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
            viewModel.Clients = new ClientRepository(context).GetClients();
            viewModel.WorkTypes = new WorkTypeRepository(context).GetWorkType();
            return View("Edit", viewModel);
        }
    }
}