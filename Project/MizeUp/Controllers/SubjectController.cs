using MizeUP.Filters;
using MizeUP.Internacionalization;
using MizeUP.Mappings;
using MizeUP.Models;
using MizeUP.Util;
using Newtonsoft.Json;
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
    public class SubjectController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<SubjectModel> models = session.Query<SubjectModel>().Where(x => x.Student.Id == account.Id).OrderBy(x => x.Name).ToList();

                return View(models);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            IList<Object> status = new List<Object>();

            status.Add(new
            {
                Id = "A",
                Name = Languages.Attending
            });
            status.Add(new
            {
                Id = "S",
                Name = Languages.Suspend
            });
            status.Add(new
            {
                Id = "P",
                Name = Languages.Pendent
            });
            status.Add(new
            {
                Id = "C",
                Name = Languages.Completed
            });

            ViewBag.Status = new SelectList(status, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectModel model, string jsonHorarios)
        {
            try
            {
                IList<ScheduleModel> schedules = JsonConvert.DeserializeObject<List<ScheduleModel>>(jsonHorarios);

                StudentModel account = Session["LoggedAccount"] as StudentModel;

                using (ISession session = NHibernateHelper.OpenSession())
                {
                    StudentModel student = session.Get<StudentModel>(account.Id);

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        model.Student = student;
                        session.Save(model);

                        model = session.Get<SubjectModel>(model.Id);

                        foreach (var schedule in schedules)
                        {
                            schedule.Subject = model;
                            session.Save(schedule);
                        }

                        session.Save(model);

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

        [HttpPost]
        public ActionResult Delete(int modelId)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {

                    SubjectModel subject = session.Get<SubjectModel>(modelId);
                    IList<ScheduleModel> schedules = session.Query<ScheduleModel>().Where(x => x.Subject == subject).ToList();

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Delete(subject);
                        foreach (var schedule in schedules)
                        {
                            session.Delete(schedule);
                        }
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
        public ActionResult Edit(int modelId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<Object> status = new List<Object>();

                status.Add(new
                {
                    Id = "A",
                    Name = Languages.Attending
                });
                status.Add(new
                {
                    Id = "S",
                    Name = Languages.Suspend
                });
                status.Add(new
                {
                    Id = "P",
                    Name = Languages.Pendent
                });
                status.Add(new
                {
                    Id = "C",
                    Name = Languages.Completed
                });

                ViewBag.Status = new SelectList(status, "Id", "Name");


                SubjectModel subject = session.Get<SubjectModel>(modelId);
                IList<ScheduleModel> schedules = session.Query<ScheduleModel>().Where(x => x.Subject == subject).ToList();

                ViewBag.schedules = schedules;
                return View(subject);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectModel subject, string jsonHorarios)
        {
            try
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    IList<ScheduleModel> schedules = JsonConvert.DeserializeObject<List<ScheduleModel>>(jsonHorarios);

                    SubjectModel persistentSubject = session.Get<SubjectModel>(subject.Id);
                    persistentSubject.Name = subject.Name;
                    persistentSubject.Status = subject.Status;

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        session.Save(persistentSubject);
                        foreach (var s in schedules)
                        {
                            ScheduleModel schedule = session.Get<ScheduleModel>(s.Id);
                            if (schedule != null)
                            {
                                schedule.Day = s.Day;
                                schedule.StartTime = s.StartTime;
                                schedule.EndTime = s.EndTime;

                                session.Save(schedule);
                            }
                            else
                            {
                                s.Subject = persistentSubject;
                                session.Save(s);
                            }

                        }

                        transaction.Commit();
                    }

                    return RedirectToAction("Index").WithMessage(Languages.Success, "alert-success", "glyphicon glyphicon-ok-sign");
                }
            }
            catch (System.Exception)
            {
                return RedirectToAction("Index").WithMessage(Languages.Error, "alert-danger", "glyphicon glyphicon-remove-sign");
                throw;
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                SubjectModel subject = session.Get<SubjectModel>(id);

                IList<ScheduleModel> schedules = session.Query<ScheduleModel>().Where(x => x.Subject == subject).ToList();
                List<object> temp = new List<object>();

                foreach (var s in schedules)
                {
                    temp.Add(new
                    {
                        Day = s.Day,
                        StartTime = s.StartTime.ToShortTimeString(),
                        EndTime = s.EndTime.ToShortTimeString()
                    });
                }
                return Json(temp, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult Timesheet()
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<SubjectModel> subjects = session.Query<SubjectModel>().Where(x => x.Student.Id == account.Id).ToList();
                // criar uma classe para receber a posição e o nome da disciplina

                List<String> primeiraColuna = new List<String>();
                string[,] matriz;

                foreach (var subject in subjects)
                {
                    primeiraColuna.Sort();
                    foreach (var horario in subject.Schedules)
                    {
                        bool jaTem = false;
                        foreach (var linha in primeiraColuna)
                        {
                            if (linha == horario.StartTime.ToShortTimeString())
                            {
                                jaTem = true;
                                break;
                            }
                        }
                        if (!jaTem)
                            primeiraColuna.Add(horario.StartTime.ToShortTimeString());
                        // adiciona um item a primeira coluna, a coluna dos horários.
                    }
                }

                // Esta matriz irá guardar a tabela em si
                matriz = new string[primeiraColuna.Count, 7];

                for (int i = 0; i < primeiraColuna.Count; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        matriz[i, j] = "-";
                    }
                }

                foreach (var subject in subjects)
                {
                    // Passa por todas as disciplinas
                    foreach (var horario in subject.Schedules)
                    {
                        // Passa por todos os horários
                        for (int i = 0; i < primeiraColuna.Count; i++)
                        {
                            // se o horário da disciplina for compatível com o horário do FOR atual
                            if (primeiraColuna[i] == horario.StartTime.ToShortTimeString())
                            {
                                // então adicione o nome na matriz na posição i (dia da semana), horário
                                matriz[i, Int16.Parse("" + horario.Day) - 1] = horario.Subject.Name;
                                // é aqui que colocaria o segundo dia...
                            }
                        }
                    }

                    ViewBag.viewBagMatriz = matriz;
                    ViewBag.primeiraColuna = primeiraColuna;
                }
            }
            return View();
        }
    }
}