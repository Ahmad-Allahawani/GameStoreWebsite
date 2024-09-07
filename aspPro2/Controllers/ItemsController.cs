//using aspPro2.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

//namespace aspPro2.Controllers
//{
//    public class ItemsController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public ItemsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        //get the indext view and the Student table by making 
//        //a function that retrieves the student info from the 
//        //data base and passes them to the index view

//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.student.ToListAsync());

//        }
//        public IActionResult Create()
//        {
//            return View();
//        }

//        //by making this function we can now create a new student instance
//        //and then add it to the list view and save the changes in the database
//        // and after submitting the form the function return a list of students AKA(index view)
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(Students student)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.student.Add(student);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(student);
//        }
//        // in this function after the user click on details this function will
//        // execute by taking the students id as a parameter and then check if its
//        // null or not(if its null the else block will executes) 
//        // if the id is not null it will access the Student table and use the
//        // "FirstOrDefaultAsync" method to to find the first record that matches 
//        // the given condition (m => m.Id == id).
//        // the lambda expression specifies the conditon to match the given id with the 
//        // student "Id" property, after that it will return the student View with data

//        public async Task<IActionResult> Details(int? id,String email)
//        {
//            if (id != null && email != null)
//            {
//                var Students = await _context.student
//                    .FirstOrDefaultAsync(m => m.Id == id && m.Email == email);
//                return View(Students);
//            }
//            else
//            {
//                return NotFound();
//            }

//        }

//        // This method retrieves the student data for the Edit view.
//        // When the user clicks the Edit button, this method is executed.
//        // It first checks if the provided id is null.
//        // If the id is null, it returns a NotFound() response.
//        // If the id is not null, it searches for the student data that matches the provided id.
//        // If a matching student is found, it stores the data in the 'Student' variable.
//        // If no student is found (Student is null), it returns a NotFound() response.
//        // If a student is found, it returns the Edit view with the student's data.

//        public async Task<IActionResult> Edit(int? id ,string email)
//        {
//			//if (id == null)
//			//{
//			//    return NotFound();

//			//}
//			//var student = await _context.Students.FindAsync(id);
//			//if (student == null)
//			//{
//			//    return NotFound();
//			//}
//			//return View(student);

//			if (id == null && email == null)
//            {
//                return NotFound();
//            }
//            var student = await _context.student
//                .FirstOrDefaultAsync(m=> m.Id == id || m.Email == email);
//            if(student == null)
//            {
//                return View();

//            }
//            return View(student);


//		}

//        // This method handles the POST request for editing a student's information.
//        // It is decorated with the HttpPost attribute to specify that it handles POST requests.
//        // The ValidateAntiForgeryToken attribute is used to protect against CSRF attacks.
//        // The method takes two parameters: the student's id and a student object containing the updated data.
//        // It first checks if the provided id matches the id of the student object. If they do not match, it returns a NotFound() response.
//        // If the model state is valid, it tries to update the student's information in the database.
//        // If a DbUpdateConcurrencyException is caught, it checks if the student still exists.
//        // If the student does not exist, it returns a NotFound() response. Otherwise, it rethrows the exception.
//        // If the update is successful, it redirects to the Index action.
//        // If the model state is not valid, it returns the student object back to the view for correction.

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int? id, Students student)
//        {

//            if (id != student.Id)
//            {
//                return NotFound();
//            }
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(student);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (StudentsExists(student.Id, student))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));

//            }
//            return View(student);
//        }


//        // this method is made to check if the student with the given id exists in the data base 
//        // it returns true if the student with the given id exists otherwise it return false
//        // we made this method to help with handling the "DbUpdateConcurrencyException" to make sure
//        // the student still exists in the db


//        private bool StudentsExists(int id, Students student)
//        {
//            if (id == student.Id)
//            {
//                return _context.student.Any();
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id != null)
//            {
//                var student = await _context.student.FirstOrDefaultAsync(m => m.Id == id);
//                if (student == null)
//                {
//                    return NotFound();
//                }
//                return View(student);
//            }
//            else
//            {
//                return NotFound();
//            }
//        }
//        [HttpPost, ActionName("delete")]
//        [ValidateAntiForgeryToken]

//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var student = await _context.student.FindAsync(id);
//            if (student != null)
//            {
//                _context.student.Remove(student);
//            }
//            else
//            {
//                return NotFound();
//            }
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));

//        }
//        //public IActionResult Profile(Student student)
//        //{
//        //    return View();
//        //}
        
//        public async Task<IActionResult> Profile( int? id)
//        {
//            if (id !=null)
//            {
//                var student = await _context.student.FirstOrDefaultAsync(m => m.Id == id);
//                return View(student);
                
//            }
//            else
//            {
//                return NotFound();
//            }
//        }
//   //     public IActionResult Search()
//   //     {
//			//return View();
//   //     }
//   //     [HttpPost, ActionName("search")]
//   //     [ValidateAntiForgeryToken]
//   //     public async Task<IActionResult> Search(string name)
//   //     {
//   //         if (name != null)
//   //         {
//   //             var item = await _context.Items.FirstOrDefaultAsync(s => s.Name == name);
//   //             //return View($"{nameof(Profile)}", student);
//   //             return RedirectToAction("Index", "home");

//   //         }
//   //         else
//   //         {
//   //             return NotFound();
//   //         }


//   //     }



//        //////old login form 
//        //public IActionResult login()
//        //{
//        //    return View();
//        //}

//        //[HttpPost, ActionName("login")]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> Login(int? id, String password)
//        //{
//        //    if (id != null && ModelState.IsValid)
//        //    {
//        //        var student = await _context.Students
//        //            .FirstOrDefaultAsync(m => m.Id == id && m.password == password);
//        //        if (student != null)
//        //        {
//        //            return RedirectToAction("Index", "Home");
//        //        }
//        //        else
//        //        {
//        //            return NotFound();
//        //        }
//        //    }
//        //    else
//        //    {
//        //        return View();
//        //    }
//        //}

//        //public IActionResult register()
//        //{
//        //    return View();
//        //}
//        //[HttpPost, ActionName("register")]
//        //[ValidateAntiForgeryToken]

//        //public async Task<IActionResult> RegisterForm(Student student, String Email)
//        //{
//        //    if (Email != null)
//        //    {
//        //        _context.Students.Add(student);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Edit), new { id = student.Id, email = Email });
//        //    }
//        //    return NotFound();
//        //}

//        //public async Task<IActionResult> RgisterCONT(String Email, String password)
//        //{
//        //    if (Email != null && password != null)
//        //    {
//        //        var student = await _context.Students
//        //            .FindAsync(Email);
//        //        return RedirectToAction(nameof(Index));
//        //    }
//        //    else
//        //    {
//        //        return View();
//        //    }
//        //}


//        // migrated to other controller

//        //public IActionResult signUp()
//        //{
//        //    return View();
//        //}

//        //[HttpPost, ActionName("Sign In")]
//        //[ValidateAntiForgeryToken]
//        //public async Task<IActionResult> SignInCONFIRM(string email , String password)
//        //{
//        //    if (email != null)
//        //    {
//        //        var student = await _context.Students
//        //            .FirstOrDefaultAsync(m => m.Email == email && m.password == password);
//        //        if (student != null)
//        //        {
//        //            return RedirectToAction("Index", "Home");
//        //        }
//        //        else
//        //        {
//        //            return View();
//        //        }
//        //    }
//        //    else
//        //    {
//        //        return View();
//        //    }
//        //}

//        //[HttpPost, ActionName("sign up")]
//        //[ValidateAntiForgeryToken]

//        //public async Task<IActionResult> signUpForm(Student student, String Email)
//        //{
//        //    if (Email != null)
//        //    {
//        //        _context.Students.Add(student);
//        //        await _context.SaveChangesAsync();
//        //        return RedirectToAction(nameof(Edit), new { id = student.Id, email = Email });
//        //    }
//        //    return NotFound();
//        //}
//    }

//}
