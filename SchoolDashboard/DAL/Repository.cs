using Dapper;
using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public static Notice[] GetNotices(int limit)
        {
            var now = DateTime.Now.Date;
            var notices = GetAllRows<Notice>();
            if (notices.Length == 0)
                return notices;

            var expired = notices.Where(n => n.Date.AddDays(n.Duration) < now.Date);
            foreach (var notice in expired)
                DeleteRow<Notice>(notice.Id);

            return ExecuteToModel<Notice>("select * from Notices order by id desc, IsImportant desc limit " + limit);
        }

        internal static void AddNotice(Notice model)
        {
            model.Date = DateTime.Now;
            Execute("insert into Notices (Title, DateBinary, [Text], Duration, IsImportant) values (@Title, @DateBinary, @Text, @Duration, @IsImportant)", model);
        }

        internal static void SaveNotice(Notice model)
        {
            Execute("update Notices set Title = @Title, DateBinary = @DateBinary, [Text] = @Text, Duration = @Duration, IsImportant = @IsImportant where Id = @Id", model);
        }

        public static Notice[] GetNotices()
        {
            return ExecuteToModel<Notice>("select * from Notices order by id desc, IsImportant desc");
        }

        public static void SaveHoliday(Holiday model)
        {
            Execute("update Holidays set [Day] = @Day, [Month] = @Month, Name = @Name, Description = @Description, Picture = @Picture where Id = @Id", model);
        }

        public static void AddHoliday(Holiday model)
        {
            Execute("insert into Holidays ([Day],[Month], Name, Description, Picture) values (@Day, @Month, @Name, @Description, @Picture)", model);
        }

        public static void AddStudent(Student model)
        {
            Execute("insert into Students(Name, BirthdayDay, BirthdayMounth, Class, IsMale) values(@Name, @BirthdayDay, @BirthdayMounth, @Class, @IsMale)", model);
        }

        public static void AddTeacher(Teacher model)
        {
            Execute("insert into Teachers(Name, BirthdayDay, BirthdayMounth, Position, IsMale) values(@Name, @BirthdayDay, @BirthdayMounth, @Position, @IsMale)", model);
        }

        public static FamousBirthday[] GetFamousBirthdaysByDay(int month, int day)
        {
            return ExecuteToModel<FamousBirthday>("select * from FamousBirthdays where Month = @Month and Day = @Day", new { Month = month, Day = day }).ToArray();
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

        public static T ExecuteScalar<T>(string sqlQuery, object parameters)
        {
            using (var conn = GetConnection())
            {
                return conn.ExecuteScalar<T>(sqlQuery, param: parameters);
            }
        }

        public static DataTable ExecuteDataTable(string sqlQuery)
        {
            using (var conn = GetConnection())
            {
                var reader = conn.ExecuteReader(sqlQuery);
                var table = new DataTable();
                table.Load(reader);
                return table;
            }
        }

        public static void DeleteRow<T>(int id)
        {
            Execute(string.Format("delete from {0} where Id = @Id", GetTableName(typeof(T))), new { Id = id });
        }

        public static void DeleteAllRows<T>()
        {
            Execute(string.Format("delete from {0} where Id > 0", GetTableName(typeof(T))), null);
        }

        public static T GetById<T>(int id)
        {
            return ExecuteToModel<T>("select * from " + GetTableName(typeof(T)) + " where Id = @Id", new { Id = id }).FirstOrDefault();
        }

        public static T[] GetAllRows<T>()
        {
            return ExecuteToModel<T>("select * from " + GetTableName(typeof(T)));
        }

        public static int CountAllRws<T>()
        {
            return ExecuteScalar<int>("select count(*) from " + GetTableName(typeof(T)), null);
        }

        private static void RunScript(string filePath, SqliteConnection connection)
        {
            var script = File.ReadAllText(filePath);
            connection.Execute(script);
        }

        private static string GetTableName(Type modelType)
        {
            var attr = modelType.GetCustomAttributes(true).Where(a => a is TableNameAttribute).FirstOrDefault() as TableNameAttribute;
            if (attr == null)
                throw new Exception("Missing TableNameAttribute for model " + modelType.FullName);

            return attr.TableName;
        }
        #endregion
    }
}
