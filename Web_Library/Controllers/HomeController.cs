using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        //code bellow allow you to execute the choosen stored procedure from local Database "WebLibraryDB"

        /// <summary>
        /// Execute Select stored procedure for Books Table
        /// </summary>
        /// <param name="books"></param>
        private void ExecuteProcedureSelect(ref List<Book> books)
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
                        books.Add(new Book()
                        {
                            ID = (int)sdr[0],
                            Title = (string)sdr[1],
                            PublishedDate = (DateTime)sdr[2],
                            ISBN = (string)sdr[3],
                            AuthorName = (string)sdr[4]
                        });
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Execute Select stored procedure for Authors Table
        /// </summary>
        /// <param name="authors"></param>
        private void ExecuteProcedureSelect(ref List<Author> authors)
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
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(deleteProcedureName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}