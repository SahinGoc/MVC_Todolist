using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Todolist.Models.Entity;

namespace Todolist.code
{
    public class SessionUtil
    {
        public User_table UserSession
        {
            get
            {
                return HttpContext.Current.Session["UserSession"] as User_table;
            }

            set
            {
                HttpContext.Current.Session["UserSession"] = value;
            }
        }

        public static SessionUtil CreateInstance
        {
            get
            {
                return new SessionUtil();
            }
        }
    }
}