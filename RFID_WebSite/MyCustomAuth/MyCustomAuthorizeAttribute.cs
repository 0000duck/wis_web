using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RFID_WebSite
{
    public class MyCustomAuthorizeAttribute : AuthorizeAttribute 
    {
        public string UserGroup
        {
            get;
            set;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            //string[] users = Users.Split(',');

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            //取得使用者的角色 
            FormsIdentity id = httpContext.User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = id.Ticket;
            string[] currentRoles = ticket.UserData.Split(' ');
            //string roles = this.GetRolesByUserGroup(UserGroup);//取得程式允許的角色 

            foreach (string role in currentRoles)  //比對身分
            {
                if (UserGroup.IndexOf(role) > -1)
                    return true;
            }
            return false;
        }

        /// <summary> 
        /// 依照程式編號取得授權角色 
        /// </summary> 
        /// <returns></returns> 
        string GetRolesByUserGroup(string group)
        {
            
            
            //testing code 不重要 
            //TODO:DB 邏輯  
            return "";
        }
    }
}