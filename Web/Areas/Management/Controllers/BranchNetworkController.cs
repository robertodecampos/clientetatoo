using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TatooReport.Exceptions;
using TatooReport.Models;
using TatooReport.Models.ViewModels.Management.BranchNetwork;
using TatooReport.Utils;

namespace TatooReport.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class BranchNetworkController : Controller
    {
        private Connection Connection { get; set; }
        private UserManager<User> UserManager { get; set; }

        public BranchNetworkController(Connection connection, UserManager<User> userManager)
        {
            Connection = connection;
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                int userId = (await UserManager.GetUserAsync(User)).Id;
                List<BranchNetwork> branchNetworks = BranchNetwork.FindByUserId(userId, Connection, null);

                if (branchNetworks.Count == 0)
                    throw new BranchNetworkException("Não existe nenhuma rede vinculada ao seu usuário!");

                var branchNetworkLines = new List<BranchNetworkLine>();

                foreach (BranchNetwork branchNetwork in branchNetworks)
                {
                    branchNetworkLines.Add(
                        new BranchNetworkLine
                        {
                            Id = branchNetwork.Id,
                            Name = branchNetwork.Name,
                            NumberBranches = Branch.FindAllByBranchNetwork(branchNetwork.Id, Connection, null).Count()
                        }
                    );
                }

                return View(branchNetworkLines);
            }
            catch (BranchNetworkException exception)
            {
                ViewBag.Message = exception.Message;
            }
            catch (Exception)
            {
                ViewBag.Message = "Ocorreu um erro ao listas as redes. Por favor, tente novamente em alguns instantes!";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
                ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create model)
        {
            MySqlTransaction transaction = null;

            try
            {
                int userId = (await UserManager.GetUserAsync(User)).Id;

                transaction = Connection.BeginTransaction();

                var branchNetwork = new BranchNetwork
                {
                    Name = model.Name
                };

                branchNetwork.Save(Connection, transaction);

                var branchNetworkUser = new BranchNetworkUser
                {
                    BranchNetworkId = branchNetwork.Id,
                    UserId = userId
                };

                branchNetworkUser.Save(Connection, transaction);

                transaction.Commit();

                if (string.IsNullOrEmpty(model.ReturnUrl))
                    return LocalRedirect("~/Management/BranchNetwork");
                else
                    return LocalRedirect(model.ReturnUrl);
            }
            catch (BranchNetworkException exception)
            {
                transaction?.Rollback();
                ViewBag.Message = exception.Message;
            }
            catch (Exception)
            {
                transaction?.Rollback();
                ViewBag.Message = "Ocorreu um erro ao tentar cadastrar a rede";
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Policy = "HasAccessInBranchNetwork")]
        public IActionResult Details(int branchNetworkId)
        {
            var details = new BranchNetworkDetails();

            try
            {
                BranchNetwork branchNetwork = BranchNetwork.FindById(branchNetworkId, Connection, null);

                details.Id = branchNetwork.Id;
                details.Name = branchNetwork.Name;
                details.Users = Models.User.FindByBranchNetworkId(branchNetworkId, Connection, null);
                details.Branches = Branch.FindAllByBranchNetwork(branchNetworkId, Connection, null);
            }
            catch (Exception)
            {

            }

            return View(details);
        }

        [HttpGet]
        [Authorize(Policy = "HasAccessInBranchNetwork")]
        public IActionResult Users(int branchNetworkId)
        {
            List<User> users = Models.User.FindByBranchNetworkId(branchNetworkId, Connection, null);

            return View(users);
        }

        [HttpGet]
        [Authorize(Policy = "HasAccessInBranchNetwork")]
        public IActionResult Branches(int branchNetworkId)
        {
            List<Branch> branches = Branch.FindAllByBranchNetwork(branchNetworkId, Connection, null);

            return View(branches);
        }
    }
}