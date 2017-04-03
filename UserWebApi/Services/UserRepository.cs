using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using UserWebApi.Models;

namespace UserWebApi.Services
{
    public class UserRepository
    {
        private const string CacheKey = "UserStore";
        public UserRepository()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                if (context.Cache[CacheKey] == null)
                {
                    var users = new UserModels[]
                    {
                        new UserModels
                        {
                            Id=1,
                            Name="Salvador Mínguez",
                            Birthdate=new DateTime(1983,4,22)
                        },
                        new UserModels
                        {
                            Id=2,
                            Name="María Bogarín",
                            Birthdate =new DateTime(1979,1,27)
                        }
                    };
                    context.Cache[CacheKey] = users;
                }
            }
        }

        internal UserModels Get(int id)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                return ((UserModels[])context.Cache[CacheKey]).Where(u => u.Id == id).FirstOrDefault();
            }
            return new Models.UserModels
            {
                Id = 0,
                Name = "Default",
                Birthdate = DateTime.MinValue
            };
        }

        public UserModels[] GetAll()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                return (UserModels[])context.Cache[CacheKey];
            }
            return new Models.UserModels[]
            {
                new Models.UserModels
                {
                    Id=0,
                    Name="Default",
                    Birthdate = DateTime.MinValue
                }
            };
        }

        public UserModels SaveUser(UserModels user)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                try
                {
                    var currentData = ((UserModels[])context.Cache[CacheKey]).ToList();
                    if (user.Id != 0 && currentData.Any(u => u.Id == user.Id))
                    {
                        var currentUser = currentData.Where(u => u.Id == user.Id).FirstOrDefault();
                        if (currentUser != null)
                        {
                            currentUser.Name = user.Name;
                            currentUser.Birthdate = user.Birthdate;
                        }
                    }
                    else
                    {
                        if (user.Id == 0)
                            user.Id = currentData.Select(u => u.Id).Max() + 1;
                        currentData.Add(user);
                    }
                    context.Cache[CacheKey] = currentData.ToArray();
                }
                catch (Exception ex)
                {
                    return user;
                }
            }
            return Get(user.Id);
        }

        public bool DeleteUser(int id)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                try
                {
                    var currentData = ((UserModels[])context.Cache[CacheKey]).ToList();
                    var currentUser = currentData.Where(u => u.Id == id).FirstOrDefault();
                    if (currentUser != null)
                        currentData.Remove(currentUser);
                    context.Cache[CacheKey] = currentData.ToArray();
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
            return true;
        }
    }
}