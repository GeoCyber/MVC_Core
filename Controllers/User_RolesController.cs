﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FixedModules.Controllers
{
    public class User_RolesController : Controller
    {
        // GET: User_Roles
        public ActionResult Index()
        {
            return View();
        }

        // GET: User_Roles/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User_Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User_Roles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User_Roles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User_Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User_Roles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}