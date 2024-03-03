using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SprintHEMone.Models;
using System.Security.Cryptography;
using System.Collections.Concurrent;

namespace SprintHEMone.Controllers
{
    public class CustomersController : Controller
    {
        private readonly RacerBookContext _context;

        public CustomersController(RacerBookContext context)
        {
            _context = context;
        }

        public sealed class LoginLogger
        {
            private static readonly LoginLogger instance = new LoginLogger();
            private static readonly object padlock = new object();
            private readonly string filePath = "login_attempts.txt";
            private ConcurrentDictionary<string, int> loginAttempts;

            private LoginLogger()
            {
                loginAttempts = new ConcurrentDictionary<string, int>();
            }

            public static LoginLogger Instance
            {
                get { return instance; }
            }

            public void LogFailedLoginAttempt(string username)
            {
                lock (padlock)
                {
                    loginAttempts.AddOrUpdate(username, 1, (key, oldValue) => oldValue + 1);
                    try
                    {
                        using (StreamWriter writer = System.IO.File.AppendText(filePath))
                        {
                            writer.WriteLine($"{DateTime.Now}: Failed login attempt for user '{username}'. Attempts: {loginAttempts[username]}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // You can handle the exception as per your application's requirements
                        // For simplicity, just rethrowing it here
                        throw ex;
                    }
                }
            }
        }

        // GET: Customers
        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token for security
        public async Task<IActionResult> Login(Customer model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user from the database based on the provided username
                var user = _context.Customers.SingleOrDefault(u => u.Email == model.Email);

                if (user != null && !string.IsNullOrEmpty(model.PasswordHash))
                {
                    // Hash the provided password for comparison with the hashed password stored in the database
                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(model.PasswordHash));
                        string hashedPassword = Convert.ToBase64String(bytes);

                        // Check if the hashed password matches the one stored in the database
                        if (user.PasswordHash == hashedPassword)
                        {
                            // Authentication successful
                            HttpContext.Response.Cookies.Append("Username", model.Email);
                            return RedirectToAction("Index", "Items");
                        }
                    }
                }
            }

            //Logging
            LoginLogger.Instance.LogFailedLoginAttempt(model.Email);

            // Authentication failed
            ModelState.AddModelError(string.Empty, "Invalid username or password");

            // Pass error message to error view
            TempData["ErrorMessage"] = "Invalid username or password";

            // Redirect to the login view with the error message
            return RedirectToAction("Index"); // Assuming your login view is named "Login
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Email == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("Username,PasswordHash,Name,Address")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Hash the password using SHA256
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(customer.PasswordHash));
                    customer.PasswordHash = Convert.ToBase64String(bytes);
                }

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email,PasswordHash,FullName,Address")] Customer customer)
        {
            if (id != customer.Email)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Email))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Email == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.Email == id);
        }
    }
}
