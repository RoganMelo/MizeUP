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
    public class LevelController : Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<LevelModel> models = session.Query<LevelModel>().ToList();

                return View(models);
            }
        }

        [HttpGet]
        [AuthenticationFilter]
        public ActionResult Create()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<InstitutionModel> institutionModels = session.Query<InstitutionModel>().ToList();
                ViewBag.Institutions = new SelectList(institutionModels, "Id", "Name");

                IList<Object> levelTypes = new List<Object>();

                levelTypes.Add(new
                {
                    Id = '1',
                    Name = "Jardim"
                });
                levelTypes.Add(new
                {
                    Id = '2',
                    Name = "Ensino Fundamental I"
                });
                levelTypes.Add(new
                {
                    Id = '3',
                    Name = "Ensino Fundamental II"
                });
                levelTypes.Add(new
                {
                    Id = '4',
                    Name = "Ensino Médio"
                });

                ViewBag.LevelTypes = new SelectList(levelTypes, "Id", "Name");

                return View();
            }
        }

        [HttpPost]
        [AuthenticationFilter]
        public ActionResult Create(LevelModel model, int Institution)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    InstitutionModel institutionModel = session.Get<InstitutionModel>(Institution);

                    model.Institution = institutionModel;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
            }
            catch
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        [HttpPost]
        [AuthenticationFilter]
        public ActionResult Delete(int modelId)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    LevelModel model = session.Get<LevelModel>(modelId);

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(model);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
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
        [AuthenticationFilter]
        public ActionResult Edit(int modelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                LevelModel model = session.Get<LevelModel>(modelId);

                IList<InstitutionModel> institutionModels = session.Query<InstitutionModel>().ToList();
                ViewBag.Institutions = new SelectList(institutionModels, "Id", "Name");

                IList<Object> levelTypes = new List<Object>();

                levelTypes.Add(new
                {
                    Id = '1',
                    Name = "Jardim"
                });
                levelTypes.Add(new
                {
                    Id = '2',
                    Name = "Ensino Fundamental I"
                });
                levelTypes.Add(new
                {
                    Id = '3',
                    Name = "Ensino Fundamental II"
                });
                levelTypes.Add(new
                {
                    Id = '4',
                    Name = "Ensino Médio"
                });

                ViewBag.LevelTypes = new SelectList(levelTypes, "Id", "Name");

                return View(model);
            }
        }

        [HttpPost]
        [AuthenticationFilter]
        public ActionResult Edit(LevelModel model, int Institution)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    LevelModel persistentModel = session.Get<LevelModel>(model.Id);

                    InstitutionModel institutionModel = session.Get<InstitutionModel>(Institution);

                    persistentModel.Institution = institutionModel;
                    persistentModel.Name = model.Name;
                    persistentModel.LevelType = model.LevelType;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(persistentModel);
                        transaction.Commit();
                    }
                }

                return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
            }
            catch(PropertyValueException)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-warning", "glyphicon glyphicon-exclamation-sign");
                throw;
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        public ActionResult ListLevelsByInstitution(int institutionId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<LevelModel> levels = session.Query<LevelModel>().Where(x => x.Institution.Id == institutionId).ToList();
                IList<object> temp = new List<object>();

                foreach (var level in levels)
                {
                    temp.Add(new
                    {
                        Id = level.Id,
                        Name = level.Name
                    });
                }

                return Json(temp, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
