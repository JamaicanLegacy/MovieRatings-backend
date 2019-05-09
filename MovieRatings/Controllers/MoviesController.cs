using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRatings.Common.Repositories;
using MovieRatings.Models;

namespace MovieRatings.Views.Movies
{
    public class MoviesController : Controller
    {


        private readonly IMoviesRepository movieRepository;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _environment;

        // GET: /<controller>/

        public MoviesController(IMoviesRepository movieRepository, IHostingEnvironment environment, AppDbContext context)
        {
            this.movieRepository = movieRepository;
            _context = context;
            _environment = environment;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await Task.FromResult(movieRepository.GetAllMovies()));
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await Task.FromResult(movieRepository.GetMovieById(id.Value));
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<string> Base64imageConversion(Movie movie, IFormFile image)
        {
            // generate file path?
            var directory = $@"{_environment.WebRootPath}/images/";
            var filePath = directory + image.FileName;
            // check if the directory already exist
            if (!Directory.Exists(directory))
            {
                await Task.Run(() =>
                {
                    Directory.CreateDirectory(directory);

                });

            }

            if (!System.IO.File.Exists(filePath))
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                    var ext = Path.GetExtension(filePath).Replace(".", "");
                    var imgData = string.Format("data:image/{0};base64,{1}", ext, Convert.ToBase64String(ReadToEnd(stream)));
                    movie.ImgThumbnailUrl = imgData;
                }
            }
            else
            {
                using (Stream stream = new FileStream(filePath, FileMode.Open))
                {
                    var ext = Path.GetExtension(filePath).Replace(".", "");
                    var imgData = string.Format("data:image/{0};base64,{1}", ext, Convert.ToBase64String(ReadToEnd(stream)));
                    movie.ImgThumbnailUrl = imgData;
                }
            }

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            return movie.ImgThumbnailUrl;
        }
        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,Title,ReleaseDate,Description,Price")] Movie movie, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                // add saved file path to database
                movie.ImgThumbnailUrl=await Base64imageConversion(movie, image);
                _context.Add(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Title,ReleaseDate,Description,Price")] Movie movie, IFormFile image)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    movie.ImgThumbnailUrl = await Base64imageConversion(movie, image);
                    _context.Update(movie);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return movieRepository.GetAllMovies().Any(e => e.MovieId == id);
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}
