using API.ViewModels;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Operations
{
    public class Account
    {
       public static bool ValidateUser(UserLoginViewModel user)
       {
           SmartLibraryContext db = new SmartLibraryContext();
           bool validation = false;
           UserLogin userLogin = db.UserLogins.Where(x => x.UserName == user.UserName && x.Password == user.Password).SingleOrDefault();
           
           if(userLogin != null)
           {
               validation = true;
           }

           return validation;
       }
    }
}