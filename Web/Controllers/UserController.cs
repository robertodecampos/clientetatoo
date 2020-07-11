using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration;
using TatooReport.Exceptions;
using TatooReport.Models;
using TatooReport.Models.ViewModels.User;
using TatooReport.Utils;

namespace TatooReport.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly Connection connection;
        private readonly ILoggerFactory loggerFactory;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, Connection connection, ILoggerFactory loggerFactory)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.connection = connection;
            this.loggerFactory = loggerFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Create model)
        {
            var user = new User();

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new UserException("Por favor, preencha todas as informações necessárias!");
                }

                user.Nome = model.Nome;
                user.Sobrenome = model.Sobrenome;
                user.Cpf = model.Cpf;
                user.Email = model.Email;
                user.Telefone = model.Telefone;
                user.Celular = model.Celular;
                user.UserName = model.Cpf;

                IdentityResult identityResult = await userManager.CreateAsync(user, model.Senha);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    var errosDuplicate = identityResult.Errors.Where(erro => erro.Code == "DuplicateUserName").ToList();

                    if (errosDuplicate.Count > 0)
                    {
                        throw new UserException("Este CPF já está cadastrado no sistema!");
                    }
                    else
                    {
                        throw new UserException(identityResult.Errors.ToString());
                    }
                }
            }
            catch (UserException erro)
            {
                ViewData["erro"] = erro.Message;
                return View(model);
            }
            catch (Exception erro)
            {
                ViewData["erro"] = erro.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(Login model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new UserException("Por favor, informe o usuário e senha!");

                Microsoft.AspNetCore.Identity.SignInResult resultSign = await signInManager.PasswordSignInAsync(model.Email, model.Senha, model.Lembrar, lockoutOnFailure: false);

                if (!resultSign.Succeeded)
                    throw new UserException("E-mail e/ou senha não encontrado(s)!");

                if (string.IsNullOrEmpty(returnUrl) || returnUrl.Equals("/"))
                {
                    int userId = (await userManager.GetUserAsync(User)).Id;

                    List<BranchNetwork> branchNetworks = BranchNetwork.FindByUserId(userId, connection, null);

                    if (branchNetworks.Count == 0)
                    {
                        return LocalRedirect("~/Management/BranchNetwork/Create?returnUrl=~/Management/BranchNetwork");
                    }
                    else
                    {
                        return Redirect(Url.Link("management", new { area = "Management", controller = "BranchNetwork", action = "Index" }));
                    } 
                }
                else
                    return LocalRedirect(returnUrl);
            }
            catch (UserException erro)
            {
                ViewBag.Mensagem = erro.Message;
            }
            catch (Exception erro)
            {
                ILogger<ConsoleLogger> logger = loggerFactory.CreateLogger<ConsoleLogger>();
                logger.LogError("Exception: " + erro.Message);
                ViewBag.Mensagem = "Ocorreu um erro inesperado ao verificar o e-mail e senha!";
            }

            return View();
        }
    }
}