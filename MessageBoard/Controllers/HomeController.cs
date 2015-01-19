using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MessageBoard.Data;
using MessageBoard.Models;
using MessageBoard.Services;

namespace MessageBoard.Controllers
{
  public class HomeController : Controller
  {
    private readonly IMailService _mailService;
    private readonly IMessageBoardRepository _messageBoardRepository;

    public HomeController(IMailService mailService, IMessageBoardRepository messageBoardRepository)
    {
      _mailService = mailService;
      _messageBoardRepository = messageBoardRepository;
    }

    public ActionResult Index()
    {
      ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

      var topics = _messageBoardRepository.GetTopics()
                                          .OrderByDescending(t => t.Created)
                                          .Take(25)
                                          .ToList();

      return View(topics);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }

    [HttpPost]
    public ActionResult Contact(ContactModel model)
    {
      var msg = string.Format("Comment From: {1}{0}Email: {2}{0}Website: {3}{0}Comment: {4}{0}",
          Environment.NewLine,
          model.Name, model.Email,
          model.Website,
          model.Comment);

      if (_mailService.SendMail("noreply@madrus.nl",
          "info@madrus.nl",
          "Website Contact",
          msg))
      {
        ViewBag.MailSent = true;
      }

      return View();
    }

    [Authorize]
    public ActionResult MyMessages()
    {
      return View();
    }

    [Authorize(Users = "madrus@gmail.com")]
    [Authorize(Roles = "Admin")]
    public ActionResult Moderation()
    {
      return View();
    }
  }
}
