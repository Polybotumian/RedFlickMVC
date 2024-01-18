using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedFlickMVC.Models;

namespace RedFlickMVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RedFlickDbContext _dbContext;

        public MovieController(ILogger<HomeController> logger, RedFlickDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult All()
        {
            AllViewModel allViewModel = new AllViewModel()
            {
                Movies = _dbContext.Movies
                            .Include(movie => movie.Categories)
                            .Include(movie => movie.Actors)
                            .Include(movie => movie.Directors)
                            .ToList(),
            Categories = _dbContext.Categories.ToList(),
                Actors = _dbContext.Actors.ToList(),
                Directors = _dbContext.Directors.ToList()
            };
            return View(allViewModel);
        }

        [HttpPost]
        public IActionResult Add([FromForm] Movie movie, [FromForm] List<int> selectedCategories, [FromForm] List<int> selectedActors, [FromForm] List<int> selectedDirectors)
        {
            try
            {
                // Ensure that the associated lists are not null before creating the movie
                movie.Categories = _dbContext.Categories.Where(c => selectedCategories.Contains(c.Id)).ToList();
                movie.Actors = _dbContext.Actors.Where(a => selectedActors.Contains(a.Id)).ToList();
                movie.Directors = _dbContext.Directors.Where(d => selectedDirectors.Contains(d.Id)).ToList();

                _dbContext.Movies.Add(movie);
                _dbContext.SaveChanges();

                return Json(new { success = true, message = "Movie added successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);

                return Json(new { success = false, message = "Failed to add the movie." });
            }
        }

        [HttpPost]
        public IActionResult Update([FromForm] Movie movie, [FromForm] List<int> selectedCategories1, [FromForm] List<int> selectedActors1, [FromForm] List<int> selectedDirectors1)
        {
            try
            {
                // Ensure that the associated lists are not null before creating the movie
                movie.Categories = _dbContext.Categories.Where(c => selectedCategories1.Contains(c.Id)).ToList();
                movie.Actors = _dbContext.Actors.Where(a => selectedActors1.Contains(a.Id)).ToList();
                movie.Directors = _dbContext.Directors.Where(d => selectedDirectors1.Contains(d.Id)).ToList();

                _dbContext.Movies.Update(movie);
                _dbContext.SaveChanges();

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);

                return Json(new { success = false, message = "Failed to update the movie." });
            }
        }

        [HttpPost]
        public IActionResult Delete([FromForm] int id)
        {
            try
            {
                var movie = _dbContext.Movies.Find(id);

                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();

                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine(ex.Message);

                return Json(new { success = false, message = "Failed to update the movie." });
            }
        }

        //[HttpPost]
        //public IActionResult LoadData([FromBody] MovieDataTableRequest request)
        //{
        //    try
        //    {
        //        // Perform data processing based on DataTablesRequest parameters
        //        var result = _dbContext.Movies
        //            .Include(movie => movie.Categories)
        //            .Include(movie => movie.Actors)
        //            .Include(movie => movie.Directors)
        //            .ToList(); // You need to adjust this based on your actual data retrieval logic

        //        return Json(new { draw = request.Draw, recordsTotal = result.Count, recordsFiltered = result.Count, data = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log and handle the exception
        //        Console.WriteLine(ex.Message);
        //        return Json(new { error = "Error occurred while processing data." });
        //    }
        //}
    }
}
