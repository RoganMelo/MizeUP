using MizeUP.Util;
using MizeUP.Models;
using MizeUP.Mappings;
using MizeUP.Internacionalization;
using MizeUP.Filters;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using NHibernate.Linq;
using NHibernate;

namespace MizeUP.Controllers
{
    [AuthenticationFilter]
    public class TaskController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<TaskModel> models = session.Query<TaskModel>().Where(x => x.Subject.Student.Id == account.Id).OrderBy(x => x.Name).ToList();

                return View(models);
            }
        }

        [HttpPost]
        public ActionResult InsertGrade(double Grade, int idTask)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    TaskModel task = session.Get<TaskModel>(idTask);
                    task.Grade = Grade;
                    task.Status = '3';

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(task);
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<SubjectModel> subjectModels = session.Query<SubjectModel>().Where(x => x.Student.Id == account.Id && x.Status == 'A').OrderBy(x => x.Name).ToList();
                ViewBag.Subjects = new SelectList(subjectModels, "Id", "Name");
            }

            IList<Object> stage = new List<Object>();

            stage.Add(new
            {
                Id = '1',
                Name = "AV1"
            });
            stage.Add(new
            {
                Id = '2',
                Name = "AV2"
            });
            stage.Add(new
            {
                Id = '3',
                Name = "AV3"
            });

            ViewBag.Stage = new SelectList(stage, "Id", "Name");

            IList<Object> status = new List<Object>();

            status.Add(new
            {
                Id = '1',
                Name = Languages.ToDo
            });
            status.Add(new
            {
                Id = '2',
                Name = Languages.Doing
            });

            ViewBag.Status = new SelectList(status, "Id", "Name");

            return View();
        }

        [HttpPost]
        public ActionResult Create(TaskModel model, int Subject)
        {
            try
            {

                using (ISession session = NHibernateHelper.OpenSession())
                {
                    SubjectModel subjectModel = session.Get<SubjectModel>(Subject);

                    model.Subject = subjectModel;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(model);
                        transaction.Commit();
                    }
                    return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }

            }
            catch (ADOException)
            {
                ModelState.AddModelError("", Languages.InvalidModelState);
                return View();
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public ActionResult Delete(int modelId)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    TaskModel task = session.Get<TaskModel>(modelId);
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(task);
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
        public ActionResult Edit(int taskId)
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                TaskModel task = session.Get<TaskModel>(taskId);

                IList<SubjectModel> subject = session.Query<SubjectModel>().Where(x => x.Student.Id == account.Id && x.Status == 'A').ToList();
                ViewBag.subject = new SelectList(subject, "Id", "Name");

                IList<Object> stage = new List<Object>();

                stage.Add(new
                {
                    Id = '1',
                    Name = "AV1"
                });
                stage.Add(new
                {
                    Id = '2',
                    Name = "AV2"
                });
                stage.Add(new
                {
                    Id = '3',
                    Name = "AV3"
                });

                ViewBag.Stage = new SelectList(stage, "Id", "Name");

                IList<Object> status = new List<Object>();

                status.Add(new
                {
                    Id = '1',
                    Name = Languages.ToDo
                });
                status.Add(new
                {
                    Id = '2',
                    Name = Languages.Doing
                });

                ViewBag.Status = new SelectList(status, "Id", "Name");

                return View(task);
            }
        }
        [HttpPost]
        public ActionResult Edit(TaskModel Task, int Subject)
        {
            try
            {

                using (ISession session = NHibernateHelper.OpenSession())
                {
                    SubjectModel subject = session.Get<SubjectModel>(Subject);
                    TaskModel persistentModel = session.Get<TaskModel>(Task.Id);

                    persistentModel.Name = Task.Name;
                    persistentModel.Subject = subject;
                    persistentModel.Status = Task.Status;
                    persistentModel.Stage = Task.Stage;
                    persistentModel.DeadLine = Task.DeadLine;
                    persistentModel.Description = Task.Description;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(persistentModel);
                        transaction.Commit();
                    }
                    return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }
            }
            catch (ADOException)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
            }
        }


        [HttpGet]
        public ActionResult ListTaskModal(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<TaskModel> listTask = session.Query<TaskModel>().Where(x => x.Subject.Id == id).ToList();
                List<object> temp = new List<object>();
                foreach (var task in listTask)
                {
                    temp.Add(new
                    {
                        Id = task.Id,
                        Name = task.Name,
                        DeadLine = task.DeadLine.ToShortDateString(),
                        Status = task.Status,
                        Grade = task.Grade
                    });
                }
                return Json(temp, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
