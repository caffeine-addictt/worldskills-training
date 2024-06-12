using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;


public class Dev : Controller
{
    [Route("/api/nuke")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Populate()
    {
        using (Ws1Context ctx =  new Ws1Context())
        {
            ctx.Answers.RemoveRange(ctx.Answers);
            ctx.Categories.RemoveRange(ctx.Categories);
            ctx.Options.RemoveRange(ctx.Options);
            ctx.Questions.RemoveRange(ctx.Questions);
            ctx.Saloons.RemoveRange(ctx.Saloons);
            ctx.Surveys.RemoveRange(ctx.Surveys);
            ctx.Users.RemoveRange(ctx.Users);
            ctx.Workshops.RemoveRange(ctx.Workshops);
            ctx.SaveChanges();
        }
        return Ok(new { Status = 200, Message = "Nuked database" });
    }

    // GET: HomeController
    public ActionResult Index()
    {
        return View();
    }

    // GET: HomeController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: HomeController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: HomeController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: HomeController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: HomeController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: HomeController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: HomeController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
