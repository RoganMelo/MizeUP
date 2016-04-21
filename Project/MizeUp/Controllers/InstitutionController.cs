using MizeUP.Filters;
using MizeUP.Internacionalization;
using MizeUP.Mappings;
using MizeUP.Models;
using MizeUP.Util;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MizeUP.Controllers
{
    [AuthenticationFilter]
    public class InstitutionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<InstitutionModel> institutions = session.Query<InstitutionModel>().OrderBy(x => x.Name).ToList();
                
                return View(institutions);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstitutionModel institution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(institution);
                            transaction.Commit();
                        }

                        return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                    }
                }
                else
                {
                    return RedirectToAction("Index").WithMessage(Languages.InvalidModelState, "alert-warning", "glyphicon glyphicon-exclamation-sign");
                }
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        [HttpPost]
        public ActionResult Delete(int modelId)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    InstitutionModel institution = session.Get<InstitutionModel>(modelId);

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(institution);
                        transaction.Commit();
                    }

                    return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }
            }
            catch (ADOException)
            {
                return RedirectToAction("Index").WithMessage(Languages.ADOException, "alert-warning", "glyphicon glyphicon-exclamation-sign");
                throw;
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                InstitutionModel institution = session.Get<InstitutionModel>(id);

                return View(institution);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InstitutionModel institution)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        InstitutionModel persistentInstitution = session.Get<InstitutionModel>(institution.Id);

                        persistentInstitution.Name = institution.Name;

                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(persistentInstitution);
                            transaction.Commit();
                        }

                        return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                    }
                }
                else
                {
                    return RedirectToAction("Index").WithMessage(Languages.InvalidModelState, "alert-warning", "glyphicon glyphicon-exclamation-sign");
                }
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }
    }
}