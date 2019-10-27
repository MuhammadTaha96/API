using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ValuesController : ApiController
    {
        SmartLibraryContext db = new SmartLibraryContext();
        // GET api/values
        public object GetAllBooks()
        {
            var q = (from b in db.Books
                     select new
                     {
                         b.BookId,
                         b.Title,
                         b.Description,
                         b.ISBN_No,
                         b.IsAvailable,
                         b.IsDeleted,
                         b.Language,
                         b.PublishedYear,
                         b.Category,
                         b.Reviews,
                         b.Publisher,
                         b.Author,
                         b.ImagePath,
                         b.Copies
                     }).OrderBy(x => x.BookId).ToList();

            return q;
        }

        public object GetCopies()
        {
            var q = (from c in db.Copies
                     select new
                     {
                         c.CopyId,
                         c.Location,
                         c.Status,
                         c.Book
                     }).OrderBy(x => x.CopyId).ToList();

            return q;
        }

        public object GetAllAuthors()
        {
            var authorList = db.Authors.ToList();
            return authorList;
        }

        public object GetBookById(int id)
        {
            var book = db.Books.Where(x => x.BookId == id).SingleOrDefault();
            return book;
        }


        //public object GetAllCategories()
        //{
        //    var categories = db.Categories.ToList();
        //    return categories;
        //}


        // GET api/values/5
        public Book Get(int id)
        {
            return db.Books.Where(x => x.BookId == id).SingleOrDefault();

        }

        [HttpGet]
        public object ValidateUser(string username, string password)
        {
            var user = (from ul in db.UserLogins
                        where ul.UserName == username && ul.Password == password
                        select new
                        {
                            ul.UserName,
                            ul.FullName,
                            ul.Email,
                            ul.IsActive,
                            ul.PhoneNumber,
                            ul.RFID,
                            ul.UserType,
                            ul.UserLoginId
                        }).SingleOrDefault();

            return user;
        }

        [HttpGet]
        public bool ReserveACopy(int bookId, int userLoginId)
        {
            try
            {
                Reservation res = new Reservation();
                res.ReservedBy = db.UserLogins.Where(x => x.UserLoginId == userLoginId).SingleOrDefault();
                res.ReservedCopy = db.Copies.Where(x => x.Book.BookId == bookId && x.Status.Name == "Available").FirstOrDefault();
                res.ReservedCopy.Status = db.Status.Where(x => x.Name == "Reserved").SingleOrDefault();

                res.StartDateTime = DateTime.Now;
                res.EndDateTime = DateTime.Today.AddDays(1);

                db.Reservations.Add(res);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        public bool ChangePassword(string username, string password)
        {
            bool success = false;
            try
            {
                UserLogin user = db.UserLogins.Where(x => x.UserName == username).SingleOrDefault();
                user.Password = password;
                db.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                return success;
            }

            return success;

        }

        [HttpGet]
        public object GetComments(string bookId)
        {

            var commentList = (
                               from c in db.Comments
                               select new
                               {
                                   c.CommentId,
                                   c.Content,
                                   c.Rating,
                                   c.Commenter,
                                   c.Book
                               }

                               ).ToList();

            return commentList;

        }


        [HttpGet]
        public object GetReviews(string bookId)
        {

            var commentList = (
                               from r in db.Reviews
                               select new
                               {
                                   r.ReviewId,
                                   r.Reviewer,
                                   r.Content,
                                   r.Book,
                               }

                               ).ToList();

            return commentList;

        }




        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
