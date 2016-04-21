using MizeUP.Filters;
using MizeUP.Internacionalization;
using MizeUP.Mappings;
using MizeUP.Models;
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
    public class SyllabusController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            StudentModel account = Session["LoggedAccount"] as StudentModel;

            using (ISession session = NHibernateHelper.OpenSession())
            {
                SyllabusModel syllabusModel = new SyllabusModel();

                IList<SyllabusModel> syllabusList = session.Query<SyllabusModel>().ToList();

                foreach (var syllabus in syllabusList)
                {
                    if (syllabus.Student.Id == account.Id)
                    {
                        syllabusModel = syllabus;
                    }
                }

                IList<SubjectModel> subjects = session.Query<SubjectModel>().ToList();
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


                IList<Object> objetives = new List<Object>();

                objetives.Add(new
                {
                    Id = '1',
                    Name = "ENEM"
                });
                objetives.Add(new
                {
                    Id = '2',
                    Name = "Prova"
                });
                objetives.Add(new
                {
                    Id = '3',
                    Name = "Concurso"
                });

                ViewBag.Objectives = new SelectList(objetives, "Id", "Name");

                return View(syllabusModel);
            }
        }

        [HttpPost]
        public ActionResult Index(SyllabusModel model)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                IList<SubjectModel> subjects = session.Query<SubjectModel>().ToList();
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

                IList<Object> objetives = new List<Object>();

                objetives.Add(new
                {
                    Id = '1',
                    Name = "ENEM"
                });
                objetives.Add(new
                {
                    Id = '2',
                    Name = "Prova"
                });
                objetives.Add(new
                {
                    Id = '3',
                    Name = "Concurso"
                });

                ViewBag.Objectives = new SelectList(objetives, "Id", "Name");

                StudentModel account = Session["LoggedAccount"] as StudentModel;

                StudentModel student = session.Get<StudentModel>(account.Id);

                model.Student = student;

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(model);

                    SyllabusModel syllabus = session.Get<SyllabusModel>(model.Id);

                    student.Syllabus = syllabus;

                    session.Save(student);

                    Session["LoggedAccount"] = new StudentModel()
                    {
                        Id = student.Id,
                        Name = student.Name,
                        LastName = student.LastName,
                        Email = student.Email,
                        Syllabus = student.Syllabus
                    };

                    transaction.Commit();
                }
            }

            return RedirectToAction("Index", "Syllabus");
        }
    }
}