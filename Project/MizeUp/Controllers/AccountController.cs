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
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult SignUp()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<InstitutionModel> Institutions = session.Query<InstitutionModel>().ToList();
                ViewBag.Institutions = new SelectList(Institutions, "Id", "Name");

                IList<LevelModel> Levels = session.Query<LevelModel>().ToList();
                ViewBag.Levels = new SelectList(Levels, "Id", "Name");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(StudentModel model, int Level)
        {
            try
            {
                if (model.Password.Equals(model.ConfirmPassword))
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        model.Level = session.Get<LevelModel>(Level);

                        using (ITransaction transaction = session.BeginTransaction())
                        {
                            session.Save(model);
                            transaction.Commit();

                            Session["LoggedAccount"] = new StudentModel()
                            {
                                Id = model.Id,
                                Name = model.Name,
                                LastName = model.LastName,
                                Email = model.Email,
                                Syllabus = model.Syllabus
                            };
                        }
                    }

                    return RedirectToAction("Index", "Home").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }
                else
                {
                    ModelState.AddModelError("", Languages.InvalidConfirmPassword);

                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        ViewBag.Institutions = session.Query<InstitutionModel>().ToList();
                    }

                    return View();
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
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Home").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(string Email, string Password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<StudentModel> models = session.Query<StudentModel>().ToList();

                foreach (StudentModel model in models)
                {
                    if (model.Email.Equals(Email) && model.Password.Equals(Password))
                    {
                        Session["LoggedAccount"] = new StudentModel()
                        {
                            Id = model.Id,
                            Name = model.Name,
                            LastName = model.LastName,
                            Email = model.Email,
                            Syllabus = model.Syllabus
                        };

                        return RedirectToAction("Index", "Home");
                    }
                }

                return RedirectToAction("Index", "Home").WithMessage(Languages.InvalidEmailOrPassword, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
        }

        [AuthenticationFilter]
        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword(StudentModel editedModel, string ActualPassword)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    StudentModel model = session.Get<StudentModel>(editedModel.Id);

                    if (model.Password.Equals(ActualPassword))
                    {
                        if (editedModel.Password.Equals(editedModel.ConfirmPassword))
                        {
                            model.Password = editedModel.Password;

                            using (ITransaction transaction = session.BeginTransaction())
                            {
                                session.Save(model);
                                transaction.Commit();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", Languages.InvalidConfirmPassword);

                            ViewBag.Institutions = session.Query<InstitutionModel>().ToList();

                            return RedirectToAction("Edit", "Student");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", Languages.InvalidEmailOrPassword);

                        ViewBag.Institutions = session.Query<InstitutionModel>().ToList();

                        return RedirectToAction("Edit", "Student");
                    }
                }

                return RedirectToAction("Index", "Home").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
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
            catch (System.Exception)
            {
                return RedirectToAction("Index", "Home").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
        }
    }
}
