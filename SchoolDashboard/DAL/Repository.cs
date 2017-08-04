﻿using Dapper;
using SchoolDashboard.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                SQLiteConnection.CreateFile(DbFilePath);
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

        public static SchoolLevel[] GetSchoolLevels()
        {
            return GetAllRows<SchoolLevel>("SchoolLevels");
        }

        private static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=" + DbFilePath);
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

        private static void DeleteRow(string tableName, int id)
        {
            Execute(string.Format("delete from {0} where Id = @Id", tableName), new { Id = id });
        }

        private static T GetById<T>(string tableName, int id)
        {
            using (var conn = GetConnection())
            {
                return conn.Query<T>("select * from " + tableName + " where Id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        private static T[] GetAllRows<T>(string tableName)
        {
            return ExecuteToModel<T>("select * from " + tableName);
        }

        private static void RunScript(string filePath, SQLiteConnection connection)
        {
            var script = File.ReadAllText(filePath);
            connection.Execute(script);
        }
    }
}