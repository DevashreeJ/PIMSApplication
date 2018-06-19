using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using PIMSApplication.Models;
using System.Net;
using System.IO;

namespace PIMSApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpGet]
		public ActionResult ApplicationForm()
		{
			
			return View();
		}

		[HttpPost]
		public ActionResult ApplicationForm(StudentInformationTable studentinfo) {

			if (ModelState.IsValid &&  studentinfo.Measure_type=="COUNT" && studentinfo.Category_Set_code=="ACT16" )
			{
				PIMSdbEntities pimsEntities = new PIMSdbEntities();
				pimsEntities.StudentInformationTables.Add(studentinfo);
				pimsEntities.SaveChanges();
				ViewBag.message = "Entry added to the database successfully";
				return View();
			}
			else {
				ModelState.Clear();
				ViewBag.message = "Something went wrong. Measure type has to be 'COUNT' and Category set code has to be 'ACT16'  ";
				return View(studentinfo);
			}
			
		}

		public ActionResult ShowEntries() {

			PIMSdbEntities pimsEntities = new PIMSdbEntities();
			return View(pimsEntities.StudentInformationTables.ToList());
		}


		public ActionResult DisplayFormat( string id) {

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			using (var context = new PIMSdbEntities())
			{

				//getting records of one district 
				var districtRecord = context.StudentInformationTables
					.Where(x => x.District_Code == id);

				//getting first record of a district for settign filename
				var districtOneRecord = context.StudentInformationTables
					.Where(x => x.District_Code == id)
					.First();

				string filename;
				DateTime todaydate = DateTime.Now;

				//Setting timestamp for filename
				string DateTimeString = todaydate.ToString("yyyyMMddHHmm");

				filename = districtOneRecord.District_Code + "_" + "student_" + DateTimeString;
				

				var result = WriteCsvToMemory(districtRecord.ToList());
				var memoryStream = new MemoryStream(result);
				//	return View(oneRecord);
				return new FileStreamResult(memoryStream, "text/csv") { FileDownloadName = filename+".csv" };

			}

		}


		//Writing data to csv file.
		public byte[] WriteCsvToMemory(dynamic records)
		{
			using (var memoryStream = new MemoryStream())
			using (var streamWriter = new StreamWriter(memoryStream))
			using (var csvWriter = new CsvWriter(streamWriter))
			{
				csvWriter.WriteRecords(records);
				streamWriter.Flush();
				return memoryStream.ToArray();
			}
		}

	}
}