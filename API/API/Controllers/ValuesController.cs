using API.Operations;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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


        public object GetUsers(string userType)
        {
            if (userType.Equals("all"))
            {
                var q = (from u in db.UserLogins
                         where u.IsDeleted.Equals(false)
                         select new
                         {
                             u.FullName,
                             u.Email,
                             u.PhoneNumber,
                             u.RFID,
                             u.UserName,
                             u.UserType,
                             u.IsActive,
                             u.UserLoginId,
                             u.IsDeleted
                         }).OrderBy(x => x.FullName).ToList();

                return q;
            }
            else
            {

                var q = (from u in db.UserLogins
                         where u.UserType.Name.Equals(userType) && u.IsDeleted.Equals(false)
                         select new
                         {
                             u.FullName,
                             u.Email,
                             u.PhoneNumber,
                             u.RFID,
                             u.UserName,
                             u.UserType,
                             u.IsActive,
                             u.UserLoginId,
                             u.IsDeleted
                         }).OrderBy(x => x.FullName).ToList();

                return q;
            }
        }

        public object GetReservations()
        {
            var q = (from r in db.Reservations
                     select new
                     {
                         r.ReservationId,
                         r.ReservedCopy,
                         r.ReservedBy,
                         r.StartDateTime,
                         r.EndDateTime,
                         r.ReservedCopy.Book,
                         r.Status
                         
                     }).OrderBy(x => x.ReservationId).ToList();

            return q;
        }

        public object GetTransactions()
        {
            var q = (from t in db.Transactions
                     select new
                     {
                         t.TransactionId,
                         t.CheckInDate,
                         t.CheckOutDate,
                         t.ExpectedReturnDate,
                         t.Fine,
                         t.DaysKept,
                         t.Copy,
                         t.Copy.Book,
                         t.User,
                         t.Reservation,
                         t.Type
                     }).OrderBy(x => x.TransactionId).ToList();

            return q;
        }

        public object GetActiveReservationsByUser(int userLoginId)
        {
            var q = (from r in db.Reservations
                     where r.ReservedBy.UserLoginId.Equals(userLoginId) && r.Status.Name.Equals("Active")
                     select new
                     {
                         r.ReservationId,
                         r.ReservedCopy,
                         r.ReservedBy,
                         r.StartDateTime,
                         r.EndDateTime,
                         r.ReservedCopy.Book,
                         r.Status

                     }).OrderBy(x => x.ReservationId).ToList();

            return q;
        }

        public object GetUserTypes()
        {
            var q = (from u in db.UserTypes
                     select new
                     {
                         u.UserTypeId,
                         u.Name,
                         u.Description
                     }).OrderBy(x => x.Name).ToList();

            return q;
        }

        public object GetElectronicTypes()
        {
            var q = (from u in db.ElectronicFileTypes
                     select new
                     {
                         u.ElectronicFileTypeId,
                         u.Name,
                     }).OrderBy(x => x.Name).ToList();

            return q;
        }

        public object GetElectronicFiles()
        {
            var q = (from u in db.ElectronicFiles
                     select new
                     {
                        u.ElectronicFileId,
                        u.FileName,
                        u.FileType,
                        u.Path
                     }).OrderBy(x => x.FileName).ToList();

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
            var validUser = db.UserLogins.Where(x => x.UserName == username && x.Password == password && x.IsActive.Equals(true) && x.IsDeleted.Equals(false)).SingleOrDefault();

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
        public Reservation ReserveACopy(int bookId, int userLoginId)
        {
            try
            {
                Book book = db.Books.Where(x => x.BookId == bookId).SingleOrDefault();
                Reservation res = new Reservation();
                res.ReservedBy = db.UserLogins.Where(x => x.UserLoginId == userLoginId).SingleOrDefault();
                res.ReservedCopy = db.Copies.Where(x => x.Book.BookId == bookId && x.Status.Name == "Available").FirstOrDefault();
                res.ReservedCopy.Status = db.Status.Where(x => x.Name == "Reserved").SingleOrDefault();
                res.Status = db.ReservationStatus.Where(x => x.Name.Equals("Active")).SingleOrDefault();
                res.StartDateTime = DateTime.Now;
                res.EndDateTime = DateTime.Today.AddDays(1);
                 Notification.SendTeleSignSMS("ReserverACopy", res.ReservedBy, book, res);

                db.Reservations.Add(res);
                db.SaveChanges();
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }

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
        public bool AddElectronicFile(ElectronicFile efile)
        {
            try
            {

                efile.FileType = db.ElectronicFileTypes.Where(x => x.ElectronicFileTypeId.Equals(efile.ElectronicFileId)).SingleOrDefault();

                db.ElectronicFiles.Add(efile);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        [HttpPost]
        public bool AddUser(UserLogin user)
        {
            try
            {
                user.UserType = db.UserTypes.Where(x => x.UserTypeId == user.UserType.UserTypeId).SingleOrDefault();
                db.UserLogins.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool UpdateUser(UserLogin updatedUser)
        {
            try
            {
                UserLogin User = db.UserLogins.Where(x => x.UserLoginId == updatedUser.UserLoginId).SingleOrDefault();
                User.FullName = updatedUser.FullName;
                User.Email = updatedUser.Email;
                User.PhoneNumber = updatedUser.PhoneNumber;
                User.RFID = updatedUser.RFID;
                User.IsActive = updatedUser.IsActive;
                User.Password = string.IsNullOrEmpty(updatedUser.Password) ? User.Password : updatedUser.Password;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public bool CancleReservation(int reservationId, int copyId)
        {
            try
            {
                Reservation reservation = db.Reservations.Where(x => x.ReservationId == reservationId).SingleOrDefault();
                reservation.Status = db.ReservationStatus.Where(x => x.Name.Equals("Cancelled")).SingleOrDefault();
                Copy copy = db.Copies.Where(x => x.CopyId == copyId).SingleOrDefault();
                copy.Status = db.Status.Where(x => x.Name.Equals("Available")).SingleOrDefault();

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        public bool DeleteUser(int userId)
        {
            try
            {
                UserLogin user = db.UserLogins.Where(x => x.UserLoginId == userId).SingleOrDefault();
                user.IsDeleted = true;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
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
                         b.Copies,
                         b.Edition
                     }).Where(x => x.BookId.Equals(bookId)).SingleOrDefault();

            return q;
        }


        [HttpGet]
        public bool UserNameAvailability(string username)
        {
            UserLogin user = db.UserLogins.Where(x => x.UserName.Equals(username)).SingleOrDefault();

            if (user == null)
                return true;
            else
                return false;
        }

        [HttpGet]
        public bool BookNameAvailability(string bookTitle)
        {
            Book book = db.Books.Where(x => x.Title.Equals(bookTitle)).SingleOrDefault();

            if (book == null)
                return true;
            else
                return false;
        }

        [HttpGet]
        public bool EFileNameAvailability(string eFileName)
        {
            ElectronicFile efile = db.ElectronicFiles.Where(x => x.FileName.Equals(eFileName)).SingleOrDefault();

            if (efile == null)
                return true;
            else
                return false;
        }

        [HttpGet]
        public object CheckInCheckOut(string UserRFID, string CopyRFID)
        {
            TransactionResponse response = null;
            try
            {
                UserLogin user = db.UserLogins.Where(x => x.RFID.Equals(UserRFID)).SingleOrDefault();
                Copy copy = db.Copies.Where(x => x.RFID.Equals(CopyRFID)).SingleOrDefault();

                if (user == null)
                {
                    response = new TransactionResponse()
                    {
                        ReponseCode = 003,
                        ResponseMessage = "User not found"
                    };

                    return response;
                }
                else if (copy == null)
                {
                    response = new TransactionResponse()
                    {
                        ReponseCode = 003,
                        ResponseMessage = "Book not found"
                    };

                    return response;
                }

                Reservation resActive = db.Reservations.Where(x => x.ReservedBy.UserLoginId.Equals(user.UserLoginId) && x.ReservedCopy.CopyId.Equals(copy.CopyId) && x.Status.Name.Equals("Active")).SingleOrDefault();
                Reservation resCheckIn = db.Reservations.Where(x => x.ReservedBy.UserLoginId.Equals(user.UserLoginId) && x.ReservedCopy.CopyId.Equals(copy.CopyId) && x.Status.Name.Equals("CheckIn")).SingleOrDefault();
                List<Reservation> resCheckOut = db.Reservations.Where(x => x.ReservedBy.UserLoginId.Equals(user.UserLoginId) && x.ReservedCopy.CopyId.Equals(copy.CopyId) && x.Status.Name.Equals("CheckOut")).ToList();
                UserLoginFine UserFine = db.UserLoginFines.Where(x => x.Defaulter.UserLoginId.Equals(user.UserLoginId)).SingleOrDefault();

                int FinePerDay = int.Parse(ConfigurationManager.AppSettings["FinePerDay"].ToString());
                int overdueDays = 0;


                if (resActive != null) //Check In Operation
                {
                    Transaction trans = new Transaction()
                    {
                        Reservation = resActive,
                        User = db.UserLogins.Where(x => x.RFID.Equals(UserRFID)).SingleOrDefault(),
                        Copy = db.Copies.Where(x => x.RFID.Equals(CopyRFID)).SingleOrDefault(),
                        CheckInDate = DateTime.Now,
                        Fine = 0,
                        DaysKept = 0,
                        ExpectedReturnDate = DateTime.Today.AddDays(3),
                        Type = db.TransactionType.Where(x => x.Name.Equals("CheckIn")).SingleOrDefault()
                    };

                    resActive.Status = db.ReservationStatus.Where(x => x.Name.Equals("Checkin")).SingleOrDefault();
                    copy.Status = db.Status.Where(x => x.Name.Equals("Issued")).SingleOrDefault();

                    db.Transactions.Add(trans);
                    db.SaveChanges();

                    response = new TransactionResponse()
                    {
                        ReponseCode = 200,
                        ResponseMessage = "Book Successfully Checked In. Please make sure to return the book on " + trans.ExpectedReturnDate.Date
                    };
                }

                else if (resCheckIn != null) //Check Out Operation
                {
                    string Message = string.Empty;
                    Transaction TranCheckIn = db.Transactions.Where(x => x.User.RFID.Equals(UserRFID) && x.Copy.RFID.Equals(CopyRFID) && x.Reservation.ReservationId.Equals(resCheckIn.ReservationId)).SingleOrDefault();
                    TranCheckIn.CheckOutDate = DateTime.Now;
                    TranCheckIn.Type = db.TransactionType.Where(x => x.Name.Equals("CheckOut")).SingleOrDefault();
                    TranCheckIn.DaysKept = DateTime.Now.Day - TranCheckIn.CheckInDate.Day; //TranCheckIn.ExpectedReturnDate.Day;
                    overdueDays = (DateTime.Now.Day - TranCheckIn.ExpectedReturnDate.Day) > 0 ? (DateTime.Now.Day - TranCheckIn.ExpectedReturnDate.Day) : 0;
                    TranCheckIn.Fine = overdueDays * FinePerDay;

                    resCheckIn.Status = db.ReservationStatus.Where(x => x.Name.Equals("Checkout")).SingleOrDefault();
                    copy.Status = db.Status.Where(x => x.Name.Equals("Available")).SingleOrDefault();

                   
                    if (TranCheckIn.Fine > 0)
                    {
                        Message = string.Format("You Kept this book for {0} day(s) which is {1} day(s) more than expected. Please say a fine of Ruppees {2} to the Librarian.", TranCheckIn.DaysKept, overdueDays, TranCheckIn.Fine);

                        if (UserFine != null)
                        {
                            UserFine.Amount += TranCheckIn.Fine;
                        }
                        else
                        {
                            UserLoginFine firstFine = new UserLoginFine()
                            {
                                Amount = TranCheckIn.Fine,
                                Defaulter = user
                            };

                            db.UserLoginFines.Add(firstFine);
                        }
                    }
                    else
                    {
                        Message = "Book Successfully Checked Out";
                    }

                    response = new TransactionResponse()
                   {
                       ReponseCode = 200,
                       ResponseMessage = Message,
                       Fine = TranCheckIn.Fine
                   };

                    db.SaveChanges();

                }
                else if (resCheckOut.Count > 0) //User is trying to check out the issued a book again
                {
                    response = new TransactionResponse()
                    {
                        ReponseCode = 200,
                        ResponseMessage = "You have already checked out this book. Please return it to shelve if not yet returned"
                    };
                }
                else //User has not reserved a book.
                {
                    response = new TransactionResponse()
                    {
                        ReponseCode = 200,
                        ResponseMessage = "Please reserve this book first then try to check in"
                    };
                }
            }
            catch (Exception ex)
            {
                response = new TransactionResponse()
               {
                   ReponseCode = 002,
                   ResponseMessage = "Something went wrong"
               };
            }

            return response;
        }

        [HttpGet]
        public object GetEletronicFiles(int fileType)
        {
            List<ElectronicFile> efiles = db.ElectronicFiles.Where(x => x.FileType.ElectronicFileTypeId.Equals(fileType)).ToList();
            return efiles;
        }


        //[HttpGet]
        //public object AuthenticateStudentRFID(string rfid)
        //{
        // //   UserLogin user = db.UserLogins.Where(x => x.RFID.Equals(rfid)).SingleOrDefault();
        ////    List<Reservation> reservationList = db.Reservations.Where(x => x.ReservedBy.UserLoginId.Equals(user.UserLoginId) && x.is .Equals(false) && x.ReservedCopy != null).ToList();
        //    //List<Copy> copies = db.Copies.Where(x=>x.CopyId.Equals(reser))

        ////    return user;
        //}

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
