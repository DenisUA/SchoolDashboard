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
using System.Data.SQLite;

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
            var picturesDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Web", "Content", "Images", "FamousBirthdays"));
            var newPicturesDirectory = new DirectoryInfo(Path.Combine(picturesDirectory.FullName, "NewImages"));
            if (newPicturesDirectory.Exists)
                newPicturesDirectory.Delete(true);

            newPicturesDirectory.Create();

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
                        var birthday = new DAL.Models.FamousBirthday();

                        birthday.Month = Convert.ToInt32(items[0]);
                        birthday.Day = Convert.ToInt32(items[1]);
                        birthday.Name = items[2].ToString();
                        birthday.Description = items[3].ToString();

                        var fileUrl = items[4].ToString();
                        var newFileName = Guid.NewGuid() + Path.GetExtension(fileUrl);
                        var newFilePath = Path.Combine(newPicturesDirectory.FullName, newFileName);

                        webClient.DownloadFile(fileUrl, newFilePath);

                        birthday.Photo = newFileName;

                        birthdays.Add(birthday);
                    }
                }
            }
            catch (Exception ex)
            {
                return Response.AsText(ex.ToString());
            }

            Parallel.ForEach(picturesDirectory.EnumerateFiles(), (f) => f.Delete());
            Parallel.ForEach(newPicturesDirectory.EnumerateFiles(), (f) => f.MoveTo(Path.Combine(picturesDirectory.FullName, f.Name)));

            Repository.DeleteAllRows<DAL.Models.FamousBirthday>();
            foreach (var birthday in birthdays)
            {
                Repository.AddFamousBirthday(birthday);
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
                        var student = new DAL.Models.Student();
                        student.Class = items[3].ToString();
                        student.IsMale = items[4].ToString().StartsWith("чол");


                        var name = items[1].ToString();
                        student.Name = name.Remove(name.LastIndexOf(' '));


                        if (items[2] is DateTime)
                        {
                            var dateTime = (DateTime)items[2];
                            student.BirthdayDay = dateTime.Day;
                            student.BirthdayMonth = dateTime.Month;
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

            Repository.DeleteAllRows<DAL.Models.Student>();
            foreach (var student in students)
            {
                Repository.AddStudent(student);
            }

            return Response.AsText("ok");
        }

        public dynamic UploadHolidaysFile()
        {
            var file = Request.Files.FirstOrDefault();

            var holidays = new List<DAL.Models.Holiday>();
            var picturesDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "Web", "Content", "Images", "HolidaysPictures"));
            var newPicturesDirectory = new DirectoryInfo(Path.Combine(picturesDirectory.FullName, "NewImages"));
            if (newPicturesDirectory.Exists)
                newPicturesDirectory.Delete(true);

            newPicturesDirectory.Create();

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
                        var holiday = new DAL.Models.Holiday();

                        holiday.Day = Convert.ToInt32(items[0]);
                        holiday.Month = Convert.ToInt32(items[1]);
                        holiday.Name = items[2].ToString();
                        holiday.Description = items[3].ToString();

                        var fileUrl = items[4].ToString();

                        var newFileName = Guid.NewGuid() + Path.GetExtension(fileUrl);
                        var argsIndex = newFileName.IndexOf('?');
                        if (argsIndex > 0)
                        {
                            newFileName = newFileName.Remove(argsIndex);
                        }

                        var newFilePath = Path.Combine(newPicturesDirectory.FullName, newFileName);

                        webClient.DownloadFile(fileUrl, newFilePath);

                        holiday.Picture = newFileName;

                        holidays.Add(holiday);
                    }
                }
            }
            catch (Exception ex)
            {
                return Response.AsText(ex.ToString());
            }

            Parallel.ForEach(picturesDirectory.EnumerateFiles(), (f) => f.Delete());
            Parallel.ForEach(newPicturesDirectory.EnumerateFiles(), (f) => f.MoveTo(Path.Combine(picturesDirectory.FullName, f.Name)));

            Repository.DeleteAllRows<DAL.Models.Holiday>();
            foreach (var holiday in holidays)
            {
                Repository.AddHoliday(holiday);
            }

            return Response.AsText("ok");
        }

        public dynamic Teachers()
        {
            return GetView();
        }

        public dynamic UploadTeachersFile()
        {
            var file = Request.Files.FirstOrDefault();

            var teachers = new List<DAL.Models.Teacher>();

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
                        var teacher = new DAL.Models.Teacher();
                        teacher.Position = items[2].ToString();
                        teacher.IsMale = items[4].ToString().StartsWith("чол");

                        var nameElements = items[1].ToString().Split(' ');
                        teacher.Name = nameElements[0] + " " + nameElements[1].ToUpper()[0] + ". " +nameElements[2].ToUpper()[0] + ".";

                        if (items[3] is DateTime)
                        {
                            var dateTime = (DateTime)items[3];
                            teacher.BirthdayDay = dateTime.Day;
                            teacher.BirthdayMonth = dateTime.Month;
                        }
                        else
                        {
                            throw new Exception("bad date" + items[1].ToString());
                        }

                        teachers.Add(teacher);
                    }
                }
            }
            catch (Exception ex)
            {
                return Response.AsText(ex.Message);
            }

            Repository.DeleteAllRows<DAL.Models.Teacher>();
            foreach (var teacher in teachers)
            {
                Repository.AddTeacher(teacher);
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
                DataTable table;
                try
                {
                    table = Repository.ExecuteDataTable(model.Request);
                }
                catch (SQLiteException ex)
                {
                    model.Error = ex.Message;
                    return model;
                }
                catch (Exception ex)
                {
                    model.Error = ex.ToString();
                    return model;
                }

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

            model.Error = null;

            return GetView(model);
        }
    }
}
