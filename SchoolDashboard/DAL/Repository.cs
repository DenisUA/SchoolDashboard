using Dapper;
using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if MONO
using Mono.Data.Sqlite;
#else
using SqliteConnection = System.Data.SQLite.SQLiteConnection;
#endif

namespace SchoolDashboard.DAL
{
    static class Repository
    {
        private static readonly string DbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "db.sqlite");

        static Repository()
        {
            InitDb();
        }

        private static void InitDb()
        {
            if (!File.Exists(DbFilePath))
            {
                SqliteConnection.CreateFile(DbFilePath);
                using (var conn = GetConnection())
                {
                    RunScript(Path.Combine(Directory.GetCurrentDirectory(), "DAL", "Scripts", "CreateDb.sql"), conn);
                    RunScript(Path.Combine(Directory.GetCurrentDirectory(), "DAL", "Scripts", "InitDb.sql"), conn);
                }
            }
        }

        public static Lesson[] GetLessons(int schoolLevel)
        {
            return ExecuteToModel<Lesson>("SELECT * FROM Lessons WHERE SchoolLevel = @Level", new { Level = schoolLevel });
        }

        public static void AddCalendarEvent(CalendarEvent model)
        {
            Execute("INSERT INTO CalendarEvents (TimeBinary, HasTime, Description, Place) VALUES (@TimeBinary, @HasTime, @Description, @Place)", model);
        }

        public static SchoolLevel[] GetSchoolLevels()
        {
            return GetAllRows<SchoolLevel>();
        }

        public static Awards[] GetAwards(int count)
        {
            return ExecuteToModel<Awards>("SELECT * FROM Awards ORDER BY Id DESC LIMIT " + count);
        }

        public static Awards[] GetAwards()
        {
            return ExecuteToModel<Awards>("SELECT * FROM Awards ORDER BY Id DESC");
        }

        internal static void SaveCalendarEvent(CalendarEvent calendarEvent)
        {
            Execute("update CalendarEvents set TimeBinary = @TimeBinary, HasTime = @HasTime, Description = @Description, Place = @Place where Id = @Id", calendarEvent);
        }

        public static CalendarEvent[] GetCalendarEvents(int count)
        {
            var today = DateTime.Now.Date.ToBinary();
            return ExecuteToModel<CalendarEvent>(string.Format("SELECT * FROM CalendarEvents WHERE TimeBinary > {0} ORDER BY TimeBinary ASC LIMIT {1}", today, count));
        }
        public static CalendarEvent[] GetCalendarEvents()
        {
            var today = DateTime.Now.Date.ToBinary();
            
            return ExecuteToModel<CalendarEvent>(string.Format("SELECT * FROM CalendarEvents WHERE TimeBinary > {0} ORDER BY TimeBinary ASC", today));
        }

        internal static void SaveAward(Awards award)
        {
            Execute("update Awards set [Type] = @Type, Owner = @Owner, Description = @Description where Id = @Id", award);
        }

        public static string GetRandomFact()
        {
            var facts = GetAllRows<Fact>();
            var random = new Random();
            var randomIndex = random.Next(0, facts.Length);
            return facts[randomIndex].FactText;
        }

        internal static void AddAward(Awards model)
        {
            Execute("insert into Awards ([Type], Owner, Description) values (@Type, @Owner, @Description)", model);
        }

        public static Holiday GetHoliday(int month, int day)
        {
            return ExecuteToModel<Holiday>("SELECT * FROM Holidays WHERE [Month] = @Month AND [Day] = @Day", new { Month = month, Day = day }).FirstOrDefault();
        }

        public static void AddFamousBirthday(FamousBirthday model)
        {
            Execute("INSERT INTO FamousBirthdays ([Day], [Month], Name, Description, Photo) VALUES (@Day, @Month, @Name, @Description, @Photo)", model);
        }

        public static Notice[] GetAllNotices()
        {
            var now = DateTime.Now.Date;
            var notices = GetAllRows<Notice>();
            if (notices.Length == 0)
                return notices;

            var expired = notices.Where(n => n.Date.AddDays(n.Duration) < now);
            foreach (var notice in expired)
                DeleteRow<Notice>(notice.Id);

            return GetAllRows<Notice>();
        }

        #region Helpers
        private static SqliteConnection GetConnection()
        {
            return new SqliteConnection("Data Source=" + DbFilePath);
        }

        private static T[] ExecuteToModel<T>(string sqlQuery, object parameters = null)
        {
            using (var conn = GetConnection())
            {
                var res = conn.Query<T>(sqlQuery, parameters);
                return res.ToArray();
            }
        }

        private static void Execute(string sqlQuery, object parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Execute(sqlQuery, parameters);
            }
        }

        public static void DeleteRow<T>(int id)
        {
            var attr = typeof(T).GetCustomAttributes(true).Where(a => a is TableNameAttribute).FirstOrDefault() as TableNameAttribute;
            if (attr == null)
                throw new Exception("Missing TableNameAttribute for model " + typeof(T).FullName);

            Execute(string.Format("delete from {0} where Id = @Id", attr.TableName), new { Id = id });
        }

        public static T GetById<T>(int id)
        {
            var attr = typeof(T).GetCustomAttributes(true).Where(a => a is TableNameAttribute).FirstOrDefault() as TableNameAttribute;
            if (attr == null)
                throw new Exception("Missing TableNameAttribute for model " + typeof(T).FullName);

            return ExecuteToModel<T>("select * from " + attr.TableName + " where Id = @Id", new { Id = id }).FirstOrDefault();
        }

        private static T[] GetAllRows<T>()
        {
            var attr = typeof(T).GetCustomAttributes(true).Where(a => a is TableNameAttribute).FirstOrDefault() as TableNameAttribute;
            if (attr == null)
                throw new Exception("Missing TableNameAttribute for model " + typeof(T).FullName);

            return ExecuteToModel<T>("select * from " + attr.TableName);
        }

        private static void RunScript(string filePath, SqliteConnection connection)
        {
            var script = File.ReadAllText(filePath);
            connection.Execute(script);
        } 
        #endregion
    }
}
