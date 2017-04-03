using System.Web.Mvc;
using UserWebApi.Models;

namespace UserWebApi.Controllers
{
    public class UserViewController : Controller
    {
        public ActionResult List()
        {
            UserController uc = new Controllers.UserController();
            return View(uc.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModels user)
        {
            UserController uc = new Controllers.UserController();
            uc.Create(user);
            return View("List", uc.GetAll());
        }

        public ActionResult Edit(int id)
        {
            UserController uc = new Controllers.UserController();
            return View(uc.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModels user)
        {
            UserController uc = new Controllers.UserController();
            if (ModelState.IsValid)
            {
                uc.Update(user);
            }
            return View("List", uc.GetAll());
        }
        public ActionResult Delete(int id)
        {
            UserController uc = new Controllers.UserController();
            uc.Remove(id);
            return View("List", uc.GetAll());
        }
    }
}
