using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB_BUSINESS.Models;
using business;
using System.IO;
using System.Web.Hosting;
using OfficeOpenXml;

namespace WEB_BUSINESS.Controllers
{
    public class SubjectsController : Controller
    {
        private WEB_BUSINESSContext db = new WEB_BUSINESSContext();

        // GET: Subjects
        public ActionResult Index()
        {
            return View(db.Subjects.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ITaxNum,Name,StartDate,TaxType")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ITaxNum,Name,StartDate,TaxType")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
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

		[HttpPost]
		public ActionResult Upload(HttpPostedFileBase file)
		{
			if (file != null && file.ContentLength > 0)
			{
				List<Subject> buffer = new List<Subject>();
				Serializator.ReadFileIO(file.InputStream, ref buffer);

				foreach (var value in buffer)
				{

					db.Subjects.Add(value);
					db.SaveChanges();
				}
			}

			return RedirectToAction("Index");
		}


		public ActionResult DownloadXls()
		{
			var ms = new MemoryStream();
			FileInfo newFile = new FileInfo(HostingEnvironment.ApplicationPhysicalPath + @"\temp");
			using (ExcelPackage Package = new ExcelPackage(newFile))
			{

				var worksheet = Package.Workbook.Worksheets.Add("Список ИП");

				var i = 1;
				foreach (var g in db.Subjects)
				{
					worksheet.Cells[i, 1].Value = g.Name;
					worksheet.Cells[i, 2].Value = g.StartDate.ToString();
					worksheet.Cells[i, 3].Value = g.TaxType.ToString();
					worksheet.Cells[i, 4].Value = g.ITaxNum.ToString();
					i++;
				}

				// Заполнение файла Excel вышими данными
				Package.SaveAs(ms);
			}

			return File(ms.ToArray(), "application/ooxml", (newFile.Name).Replace(" ", "_") + ".xlsx");
		}


	}
}
