using System.Web.Mvc;

namespace MiPrimeraSolucion.UI.Controllers
{
    public class AccountController : Controller
    {
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: /Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: /Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // GET: /Account/ForgotPasswordConfirmation
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        public ActionResult ResetPassword()
        {
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/VerifyCode
        public ActionResult VerifyCode()
        {
            return View();
        }

        // GET: /Account/SendCode
        public ActionResult SendCode()
        {
            return View();
        }

        // GET: /Account/ExternalLoginFailure
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        // GET: /Account/Lockout
        public ActionResult Lockout()
        {
            return View();
        }
    }
}