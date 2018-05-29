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
    }
}
