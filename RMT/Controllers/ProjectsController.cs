using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RMT.Models;
using RMT.Helpers;
using System.IO;
using System.Diagnostics;

namespace RMT.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectContext db = new ProjectContext();

        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }
        public ActionResult ProjectsList()
        {
            return View(db.Projects.ToList());
        }
        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {

            createThumbnails();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
      
            Project project = db.Projects
                                    .Where(p => p.ProjectId == id)
                                    .Include("Pictures")
                                    .FirstOrDefault();

            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        public PartialViewResult PhotoDetailAjax(int id)
        {
            int nextPicture;
            int prevPicture;

            Picture p = db.Pictures
                    .Include("Comments")
                    .Where(x => x.PictureId == id)
                    .FirstOrDefault();

            if (p == null)
            {
                ViewBag.errorMessage = "Erreur";
                return PartialView("~Views/Shared/Error.cshtml");
            }
            
            //get next Id
            var result = db.Pictures
                .Where(x => x.ProjectId == p.ProjectId)
                .Where(x => x.PictureId > id)
                .Select(x => x.PictureId)
                .FirstOrDefault();

            if (result != null)
            {
                nextPicture = result;
            }
            else
            {
                nextPicture = db.Pictures
                    .Where(x => x.ProjectId == p.ProjectId)
                    .Select(x => x.PictureId)
                    .FirstOrDefault();
            }

            ViewBag.nextP = nextPicture;

            //get previous Id
            result = db.Pictures
                .OrderByDescending(y => y.PictureId)
                .Where(x => x.ProjectId == p.ProjectId)
                .Where(x => x.PictureId < id)
                .Select(x => x.PictureId)
                .FirstOrDefault();

            if (result != null)
            {
                prevPicture = result;
            }
            else
            {
                prevPicture = 1;
                //prevPicture = db.Pictures
                //    .Where(x => x.ProjectId == p.ProjectId)
                //    .Select(x => x.PictureId)
                //    .FirstOrDefault();
            }

            ViewBag.prevP = prevPicture;
            
            return PartialView(p); ;
        }

        public ActionResult PhotoDetail(int id)
        {
            Picture p = db.Pictures
                    .Include("Comments")
                    .Where(x => x.PictureId == id)
                    .FirstOrDefault();

            if (p == null)
            {
                ViewBag.errorMessage = "Erreur";
                return PartialView("~/Views/Shared/Error.cshtml");                
            }

            int projectId = p.ProjectId;
            int currentId = p.PictureId;
            
            int firstId = db.Pictures
                .Where(x => x.ProjectId == projectId)
                .Min(x => x.PictureId);

            int lastId = db.Pictures
                .Where(x => x.ProjectId == projectId)
                .Max(x => x.PictureId);

            int previousId = db.Pictures
                    .OrderByDescending(y => y.PictureId)
                    .Where(x => x.ProjectId == projectId)
                    .Where(x => x.PictureId < currentId)
                    .Select(x => x.PictureId)
                    .FirstOrDefault();

            int nextId = db.Pictures
                    .OrderBy(y => y.PictureId)
                    .Where(x => x.ProjectId == projectId)
                    .Where(x => x.PictureId > currentId)
                    .Select(x => x.PictureId)
                    .FirstOrDefault();
            
            if(currentId == lastId)
            {
                nextId = firstId;
            }

            if (currentId == firstId)
            {
                previousId = lastId;
            }

            ViewBag.nextP = nextId;
            ViewBag.prevP = previousId;

            return PartialView(p);
        }

        [Authorize(Roles = "Admin")]
        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ProjectId,Name,Description,ImagePath,Status,BeginDate,EndDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        [Authorize(Roles = "Admin")]
        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);
            
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,Name,Description,ImagePath,Status,BeginDate,EndDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult SaveUploadedFile([Bind(Include = "PictureId,ProjectId,PictureName,Description,Path")] Picture picture)
        {
            bool isSavedSuccessfully = false;
            string fName = "";
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {

                        var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\Images\\Projects\\Project{1}", Server.MapPath(@"\"), picture.ProjectId));

                        string pathString = Path.Combine(originalDirectory.ToString());

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = Directory.Exists(pathString);

                        if (!isExists)
                            Directory.CreateDirectory(pathString);

                        //Save file
                        var path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

                        //Save thummbnail
                        var thumbsPath = string.Format("{0}\\tn_{1}", pathString, file.FileName);
                        FileHelper.SaveResizedImage(pathString, file.FileName, thumbsPath, 30);

                        var relativePath = string.Format("~/Content/Images/Projects/Project{0}/{1}", picture.ProjectId, file.FileName);
                        
                        if (ModelState.IsValid)
                        {
                            picture.Path = relativePath;
                            db.Pictures.Add(picture);
                            db.SaveChanges();
                            isSavedSuccessfully = true;
                        }

                    }

                }
                
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { uploaded = true,  Message = "Votre photo a été ajoutée" });
            }
            else
            {
                return Json(new { uploaded = false, Message = "Erreur : Photo non enregistrée" });
            }
        }
        

        public void createThumbnails()
        {
            string tnFullPath;
            string tnName;
            
            DirectoryInfo diTop = new DirectoryInfo(Server.MapPath("~/Content/Images/Projects"));
            try
            {
                foreach (var di in diTop.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (var fi in di.EnumerateFiles("*", SearchOption.AllDirectories))
                        {
                            tnName = "";
                            tnFullPath = "";

                            try
                            {
                                Debug.WriteLine("{0}\\{1}", fi.DirectoryName, fi.Name);
                                
                                tnName = "tn_" + fi.Name;
                                tnFullPath = fi.DirectoryName + "\\" + tnName;

                                if (!System.IO.File.Exists(tnFullPath) && fi.Name.Substring(0,3) != "tn_")
                                {
                                    Debug.WriteLine("creation of th thumbnails : {0}", tnName);
                                    try
                                    {
                                        FileHelper.SaveResizedImage(fi.DirectoryName, fi.Name, tnName, 30);
                                    }
                                    catch (Exception)
                                    {
                                        Debug.WriteLine("File not found: {0}", fi.Name);
                                        //throw new FileNotFoundException();
                                    }   
                                }
                            }
                            catch (UnauthorizedAccessException UnAuthFile)
                            {
                                Debug.WriteLine("UnAuthFile: {0}", UnAuthFile.Message);
                            }
                        }
                    }
                    catch (UnauthorizedAccessException UnAuthSubDir)
                    {
                        Debug.WriteLine("UnAuthSubDir: {0}", UnAuthSubDir.Message);
                    }
                }
            }
            catch (DirectoryNotFoundException DirNotFound)
            {
                Debug.WriteLine("{0}", DirNotFound.Message);
            }
            catch (UnauthorizedAccessException UnAuthDir)
            {
                Debug.WriteLine("UnAuthDir: {0}", UnAuthDir.Message);
            }
            catch (PathTooLongException LongPath)
            {
                Debug.WriteLine("{0}", LongPath.Message);
            }
            
        }
    
        public void SetTitlePicture(int id)
        {
            using (ProjectContext ctx = new ProjectContext())
            {

                var picture = ( from pict in ctx.Pictures
                                where pict.PictureId == id
                                select pict).First();

                var project = (from pict in ctx.Pictures
                             join proj in ctx.Projects
                             on pict.ProjectId equals proj.ProjectId
                             where pict.PictureId == id
                             select proj).First();

                project.ImagePath = picture.Path;
                
                int result = ctx.SaveChanges();
            }
        }

        public ActionResult DeletePicture(int id)
        {

            Picture picture = db.Pictures.Find(id);
            db.Pictures.Remove(picture);
            db.SaveChanges();

            return RedirectToAction("Details", "Projects", new { id = picture.ProjectId });
        }
    }
}
