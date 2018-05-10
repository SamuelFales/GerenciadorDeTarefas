using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Core.Service.Interfaces;
using AutoMapper;
using TaskManager.Data.Model;
using TaskManager.Web.MVC.ViewModel;
using TaskManager.Web.MVC.Service.Interfaces;
using System.Web.Routing;
using TaskManager.Web.MVC.Service;
using System.Globalization;
using System.Web.Security;


namespace TaskManager.Web.MVC.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly ITaskService _taskService;

        public IMembershipService MembershipService { get; set; }

        public UserController(IUserService userService, ITaskService taskService)
        {
            _userService = userService;
            _taskService = taskService;
        }


        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null) { MembershipService = new UserMembershipService(); }

            base.Initialize(requestContext);
        }

        
        public ActionResult LogOn()
        {
            return View();
        }



        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    SetupFormsAuthTicket(model.UserName, model.RememberMe);

               
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "O usuário ou senha estão incorretos.");
            }
            
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Master")]
        public ActionResult Index()
        {
            var userViewModel = Mapper.Map<IEnumerable<TB_User>, IEnumerable<UserViewModel>>(_userService.GetAll());
            return View(userViewModel);
        }

        [Authorize(Roles = "Master")]
        public ActionResult Details(int id)
        {
            var user = _userService.GetById(id);
            var userViewModel = Mapper.Map<TB_User, UserViewModel>(user);

            return View(userViewModel);

        }

        [Authorize(Roles = "Master")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master")]
        public ActionResult Create(UserViewModel user)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var createStatus = MembershipService.CreateUser(user.Name, user.Password, user.Email);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao criar novo usuário. Contate o administrador. ERRO:" + ex.Message );
            }

         return View(user);

        }

        // GET: User/Edit/5
        [Authorize(Roles = "Master")]
        public ActionResult Edit(int id)
        {
            var user = _userService.GetById(id);
            var userViewModel = Mapper.Map<TB_User, UserViewModel>(user);

            return View(userViewModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master")]
        public ActionResult Edit(UserViewModel user)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (_userService.ValidateEmailExists(user.ID, user.Email))
                    {
                        ModelState.AddModelError("", "E-mail já cadastrado para outro usuário.");
                    }
                    else
                    {

                        var editUser = _userService.GetById(user.ID);
                        Mapper.Map(user, editUser);

                        _userService.Update(editUser);
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao alterar cadastro do usuário. ERRO: " + ex.Message);
            }

            return View(user);
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Master")]
        public ActionResult Delete(int id)
        {


            var user = _userService.GetById(id);
            var userViewModel = Mapper.Map<TB_User, UserViewModel>(user);

            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Master")]
        public ActionResult Delete(UserViewModel user)
        {


            IEnumerable<TB_Task> tasks = _taskService.GetTasksByUserID(user.ID);

            if (tasks != null && tasks.Count() > 0)
            {
                var userDelet = _userService.GetById(user.ID);
                user = Mapper.Map<TB_User, UserViewModel>(userDelet);

                ModelState.AddModelError("", "Existem tarefas para esse usuário, não é possível excluir.");
            }
            else
            {

                var deleteUser = _userService.GetById(user.ID);
                Mapper.Map(user, deleteUser);

                _userService.Remove(deleteUser);
                return RedirectToAction("Index");
            }
            return View(user);

        }


        private TB_User SetupFormsAuthTicket(string email, bool persistanceFlag)
        {
            TB_User user;

            user = _userService.GetUserByEmail(email);

             var userId = user.ID;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                                        email, // user name
                                                        DateTime.Now,             //creation
                                                        DateTime.Now.AddMinutes(30), //Expiration
                                                        persistanceFlag, //Persistent
                                                        userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return user;
        }

       
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
           
            switch (createStatus)
            {

                case MembershipCreateStatus.DuplicateEmail:
                    return "O usuário para esse e-mail já existe. Favor utilizar outro e-mail";

                case MembershipCreateStatus.InvalidPassword:
                    return "Senha inválida.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Endereço de e-mail inválido.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Usuário inválido.";

                default:
                    return "Erro inesperado. Favor tente novamente.";
            }
        }
        
    }


}

