using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScheduler.Models;

namespace WebScheduler.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<Task> TaskList;
            using (var db = new TaskContext())
            {
                TaskList = db.Tasks.ToList();
            }
            return View(TaskList);
        }

        [HttpPost]
        public ActionResult AddTask(string newTask)
        {
            Task newT;
            List<Task> tasks;
            using (var db = new TaskContext())
            {
                if (newTask != null)
                {
                    newT = new Task {TaskContent = newTask};
                    db.Tasks.Add(newT);
                    db.SaveChanges();
                }
                tasks = db.Tasks.ToList();
            }
            
            return PartialView("TaskList",tasks);
        }

        [HttpGet]
        public ActionResult DeleteTask(int id)
        {
           
            List<Task> tasks;
            using (var db = new TaskContext())
            {
                var removableTask = db.Tasks.Find(id);
                if (removableTask != null)
                {
                    db.Entry(removableTask).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                tasks = db.Tasks.ToList();
            }
           
            return PartialView("TaskList", tasks);
        }

        [HttpPost]
        public ActionResult EditTask(string id, string task)
        {

            List<Task> tasks;
            if (id == null)
            {
                return HttpNotFound();
            }
            using (var db = new TaskContext())
            {
            
                var taskId = Int32.Parse(id);
                var editableTask = db.Tasks.Find(taskId);
                if (editableTask != null)
                {
                    editableTask.TaskContent = task;
                    db.Entry(editableTask).State = EntityState.Modified;
                    db.SaveChanges();

                }
                tasks = db.Tasks.ToList();
             }
            return PartialView("TaskList", tasks);
        }
    }
}