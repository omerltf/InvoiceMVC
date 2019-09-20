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
    public class WorkTypesController : BaseController
    {


        // GET: WorkType
        public ActionResult Index()
        {
            WorkTypeRepository repository = new WorkTypeRepository(context);
            List<WorkType> worktypes = repository.GetWorkType();
            return View("Index", worktypes);
        }

        //GET
        public ActionResult Create()
        {
            CreateWorkType workType = new CreateWorkType();
            return View("Create", workType);
        }

        //POST
        [HttpPost]
        public ActionResult Create(CreateWorkType workType)
        {
            WorkTypeRepository repository = new WorkTypeRepository(context);
            try
            {
                WorkType type = new WorkType(0, workType.WorkTypeName, workType.Rate);
                repository.Insert(type);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException se)
            {
                HandleDbUpdateException(se);
            }
            return View("Create", workType);
        }

        //GET
        public ActionResult Edit (int id)
        {
            WorkTypeRepository repository = new WorkTypeRepository(context);
            WorkType type = repository.GetWorkTypeById(id);
            EditWorkType model = new EditWorkType();

            if (type.Name != null)
            {
                model.Id = type.Id;
                model.WorkTypeName = type.Name;
                model.Rate = type.Rate;
            }

            return View("Edit", model);
        }

        //POST
        [HttpPost]
        public ActionResult Edit (int id, EditWorkType workType)
        {
            WorkTypeRepository repository = new WorkTypeRepository(context);
            try
            {
                WorkType type = new WorkType(id, workType.WorkTypeName, workType.Rate);
                repository.Update(type);
                return RedirectToAction("Index");
            }
            catch (DbUpdateException se)
            {
                HandleDbUpdateException(se);
            }
            return View("Edit", workType);
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

    }
}