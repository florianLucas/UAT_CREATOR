﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UAT_CREATOR.DAL;
using UAT_CREATOR.Model;

namespace UAT_CREATOR.Controllers
{
    public class ApplicationController : Controller
    {

        protected UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Application
        public ActionResult Index()
        {


            return View(unitOfWork.ApplicationRepository.ToList());
        }


        public ActionResult _Edit(int? applicationId) {
            Application application = new Application();
            if (applicationId.HasValue) application = unitOfWork.ApplicationRepository.Find(applicationId);


            return PartialView(application);
        }

        [HttpPost]
        public ActionResult SubmitEditApplication(Application application) {

            application.Code = application.Label.ToUpperInvariant();
            if (application.Id == 0) unitOfWork.ApplicationRepository.Add(application);
            else unitOfWork.ApplicationRepository.Update(application);

            unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}