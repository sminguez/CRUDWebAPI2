using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserWebApi.Models;
using UserWebApi.Services;

namespace UserWebApi.Controllers
{
    public class UserController : ApiController
    {
        private UserRepository userRepository;

        public UserController()
        {
            this.userRepository = new Services.UserRepository();
        }

        public UserModels[] GetAll()
        {
            return this.userRepository.GetAll();
        }
        [AcceptVerbs("GET","HEAD")]
        public UserModels Get(int id)
        {
            return this.userRepository.Get(id);
        }

        public UserModels Create([FromBody]UserModels user)
        {
            try
            {
                user = this.userRepository.SaveUser(user);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return user;
        }

        public UserModels Update([FromBody]UserModels user)
        {
            try
            {
                user = this.userRepository.SaveUser(user);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }
        [AcceptVerbs("GET")]
        public void Remove(int id)
        {
            var serverData = this.userRepository.Get(id);
            if (serverData == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            this.userRepository.DeleteUser(id);
        }
    }
}
