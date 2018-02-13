using System.Threading.Tasks;
using System.Web.Mvc;
using Jurassic.AppCenter;
using Jurassic.AppCenter.Logs;
using Jurassic.AppCenter.Models;
using Jurassic.Com.Tools;
using Jurassic.WebFrame;
using Jurassic.WebFrame.Controllers;

namespace Jurassic.So.SooilApp.Controllers
{
    public class WebAppAccountController : AccountController
    {
        //const string Cookie_StartPage = "startPage";
        [JAuth(JAuthType.EveryOne)]
        public override async Task<ActionResult> Login(string returnUrl)
        {
            var toUrl = string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("SearchIndex", "WebApp") : returnUrl;

            ViewBag.ReturnUrl = null;// returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                //将登录页的目标返回页用cookie记录，在index首页加载时自动加载到它的iframe内。
                //  SetStartPageCookie(returnUrl);
                return Redirect(toUrl);
            }
            var model = this.GetLoginModel();

            if (!model.Password.IsEmpty() && model.RememberMe)
            {
                Login(model, returnUrl);
                if (_loginResult == LoginState.OK)
                {
                    //将登录页的目标返回页用cookie记录，在index首页加载时自动加载到它的iframe内。
                    // SetStartPageCookie(returnUrl);
                    return Redirect(toUrl);
                }
            }

            return View(model);
        }

        // POST: /Account/Login
        LoginState _loginResult = LoginState.UnKnown;
        [JAuth(JAuthType.EveryOne)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public override ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return JsonTips();
            }
            _loginResult = AppManager.Instance.Login(model);
            LogInfo.UserName = model.UserName;
            if (_loginResult == LoginState.OK)
            {
                //验证是否强制修改密码_by_zjf
                if (model.IsChangedPassword)
                {
                    return JsonTipsLang("success", null, "UserPasswordChange", new { Url = Url.Action("ChangePassword", "Account", new { isLogin = true }) });
                }

                var user = AppManager.Instance.UserManager.GetByName(model.UserName);
                //在同一台机器上不同用户登录时，通知AppManger设置原用户为离线
                // AppManager.Instance.Logout(User.Identity.Name);
                SetStartPageCookie(returnUrl);
                if (model.RememberMe)
                {
                    SetLoginModel(model);
                }
                else
                {
                    ClearLoginModel();
                }
                var toUrl = string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("SearchIndex", "WebApp") : returnUrl;
                return JsonTipsLang("success", null, "Login_Success", new { Url = toUrl, User = user });//User =  CurrentUser});
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            // ModelState.AddModelError("", "提供的用户名或密码不正确。");
            LogInfo.LogType = JLogType.Warning.ToString();
            return JsonTipsLang("error", null, "LoginState_" + _loginResult);
        }

        private void SetStartPageCookie(string returnUrl)
        {
            if (!returnUrl.IsEmpty() && !returnUrl.Equals(Url.Action("Index", "Home")))
            {
                var function = AppManager.Instance.GetFunctionByLocation(returnUrl);
                WebHelper.SetCookie(Cookie_StartPage, JsonHelper.ToJson(new
                {
                    Id = function == null ? "shortcut_start page" : function.Id,
                    Name = function == null ? null : function.Name,
                    Location = returnUrl
                }));
            }
            else
            {
                WebHelper.RemoveCookie(Cookie_StartPage);
            }
        }
        public override ActionResult Logout(string returnUrl)
        {
            AppManager.Instance.Logout(User.Identity.Name);
            this.ClearLoginModel();
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = Url.Action("Index", "Home");
            //return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = Url.Action("SearchIndex", "WebApp");
            return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
        }
        internal const string Cookie_LoginName = "_Ln_";
        internal const string Cookie_Password = "_Pw_";
        internal const string Cookie_RememberMe = "_Rb_";
        internal const string Cookie_StartPage = "startPage";
        public LoginModel GetLoginModel()
        {
            var model = new LoginModel();
            model.UserName = Encryption.Decrypt(this.GetCookie(Cookie_LoginName));
            model.Password = Encryption.Decrypt(this.GetCookie(Cookie_Password));
            model.RememberMe = CommOp.ToBool(this.GetCookie(Cookie_RememberMe));
            return model;
        }

        public void SetLoginModel(LoginModel model)
        {
            SetCookie(Cookie_RememberMe, "True");
            SetCookie(Cookie_LoginName, Encryption.Encrypt(model.UserName), 30 * 24 * 60);
            SetCookie(Cookie_Password, Encryption.Encrypt(model.Password), 30 * 24 * 60);
        }

        public void ClearLoginModel()
        {
            RemoveCookie(Cookie_RememberMe);
            RemoveCookie(Cookie_LoginName);
            RemoveCookie(Cookie_Password);
        }
    }
}
