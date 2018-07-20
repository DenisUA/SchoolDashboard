using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolDashboard.Controllers.Tiles.ViewDataModels;
using SchoolDashboard.DAL;

namespace SchoolDashboard.Controllers.Tiles
{
    class BirthdaysTile : Tile
    {
        public override bool IsActive
        {
            get
            {
                var today = DateTime.Now;
                var studentsBirthdays = Repository.GetStudentsBirthdays(today.Day, today.Month);
                var teachersBirthdays = Repository.GetTeachersBirthdays(today.Day, today.Month);
                return studentsBirthdays.Length + teachersBirthdays.Length > 0;
            }
        }

        public override string TileId { get { return "birthdaysPanel"; } }

        public override DataModel GetViewData()
        {
            var today = DateTime.Now;
            var studentsBirthdays = Repository.GetStudentsBirthdays(today.Day, today.Month);
            var teachersBirthdays = Repository.GetTeachersBirthdays(today.Day, today.Month);

            var birthdays = new List<BirthdayItem>();
            birthdays.AddRange(studentsBirthdays.Select(s => new BirthdayItem() { Name = s.Name, Description = "учень " + s.Class, IsMale = s.IsMale }));
            birthdays.AddRange(teachersBirthdays.Select(t => new BirthdayItem() { Name = t.Name, Description = t.Position, IsMale = t.IsMale }));

            var model = new BirthdayTileData();
            model.Items = birthdays.ToArray();

            return model;
        }

        public override bool IsPriority { get { return true; } }
    }
}