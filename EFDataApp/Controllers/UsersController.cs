using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CustomIdentityApp.Models;
using CustomIdentityApp.ViewModels;
using EFDataApp.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace CustomIdentityApp.Controllers
{
    public class UsersController : Controller
    {
       
        UserManager<User> _userManager;
         
        private IdentityResult result;
        private readonly SignInManager<User> _signInManager;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View(_userManager.Users.ToList());
        //public async Task<IActionResult> Index()
        //{
        //    return View();
        //}

        [Authorize]
        public IActionResult Create() => View();
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email};
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id).ConfigureAwait(false);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    

                    var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
        
        User user = await _userManager.FindByIdAsync(id);
           
                if (user != null)
                {
                    if (User.Identity.Name == user.UserName)
                    {
                   
                        await _signInManager.SignOutAsync().ConfigureAwait(false);
                  
                    }
                    result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь не найден");
                }
            
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blocked(string id)
        {

            User user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                if (User.Identity.Name == user.UserName)
                {
                    //user.IsSelected = "Blocked";
                    await _signInManager.SignOutAsync().ConfigureAwait(false);

                }
                //result = await _userManager.DeleteAsync(user).ConfigureAwait(false);
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Unblock(string id)
        {


            User user = await _userManager.FindByIdAsync(id).ConfigureAwait(false);
           // user.IsBlocked = "Unblocked";
            var result = await _userManager.UpdateAsync(user).ConfigureAwait(false);
            //user.IsSelected = "Unblocked";


            return RedirectToAction("Index");

        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> DeleteAll(string[] selectedUsers)
        {
            for (int i = 0; i < selectedUsers.Length; i++)
            {
                User user = await _userManager.FindByIdAsync(selectedUsers[i]).ConfigureAwait(false);
                if (user != null)
                {
                   
                    await _signInManager.SignOutAsync().ConfigureAwait(false);
                    _ = await _userManager.DeleteAsync(user).ConfigureAwait(false);
                }

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BlockAll(string[] selectedUsers)
        {

            for (int i = 0; i < selectedUsers.Length; i++)
            {
                User user = await _userManager.FindByIdAsync(selectedUsers[i]);
                await _signInManager.SignOutAsync().ConfigureAwait(false);
                //user.IsSelected = "Blocked";
                var result = await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UnblockAll (string[] selectedUsers)
        {

            for (int i = 0; i < selectedUsers.Length; i++)
            {
                User user = await _userManager.FindByIdAsync(selectedUsers[i]);
               // user.IsBlocked = "Unblocked";
                var result = await _userManager.UpdateAsync(user);
                //user.IsSelected = "Unblocked";
            }

            return RedirectToAction("Index");

        }
    }
  



}