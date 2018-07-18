using Nancy;
using Nancy.ModelBinding;
using SchoolDashboard.Web.RequestsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.DAL;
using SchoolDashboard.Web.Models;
using System.IO;
using ExcelDataReader;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Net;
using System.Reflection.Emit;

namespace SchoolDashboard.Web
{
    public class ControlPanelWebModule : WebModule
    {
        internal override string PathPrefix
        {
            get
            {
                return "ControlPanel";
            }
        }

        public dynamic CalendarEvents()
        {
            var events = Repository.GetCalendarEvents().Select(e => new CalendarEvent(e)).ToArray();
            var model = new CalendarEvents() { Events = events };
            return GetView(model);
        }

        public dynamic EditCalendarEvent(dynamic parameters)
        {
            if (parameters.Id == null)
            {
                var model = new CalendarEvent();
                return GetView(model);
            }
            else
            {
                var ev = Repository.GetById<DAL.Models.CalendarEvent>(parameters.Id);
                var model = new CalendarEvent(ev);

                return GetView(model);
            }
        }

        public dynamic SaveCalendarEvent()
        {
            var model = this.Bind<CalendarEvent>();

            var t = DateTime.Parse(model.DateTimeString);
            t = DateTime.SpecifyKind(t, DateTimeKind.Local);
            model.TimeBinary = t.ToBinary();

            if (model.Id == 0)
            {
                Repository.AddCalendarEvent(model as DAL.Models.CalendarEvent);
            }
            else
            {
                Repository.SaveCalendarEvent(model as DAL.Models.CalendarEvent);
            }

            return Redirect(CalendarEvents);
        }

        public dynamic DeleteCalendarEvent(dynamic parameters)
        {
            Repository.DeleteRow<DAL.Models.CalendarEvent>(parameters.Id);

            return Redirect(CalendarEvents);
        }

        public dynamic Awards()
        {
            var awards = Repository.GetAwards();
            var model = new Awards()
            {
                ActiveAwards = awards.Take(10).Select(a => new Award(a)).ToArray(),
                UnactiveAwards = awards.Skip(10).Select(a => new Award(a)).ToArray()
            };

            return GetView(model);
        }

        public dynamic EditAward(dynamic parameters)
        {
            Award model;

            if (parameters.Id == null)
            {
                model = new Award();
            }
            else
            {
                var award = Repository.GetById<Award>(parameters.Id);
                model = new Award(award);
            }

            return GetView(model);
        }

        public dynamic SaveAward()
        {
            var model = this.Bind<Award>();
            if (model.Id == 0)
            {
                Repository.AddAward(model);
            }
            else
            {
                Repository.SaveAward(model);
            }
            return Redirect(Awards);
        }

        public dynamic DeleteAward(dynamic parameters)
        {
            Repository.DeleteRow<Award>(parameters.Id);
            return Redirect(Awards);
        }

        public dynamic Notices()
        {
            var notices = Repository.GetNotices();
            var model = new Notices() { AllNotices = notices.Select(n => new Notice(n)).ToArray() };

            return GetView(model);
        }

        public dynamic EditNotice(dynamic parameters)
        {
            Notice model;
            if (parameters.Id == null)
            {
                model = new Notice();
                model.Duration = 1;
            }
            else
            {
                model = Repository.GetById<Notice>(parameters.Id);
            }

            return GetView(model);
        }

        public dynamic SaveNotice()
        {
            var model = this.Bind<Notice>();
            if (model.Id == 0)
            {
                Repository.AddNotice(model);
            }
            else
            {
                Repository.SaveNotice(model);
            }

            return Redirect(Notices);
        }

        public dynamic DeleteNotice(dynamic parameters)
        {
            Repository.DeleteRow<Notice>(parameters.Id);
            return Redirect(Notices);
        }

        public dynamic Holidays()
        {
            var model = Repository.GetAllRows<Holiday>().OrderBy(h => h.Date).ToArray();
            return GetView(model);
        }

        public dynamic EditHoliday(dynamic parameters)
        {
            Holiday model;
            if (parameters.Id == null)
            {
                model = new Holiday();
                model.Day = 1;
                model.Month = 1;
            }
            else
            {
                model = Repository.GetById<Holiday>(parameters.Id);
            }

            return GetView(model);
        }

        public dynamic SaveHoliday()
        {
            var model = this.Bind<Holiday>();
            if (model.Id == 0)
            {
                Repository.AddHoliday(model);
            }
            else
            {
                Repository.SaveHoliday(model);
            }

            return Redirect(Holidays);
        }

        public dynamic DeleteHoliday(dynamic paramters)
        {
            Repository.DeleteRow<Holiday>(paramters.Id);
            return Redirect(Holidays);
        }

        public dynamic Students()
        {
            var students = Repository.GetAllRows<DAL.Models.Student>();
            var model = new Students()
            {
                StudentsCount = students.Length
            };

            return GetView(model);
        }

        public dynamic FamousBirthdays()
        {
            return GetView();
        }

        public dynamic UplodaFamousBirthdaysFile()
        {
            var file = Request.Files.FirstOrDefault();

            var birthdays = new List<DAL.Models.FamousBirthday>();
            var picturesDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Web", "Content", "Images", "FamousBirthdays", "NewImages"));
            if (picturesDirectory.Exists)
                picturesDirectory.Delete(true);

            picturesDirectory.Create();

            var webClient = new WebClient();

            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(file.Value))
                {
                    var dataSet = reader.AsDataSet();
                    var table = dataSet.Tables[0];
                    var rows = table.Rows;

                    for (int i = 0; i < rows.Count; i++)
                    {
                        var items = rows[i].ItemArray;
                        var birthday = new DAL.Models.FamousBirthday()
                        {
                            Day = (int)items[0],
                            Month = (int)items[1],
                            Name = items[2].ToString(),
                            Description = items[3].ToString(),
                        };

                        webClient.DownloadFile(items[4].ToString(), picturesDirectory.FullName);

                        birthdays.Add(birthday);
                    }
                }
            }
            catch (Exception ex)
            {
                return Response.AsText(ex.Message);
            }



            return Response.AsText("ok");
        }

        public dynamic UploadStudentsFile()
        {
            var file = Request.Files.FirstOrDefault();

            var students = new List<DAL.Models.Student>();

            try
            {
                using (var reader = ExcelReaderFactory.CreateReader(file.Value))
                {
                    var dataSet = reader.AsDataSet();
                    var table = dataSet.Tables[0];
                    var rows = table.Rows;

                    for (int i = 0; i < rows.Count; i++)
                    {
                        var items = rows[i].ItemArray;
                        var student = new DAL.Models.Student()
                        {
                            Name = items[1].ToString(),
                            Class = items[3].ToString(),
                            IsMale = items[4].ToString().StartsWith("чол")
                        };


                        if (items[2] is DateTime)
                        {
                            var dateTime = (DateTime)items[2];
                            student.BirthdayDay = dateTime.Day;
                            student.BirthdayMounth = dateTime.Month;
                        }
                        else
                        {
                            throw new Exception("bad date" + items[1].ToString());
                        }

                        students.Add(student);
                    }
                }
            }
            catch (Exception ex)
            {
                return Response.AsText(ex.Message);
            }

            Repository.DeleteAllStudents();
            foreach (var student in students)
            {
                Repository.AddStudent(student);
            }

            return Response.AsText("ok");
        }

        public dynamic ExecuteSql()
        {
            var model = this.Bind<ExecuteSql>();
            if (model.Request == null)
            {
                model.Request = "";
            }
            else
            {
                var table = Repository.ExecuteDataTable(model.Request);
                var rows = new List<string[]>();

                model.Headers = table.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToArray();

                foreach (DataRow row in table.Rows)
                {
                    var items = new List<string>();
                    foreach (var i in row.ItemArray)
                    {
                        items.Add(i.ToString());
                    }

                    rows.Add(items.ToArray());
                }

                model.Data = rows.ToArray();
            }

            return GetView(model);
        }
    }
}
