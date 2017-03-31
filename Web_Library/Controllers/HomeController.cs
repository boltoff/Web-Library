using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Library.Models;

namespace Web_Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //code bellow allow you to work with Books

        public ActionResult Books()
        {
            List<Book> bookslist = new List<Book>();
            try
            {
                ExecuteProcedureSelectAll(ref bookslist);
                return View(bookslist);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Get Insert or Update View
        /// </summary>
        /// <param name="bookId">if bookId not null it's Update View</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BookAction(int? bookId = null)
        {
            try
            {
                List<Author> authors = new List<Author>();
                ExecuteProcedureSelectAll(ref authors);
                ViewBag.Data = authors;
                if (bookId != null)
                {
                    ViewBag.Controller = "BookActionUpdate";
                    ViewBag.Legend = "Change Book";
                    ViewBag.ButtonName = "Save";
                    Book book = ExecuteProcedureSelectWhereBook(bookId);
                    return View(book);
                }
                else
                {
                    ViewBag.Controller = "BookActionInsert";
                    ViewBag.Legend = "Add Book";
                    ViewBag.ButtonName = "Add";
                    return View();
                }
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Insert book
        /// </summary>
        /// <param name="title"></param>
        /// <param name="publishedDate"></param>
        /// <param name="isbn"></param>
        /// <param name="authorId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BookActionInsert(string title, DateTime publishedDate, string isbn, int authorId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExecuteProcedureInsert(title, publishedDate, isbn, authorId);
                    return RedirectToAction("Books");
                }
                return View();
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Delete book
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public ActionResult BookActionDelete(int bookId)
        {
            try
            {
                ExecuteProcedureDelete(bookId, "BooksDelete");
                return RedirectToAction("Books");
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Update book
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BookActionUpdate(int id, string title, DateTime publishedDate, string isbn, int authorId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExecuteProcedureUpdate(title, publishedDate, isbn, authorId, id);
                    return RedirectToAction("Books");
                }

                return View();
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        //code bellow allow you to work with Authors

        public ActionResult Authors()
        {
            try
            {
                List<Author> authorslist = new List<Author>();
                ExecuteProcedureSelectAll(ref authorslist);
                return View(authorslist);
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Get Insert or Update View
        /// </summary>
        /// <param name="authorId">if bookId not null it's Update View</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuthorAction(int? authorId = null)
        {
            try
            {
                if (authorId != null)
                {
                    ViewBag.Controller = "AuthorActionUpdate";
                    ViewBag.Legend = "Change Author";
                    ViewBag.ButtonName = "Save";
                    Author author = ExecuteProcedureSelectWhereAuthor(authorId);
                    return View(author);
                }
                else
                {
                    ViewBag.Controller = "AuthorActionInsert";
                    ViewBag.Legend = "Add Author";
                    ViewBag.ButtonName = "Add";
                    return View();
                }
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Insert author
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuthorActionInsert(string fName, string lName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExecuteProcedureInsert(fName, lName);
                    return RedirectToAction("Authors");
                }

                return View();
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Delete author
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        public ActionResult AuthorActionDelete(int authorId)
        {
            try
            {
                ExecuteProcedureDelete(authorId, "AuthorsDelete");
                return RedirectToAction("Authors");
            }

            catch (Exception ex)
            {
                return View("Error");
            }
        }

        /// <summary>
        /// Action is Update author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AuthorActionUpdate(int id, string fName, string lName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExecuteProcedureUpdate(fName, lName, id);
                    return RedirectToAction("Authors");
                }

                return View();
            }

            catch (Exception ex)
            {
                return View("Error");
            }

        }

        //code bellow allow you to execute choosen stored procedure from local Database "WebLibraryDB"

        /// <summary>
        /// Execute Select All stored procedure for Books Table
        /// </summary>
        /// <param name="books"></param>
        private void ExecuteProcedureSelectAll(ref List<Book> books)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BooksSelectAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        DateTime? pubdate;

                        if (DBNull.Value.Equals(sdr[2]))
                            pubdate = null;
                        else
                            pubdate = (DateTime)sdr[2];

                        books.Add(new Book()
                        {
                            ID = (int)sdr[0],
                            Title = (string)sdr[1],
                            PublishedDate = (DateTime)sdr[2],
                            ISBN = (string)sdr[3],
                            AuthorId = (int)sdr[4],
                            FName = (string)sdr[5],
                            LName = (string)sdr[6]
                        });
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("My Error:" + ex);
                }
            }
        }

        /// <summary>
        /// Execute Select All stored procedure for Authors Table
        /// </summary>
        /// <param name="authors"></param>
        private void ExecuteProcedureSelectAll(ref List<Author> authors)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AuthorsSelectAll", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        authors.Add(new Author()
                        {
                            ID = (int)sdr[0],
                            FName = (string)sdr[1],
                            LName = (string)sdr[2]
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Select Where stored procedure for Books Table
        /// </summary>
        /// <param name="book"></param>
        private Book ExecuteProcedureSelectWhereBook(int? bookId)
        {
            Book book = new Book();
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BooksSelectWhere", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", bookId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        DateTime? pubdate;

                        if (DBNull.Value.Equals(sdr[2]))
                            pubdate = null;
                        else
                            pubdate = (DateTime)sdr[2];

                        book.ID = (int)sdr[0];
                        book.Title = (string)sdr[1];
                        book.PublishedDate = (DateTime)sdr[2];
                        book.ISBN = (string)sdr[3];
                        book.AuthorId = (int)sdr[4];
                        book.FName = (string)sdr[5];
                        book.LName = (string)sdr[6];
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("My Error:" + ex);
                }
            }
            return book;
        }

        /// <summary>
        /// Execute Select Where stored procedure for Authors Table
        /// </summary>
        /// <param name="authorId"></param>
        /// <returns></returns>
        private Author ExecuteProcedureSelectWhereAuthor(int? authorId)
        {
            Author author = new Author();
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AuthorsSelectWhere", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", authorId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        author.ID = (int)sdr[0];
                        author.FName = (string)sdr[1];
                        author.LName = (string)sdr[2];
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("My Error:" + ex);
                }
            }
            return author;
        }

        /// <summary>
        /// Execute Insert stored procedure for Authors Table
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        private void ExecuteProcedureInsert(string fName, string lName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AuthorsInsert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fName", fName);
                    cmd.Parameters.AddWithValue("@lName", lName);
                    SqlDataReader sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Insert stored procedure for Books Table
        /// </summary>
        /// <param name="title"></param>
        /// <param name="publishedDate"></param>
        /// <param name="isbn"></param>
        /// <param name="authorId"></param>
        private void ExecuteProcedureInsert(string title, DateTime publishedDate, string isbn, int authorId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BooksInsert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@publishedDate", publishedDate);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@authorId", authorId);
                    SqlDataReader sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Update stored procedure for Authors Table
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <param name="id"></param>
        private void ExecuteProcedureUpdate(string fName, string lName, int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("AuthorsUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fName", fName);
                    cmd.Parameters.AddWithValue("@lName", lName);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Update stored procedure for Books Table
        /// </summary>
        /// <param name="title"></param>
        /// <param name="publishedDate"></param>
        /// <param name="isbn"></param>
        /// <param name="authorId"></param>
        /// <param name="id"></param>
        private void ExecuteProcedureUpdate(string title, DateTime publishedDate, string isbn, int authorId, int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("BooksUpdate", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@publishedDate", publishedDate);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@authorId", authorId);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Delete stored procedure for Authors and Books tables
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deleteProcedureName"></param>
        private void ExecuteProcedureDelete(int id, string deleteProcedureName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WebLibraryDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(deleteProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
            }
        }
    }
}