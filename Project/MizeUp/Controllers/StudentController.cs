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
    public class StudentController : Controller
    {
        [HttpGet]
        public ActionResult Edit(int modelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                StudentModel model = session.Get<StudentModel>(modelId);

                IList<InstitutionModel> Institutions = session.Query<InstitutionModel>().OrderBy(x => x.Name).ToList();
                ViewBag.Institutions = new SelectList(Institutions, "Id", "Name", model.Level.Institution.Id);

                IList<LevelModel> Levels = session.Query<LevelModel>().Where(x => x.Institution.Id == model.Level.Institution.Id).OrderBy(x => x.Name).ToList();
                ViewBag.Levels = new SelectList(Levels, "Id", "Name");

                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Edit(StudentModel editedModel, int Level)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    StudentModel model = session.Get<StudentModel>(editedModel.Id);

                    model.Level = session.Get<LevelModel>(Level);
                    model.Email = editedModel.Email;
                    model.Name = editedModel.Name;
                    model.LastName = editedModel.LastName;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();

                        Session["LoggedAccount"] = new StudentModel()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            LastName = model.LastName,
                            Email = model.Email
                        };
                    }

                    return RedirectToAction("Index", "Home").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }
            }
            catch (PropertyValueException)
            {
                ModelState.AddModelError("", Languages.InvalidModelState);

                using (ISession session = NHibernateHelper.OpenSession())
                {
                    ViewBag.Institutions = session.Query<InstitutionModel>().ToList();
                }

                return View();
            }
            catch(System.Exception)
            {
                return RedirectToAction("Index", "Home").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
        }
    }
}