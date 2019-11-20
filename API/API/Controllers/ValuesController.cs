﻿using API.Operations;
using Models;
using Newtonsoft.Json;
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
                     where b.IsDeleted.Equals(false)
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
                         b.Authors,
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
            var authorList = db.Authors.OrderBy(x => x.Name).ToList();
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
        public object ValidateLibrarian(string username, string password)
        {
            var user = (from ul in db.UserLogins
                        where ul.UserName == username && ul.Password == password && ul.UserType.Name.Equals("librarian")
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
        public object ValidateUserLogin(string username, string password)
        {
            UserLogin user = new UserLogin();

            var userNameExist = db.UserLogins.Where(x => x.UserName == username).SingleOrDefault();
            var validUser = db.UserLogins.Where(x => x.UserName == username && x.Password == password).SingleOrDefault();

            if (userNameExist != null && validUser != null)
            {
                var userR = (from ul in db.UserLogins
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
                                 ul.UserLoginId,
                                 ul.ValidationErrorMessage
                             }).SingleOrDefault();
                return userR;
            }
            else if (userNameExist != null && validUser == null)
            {
                user.ValidationErrorMessage = "Please enter correct password";
            }
            else if (userNameExist == null)
            {
                user.ValidationErrorMessage = "Please enter correct username";
            }

            return user;
        }

        [HttpGet]
        public bool ReserveACopy(int bookId, int userLoginId)
        {
            try
            {
                Book book = db.Books.Where(x => x.BookId == bookId).SingleOrDefault();
                Reservation res = new Reservation();
                res.ReservedBy = db.UserLogins.Where(x => x.UserLoginId == userLoginId).SingleOrDefault();
                res.ReservedCopy = db.Copies.Where(x => x.Book.BookId == bookId && x.Status.Name == "Available").FirstOrDefault();
                res.ReservedCopy.Status = db.Status.Where(x => x.Name == "Reserved").SingleOrDefault();

                res.StartDateTime = DateTime.Now;
                res.EndDateTime = DateTime.Today.AddDays(1);
               Notification.SMS("ReserverACopy", res.ReservedBy, book, res);

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
            int bId = int.Parse(bookId);
            var commentList = (
                               from c in db.Comments
                               where c.Book.BookId.Equals(bId)
                               select new
                               {
                                   c.CommentId,
                                   c.Content,
                                   c.Rating,
                                   c.Commenter,
                                   c.Book,
                                   c.Date
                               }

                               ).ToList();

            return commentList;

        }


        [HttpGet]
        public object GetReviews(string bookId)
        {
            int bId = int.Parse(bookId);
            var commentList = (
                               from r in db.Reviews
                               where r.Book.BookId.Equals(bId)
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

        [HttpGet]
        public bool AddComment(int bookId, string commentText, int userLoginId)
        {
            try
            {
                UserLogin user = db.UserLogins.Where(x => x.UserLoginId == userLoginId).SingleOrDefault();
                Book book = db.Books.Where(x => x.BookId == bookId).SingleOrDefault();

                Comment comment = new Comment();
                comment.Commenter = user;
                comment.Book = book;
                comment.Content = commentText;
                comment.Date = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpGet]
        public object GetAvailableCopies(int bookId)
        {
            //   List<Copy> availableCopies = db.Copies.Where(x => x.Book.BookId == bookId && x.Status.Name.ToLower() == "available").ToList();
            var availableCopies = (
                             from c in db.Copies
                             where c.Book.BookId.Equals(bookId) && c.Status.Name.ToLower().Equals("available")
                             select new
                             {
                                 c.CopyId,
                                 c.RFID,
                                 c.Book,
                                 c.Location,
                                 c.Status
                             }

                             ).ToList();

            return availableCopies;
        }


        [HttpPost]
        public bool AddBook(Book book)
        {
            List<Author> authorList = new List<Author>();

            book.Publisher = db.Publishers.Where(x => x.PublisherId.Equals(book.Publisher.PublisherId)).SingleOrDefault();
            book.Category = db.Categories.Where(x => x.CategoryId.Equals(book.Category.CategoryId)).SingleOrDefault();

            foreach (var author in book.Authors)
            {
                Author auth = new Author();
                auth = db.Authors.Where(x => x.Name == author.Name).SingleOrDefault();
                authorList.Add(auth);
            }
            book.Authors = authorList;

            db.Books.Add(book);
            db.SaveChanges();

            return true;
        }

         [HttpPost]
        public bool UpdateBook(Book book)
        {
            try
            {
                Book bookDetails = db.Books.Where(x => x.BookId == book.BookId).SingleOrDefault();

                bookDetails.Publisher = db.Publishers.Where(x => x.PublisherId.Equals(book.Publisher.PublisherId)).SingleOrDefault();
                bookDetails.Category = db.Categories.Where(x => x.CategoryId.Equals(book.Category.CategoryId)).SingleOrDefault();
                bookDetails.Description = book.Description;
                bookDetails.ISBN_No = book.ISBN_No;
                bookDetails.PublishedYear = book.PublishedYear;
                bookDetails.Language = book.Language;

                db.SaveChanges();
                return true;
            }
             catch(Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public bool DeleteBook(int bookId)
        {
            try
            {
                Book bookDetails = db.Books.Where(x => x.BookId == bookId).SingleOrDefault();
                bookDetails.IsDeleted = true;

                db.SaveChanges();
                return true;
            }
             catch(Exception ex)
            {
                return false;
            }
        }
   
        [HttpPost]
        public bool AddAuthor(Author author)
        {

            try
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        [HttpPost]
        public bool AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public bool AddPublisher(Publisher publisher)
        {
            try
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }



        [HttpPost]
        public bool AddCopy(Copy copy)
        {
            try
            {
                copy.Book = db.Books.Where(x => x.BookId.Equals(copy.Book.BookId)).SingleOrDefault();
                copy.Status = db.Status.Where(x => x.Name.Equals(copy.Status.Name)).SingleOrDefault();
                copy.Location = db.Locations.Where(x => x.Shelf.Equals(copy.Location.Shelf) && x.ShelfRow.Equals(copy.Location.ShelfRow) && x.ShelfCol.Equals(copy.Location.ShelfCol)).SingleOrDefault();

                db.Copies.Add(copy);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public object GetCategories()
        {
            List<Category> categoryList = db.Categories.OrderBy(x => x.Name).ToList();
            return categoryList;
        }

        [HttpGet]
        public object GetPublishers()
        {
            List<Publisher> categoryList = db.Publishers.OrderBy(x => x.Name).ToList();
            return categoryList;
        }

        [HttpGet]
        public object GetAvailableLocation()
        {
            var availableLocations = (
                           from l in db.Locations
                           where !(from c in db.Copies
                                   select c.Location.LocationId)
        .Contains(l.LocationId)
                           select l).ToList();

            return availableLocations;
        }

        [HttpGet]
        public object GetBookDetails(int bookId)
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
                         b.Authors,
                         b.ImagePath,
                         b.Copies
                     }).Where(x => x.BookId.Equals(bookId)).SingleOrDefault();

            return q;
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
