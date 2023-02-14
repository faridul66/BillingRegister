﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BJProduction.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BJProduction.Controllers
{

    public class Sales_OrderController : Controller
    {

        public Sales_OrderController()
        {

        }
        public Sales_OrderController(ApplicationUserManager userManager,
            ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sales_Order
        public ActionResult Index()
        {
            var sales_Order = db.Sales_Order.Include(s => s.Company).Include(s => s.Customer).Include(s => s.Feature).Include(s => s.Location).Include(s => s.Product_Type).Include(s => s.Unit_Measurement);
            return View(sales_Order.ToList());
        }

        // GET: Sales_Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Order sales_Order = db.Sales_Order.Find(id);
            if (sales_Order == null)
            {
                return HttpNotFound();
            }
            return View(sales_Order);
        }

        // GET: Sales_Order/Create
        public ActionResult Create()
        {
            ViewBag.Companyid = new SelectList(db.Companies, "Id", "Company_Name");
            ViewBag.Customerid = new SelectList(db.Customers, "id", "name");
            ViewBag.Featureid = new SelectList(db.Features, "id", "feature_name");
            ViewBag.Locationid = new SelectList(db.Locations.Include(l => l.Location_Type).Where(l => l.Location_Type.name == "Country"), "id", "name");
            ViewBag.Product_Typeid = new SelectList(db.Product_Type, "id", "type_name");
            ViewBag.Unit_Measurementid = new SelectList(db.Unit_Measurement, "id", "code");
            return View();
        }

        // POST: Sales_Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Companyid,Customerid,Product_Typeid,qty,Featureid,sale_count,Unit_Measurementid,Locationid,order_no,sell_date,status,chged_by,chgd_date")] Sales_Order sales_Order)
        {
            if (ModelState.IsValid)
            {
                db.Sales_Order.Add(sales_Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Companyid = new SelectList(db.Companies, "Id", "Company_Code", sales_Order.Companyid);
            ViewBag.Customerid = new SelectList(db.Customers, "id", "code", sales_Order.Customerid);
            ViewBag.Featureid = new SelectList(db.Features, "id", "feature_code", sales_Order.Featureid);
            ViewBag.Locationid = new SelectList(db.Locations, "id", "name", sales_Order.Locationid);
            ViewBag.Product_Typeid = new SelectList(db.Product_Type, "id", "type_code", sales_Order.Product_Typeid);
            ViewBag.Unit_Measurementid = new SelectList(db.Unit_Measurement, "id", "code", sales_Order.Unit_Measurementid);
            return View(sales_Order);
        }

        // GET: Sales_Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Order sales_Order = db.Sales_Order.Find(id);
            if (sales_Order == null)
            {
                return HttpNotFound();
            }
            ViewBag.Companyid = new SelectList(db.Companies, "Id", "Company_Code", sales_Order.Companyid);
            ViewBag.Customerid = new SelectList(db.Customers, "id", "code", sales_Order.Customerid);
            ViewBag.Featureid = new SelectList(db.Features, "id", "feature_code", sales_Order.Featureid);
            ViewBag.Locationid = new SelectList(db.Locations, "id", "name", sales_Order.Locationid);
            ViewBag.Product_Typeid = new SelectList(db.Product_Type, "id", "type_code", sales_Order.Product_Typeid);
            ViewBag.Unit_Measurementid = new SelectList(db.Unit_Measurement, "id", "code", sales_Order.Unit_Measurementid);
            return View(sales_Order);
        }

        // POST: Sales_Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Companyid,Customerid,Product_Typeid,qty,Featureid,sale_count,Unit_Measurementid,Locationid,order_no,sell_date,status,chged_by,chgd_date")] Sales_Order sales_Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Companyid = new SelectList(db.Companies, "Id", "Company_Code", sales_Order.Companyid);
            ViewBag.Customerid = new SelectList(db.Customers, "id", "code", sales_Order.Customerid);
            ViewBag.Featureid = new SelectList(db.Features, "id", "feature_code", sales_Order.Featureid);
            ViewBag.Locationid = new SelectList(db.Locations, "id", "name", sales_Order.Locationid);
            ViewBag.Product_Typeid = new SelectList(db.Product_Type, "id", "type_code", sales_Order.Product_Typeid);
            ViewBag.Unit_Measurementid = new SelectList(db.Unit_Measurement, "id", "code", sales_Order.Unit_Measurementid);
            return View(sales_Order);
        }

        // GET: Sales_Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_Order sales_Order = db.Sales_Order.Find(id);
            if (sales_Order == null)
            {
                return HttpNotFound();
            }
            return View(sales_Order);
        }

        // POST: Sales_Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales_Order sales_Order = db.Sales_Order.Find(id);
            db.Sales_Order.Remove(sales_Order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetFormData(string[] cpl)
        {
            int companyId = Convert.ToInt32(cpl[0]);
            string salesOrder = cpl[1];
            string lotNumber = cpl[2];
            string[] messages = new string[2];


            if (db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.lot_number == lotNumber && x.Sales_Order.order_no == salesOrder && x.Sales_Order.Companyid == companyId && x.status == "C").ToList().Count != 0)
            {
                messages[0] = "C";
                if (db.Sales_Order.Where(x => x.order_no == salesOrder && x.Companyid == companyId && x.status == "C").ToList().Count != 0)
                {
                    messages[1] = "C";
                }
                else
                {
                    messages[1] = "N";
                }
                return Json(messages, JsonRequestBehavior.AllowGet);
            }
            else if (db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.lot_number == lotNumber && x.Sales_Order.order_no == salesOrder && x.Sales_Order.Companyid == companyId).ToList().Count != 0)
            {
                messages[0] = "F";
                if (db.Sales_Order.Where(x => x.order_no == salesOrder && x.Companyid == companyId && x.status == "C").ToList().Count != 0)
                {
                    messages[1] = "C";
                }
                else
                {
                    messages[1] = "N";
                }
                var salesOrders = db.Sales_Order.Where(x => x.order_no == salesOrder && x.Companyid == companyId).ToList();
                var productAndSalesLedger = db.Sales_Ledger.Include(x => x.Product).Include(x => x.Sales_Order).Where(x => x.lot_number == lotNumber && x.Sales_Order.order_no == salesOrder).ToList();
                var productId = db.Sales_Ledger.Include(y => y.Product).Include(y => y.Sales_Order).FirstOrDefault(y => y.lot_number == lotNumber).Productid;
                var productFeature = db.Product_Feature.Include(x => x.Feature).Where(x => x.Productid == productId).ToList();
                var warehouseId = db.Sales_Ledger.Include(x => x.Sales_Order).FirstOrDefault(x => x.lot_number == lotNumber && x.Sales_Order.order_no == salesOrder).Locationid;
                var warehouse = db.Locations.Where(x => x.id == warehouseId);
                var orderData = new dynamic[] { messages, salesOrders, productAndSalesLedger, productFeature, warehouse };
                return Json(orderData, JsonRequestBehavior.AllowGet);
            }

            else
            {
                messages[0] = "N";
                if (db.Sales_Order.Where(x => x.order_no == salesOrder && x.Companyid == companyId && x.status == "C").ToList().Count != 0)
                {
                    messages[1] = "C";
                }
                else
                {
                    messages[1] = "N";
                }
                return Json(messages, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [HttpPost]
        public JsonResult SaveData(string[][][] values)
        {

            var orderNo = values[0][8][0];
            var lotNumber = values[0][15][0];
            var companyId = Convert.ToInt32(values[0][0][0]);

            // var salesOrderId = db.Sales_Order.FirstOrDefault(x => x.order_no == orderNo && x.Companyid == companyId).id;

            List<DuplicateBaleError> duplicateBaleErrors = new List<DuplicateBaleError>(); //value[2].Count, Loop values[2][i]

            //for (int i = 0; i < values[2].Length; i++)
            //{


            //    DuplicateBaleError DuplicateBales = new DuplicateBaleError()
            //    {
            //        Id = Convert.ToInt32(values[2][i][0]),
            //        BaleNumber = values[2][i][1],
            //        Lot = "1",
            //        Order = "SO001"
            //    };
            //    duplicateBaleErrors.Add(DuplicateBales);
            //}



            List<string> missingProducts = new List<string>();
            // Sales Ledger found and check sales order exists , if exists then set status of Sales Order= C
            if (db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.Sales_Order.order_no == orderNo && x.lot_number != lotNumber && x.Sales_Order.Companyid == companyId).ToList().Count != 0)
            {
                var salesOrderId = db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.Sales_Order.Companyid == companyId && x.Sales_Order.order_no == orderNo).FirstOrDefault().Sales_Order.id;
                Sales_Order po = db.Sales_Order.Find(salesOrderId) ?? throw new ArgumentNullException("db.Sales_Order.Find(salesOrderId)");
                po.status = "C";
                db.SaveChanges();

            }
            if (db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.Sales_Order.order_no == orderNo && x.lot_number == lotNumber && x.Sales_Order.Companyid == companyId).ToList().Count != 0)
            {
                var salesOrderId = db.Sales_Ledger.Include(x => x.Sales_Order).FirstOrDefault(x => x.lot_number == lotNumber && x.Sales_Order.order_no == orderNo).Sales_Order.id;
                Sales_Order po = db.Sales_Order.Find(salesOrderId);

                var productId = db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.lot_number == lotNumber && x.Sales_Order.order_no == orderNo).Select(x => x.Productid).ToList();

                //foreach (var item in productId)
                //{
                //    var productFeature = db.Product_Feature.Where(x => x.Productid == item);
                //    db.Product_Feature.RemoveRange(productFeature);
                //    db.SaveChanges();

                //    var salesLedger = db.Sales_Ledger.Where(x => x.Productid == item);
                //    db.Sales_Ledger.RemoveRange(salesLedger);
                //    db.SaveChanges();

                //    var product = db.Products.Where(x => x.id == item);
                //    db.Products.RemoveRange(product);
                //    db.SaveChanges();

                //}

                if (po.status != "C")
                {
                    db.Sales_Order.Remove(po);
                    db.SaveChanges();
                }
            }

            Sales_Order salesOrder = new Sales_Order();
            // First Array Element Order in Front end (Company_Id, Vendor_Id, Product_Type_Id, Total_Count, MainFeature_Id, MainFeature_Value, Main_Unit, Country_Id, Order_No, LC_Number, LC_Date, Purchase_Date, Arrival_Date, Site_Id, Warehouse_Id,Lot_Number)
            salesOrder.Companyid = Convert.ToInt32(values[0][0][0]);
            salesOrder.Customerid = Convert.ToInt32(values[0][1][0]);
            salesOrder.Product_Typeid = Convert.ToInt32(values[0][2][0]);
            salesOrder.qty = Convert.ToInt32(values[0][3][0]);

            int featureTypeid = Convert.ToInt32(values[0][4][0]);
            int featureId = db.Features.FirstOrDefault(x => x.Feature_Typeid == featureTypeid).id;
            salesOrder.Featureid = featureId;

            salesOrder.sale_count = Convert.ToDouble(values[0][5][0]);
            salesOrder.Unit_Measurementid = Convert.ToInt32(values[0][6][0]);
            salesOrder.Locationid = Convert.ToInt32(values[0][7][0]);
            salesOrder.order_no = values[0][8][0];
            salesOrder.sell_date = Convert.ToDateTime(values[0][11][0]);
            salesOrder.status = "";
            salesOrder.chged_by = UserManager.FindById(User.Identity.GetUserId()).UserName;
            salesOrder.chgd_date = DateTime.UtcNow;

            Sales_Order po1 = db.Sales_Order.FirstOrDefault(x => x.order_no == orderNo);  // Need to test
            int poid;
            if (po1 == null)
            {
                db.Sales_Order.Add(salesOrder);
                db.SaveChanges();
                poid = salesOrder.id;
            }
            else if (po1.status != "C")
            {
                db.SaveChanges();
                poid = po1.id;
            }
            else
            {
                poid = po1.id;
            }


            for (int i = 0; i < values[2].Length; i++)
            {
                Product product = new Product();
                Sales_Ledger salesLedger = new Sales_Ledger();

                //product.Product_Typeid = Convert.ToInt32(values[0][2][0]);
                //product.product_serial = values[2][i][1];
                //product.status = "";
                //product.chged_by = UserManager.FindById(User.Identity.GetUserId()).UserName;
                //product.chgd_date = DateTime.UtcNow;
                //db.Products.Add(product);
                //db.SaveChanges();

                salesLedger.Productid = product.id;
                salesLedger.sale_count = Convert.ToDouble(values[2][i][2]);
                salesLedger.Locationid = Convert.ToInt32(values[0][14][0]); ;
                salesLedger.delivery_date = Convert.ToDateTime(values[0][12][0]);
                salesLedger.Sales_Orderid = poid;
                salesLedger.lot_number = values[0][15][0];
                salesLedger.status = "";
                salesLedger.chgd_date = DateTime.UtcNow;
                salesLedger.chged_by = UserManager.FindById(User.Identity.GetUserId()).UserName;
                db.Sales_Ledger.Add(salesLedger);
                db.SaveChanges();

                //for (int j = 0; j < values[1].Length; j++)
                //{
                //    Product_Feature productFeature = new Product_Feature();
                //    productFeature.Productid = product.id;
                //    productFeature.Featureid = Convert.ToInt32(values[1][j][1]);
                //    productFeature.Value = 0;
                //    productFeature.Unit_Measurementid = null;//purchaseOrder.Unit_Measurementid;
                //    productFeature.status = "";
                //    productFeature.chgd_date = DateTime.UtcNow;
                //    productFeature.chged_by = UserManager.FindById(User.Identity.GetUserId()).UserName;
                //    db.Product_Feature.Add(productFeature);
                //    db.SaveChanges();
                //}


            }

            return Json(missingProducts.ToArray(), JsonRequestBehavior.AllowGet);
        }




        [Authorize]
        [HttpPost]
        public JsonResult SubmitData(string[] values)
        {
            var orderNo = values[0];
            var lotNumber = values[1];
            var transTypeId = db.Transaction_Type.FirstOrDefault(x => x.type_code == "SO").id;
            var orderId = db.Sales_Order.FirstOrDefault(x => x.order_no == orderNo).id;
            List<int> salesLedger = db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.Sales_Order.order_no == orderNo && x.lot_number == lotNumber).Select(x => x.id).ToList();
            foreach (var items in salesLedger)
            {
                var ledger = db.Sales_Ledger.Find(items);
                ledger.status = "C";
                db.SaveChanges();
                General_Ledger generalLedger = new General_Ledger();
                generalLedger.Productid = ledger.Productid;
                generalLedger.Transaction_Typeid = transTypeId;
                generalLedger.trans_ref_id = ledger.id;
                generalLedger.trans_date = ledger.delivery_date;
                generalLedger.is_current = true;
                generalLedger.chged_by = UserManager.FindById(User.Identity.GetUserId()).UserName;
                generalLedger.chgd_date = DateTime.Now;
                db.General_Ledger.Add(generalLedger);
                db.SaveChanges();
            }
            var salesOrder = db.Sales_Order.Find(orderId);
            if (salesOrder != null) salesOrder.status = "C";
            db.SaveChanges();


            return Json("", JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetProductData(string[][][] values)
        {
            var companyId = Convert.ToInt32(values[0][0][0]);
            var orderNo = values[0][1][0];
            var lot = values[0][2][0];
            var transactionTypeId = db.Transaction_Type.Where(x => x.type_code == "PO").FirstOrDefault().id;
            var productTypeId = Convert.ToInt32(values[0][3][0]);
            if (db.Sales_Ledger.Include(x => x.Sales_Order).Where(x => x.Sales_Order.order_no == orderNo && x.lot_number == lot && x.Sales_Order.Companyid == companyId).ToList().Count != 0)
            {

            }
            else
            {
                if (db.Temp_Feature.Where(x => x.CompanyId == companyId && x.OrderNo == orderNo && x.Lot == lot).ToList().Count != 0)
                {
                    var tempFeatures = db.Temp_Feature.Where(x => x.CompanyId == companyId && x.OrderNo == orderNo && x.Lot == lot).ToList();
                    db.Temp_Feature.RemoveRange(tempFeatures);
                    db.SaveChanges();

                }
                for (int i = 0; i < values[1].Length; i++)
                {
                    Temp_Feature temp_Feature = new Temp_Feature();
                    temp_Feature.CompanyId = companyId;
                    temp_Feature.OrderNo = orderNo;
                    temp_Feature.Lot = lot;
                    temp_Feature.Feature_Typeid = Convert.ToInt32(values[1][i][0]);
                    temp_Feature.Featureid = Convert.ToInt32(values[1][i][1]);
                    temp_Feature.Transaction_Typeid = transactionTypeId;
                    db.Temp_Feature.Add(temp_Feature);
                    db.SaveChanges();

                }
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
