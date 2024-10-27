using Biblioteka.DatabContext;
using Biblioteka.Interfaces;
using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteka.Services
{
    public class RentalService(BiblioApiDB context) : IRentalService
    {
        readonly BiblioApiDB _context = context;

        public async Task RentBookById(int id, int readerId, int rentalTime)
        {
            var checkBook = await _context.Book.FirstOrDefaultAsync(b => b.Id_Book == id);
            var checkReader = await _context.Reader.FirstOrDefaultAsync(r => r.Id_Reader == readerId);
            //var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkBook.Id_Book);
            var rental = new Rental()
            {
                Id_Book = checkBook.Id_Book,
                Id_Reader = checkReader.Id_Reader,
                Rental_Start = DateTime.Now,
                Rental_Time = rentalTime,
                Rental_End = DateTime.Now.AddDays(rentalTime),
                Rental_Status = "нет"
            };
            await _context.Rental.AddAsync(rental);
            //bookExemplar.Books_Count -= 1;
            await _context.SaveChangesAsync();
        }

        public async Task ReturnRent(int rentId)
        {
            var checkRent = await _context.Rental.FirstOrDefaultAsync(r => r.id_Rent == rentId);
            //var bookExemplar = await _context.BookExemplar.FirstOrDefaultAsync(e => e.Book_Id == checkRent.Id_Book);
            checkRent.Rental_Status = "да";
            //bookExemplar.Books_Count += 1;
            await _context.SaveChangesAsync();
        }
        public List<Rental> GetBookRentals(int id)
        {
            return _context.Rental.Where(h => h.Id_Book == id).Include(h => h.Book).ToList();
        }

        public List<Rental> GetCurrentRentals()
        {
            return _context.Rental.Where(h => h.Rental_Status == "нет").Include(h => h.Book).Include(h => h.Reader).ToList();
        }

        public List<Rental> GetReadersRentals(int id)
        {

            return _context.Rental.Where(r => r.Id_Reader == id).Include(h => h.Book).Include(h => h.Reader).ToList();
        }
        public bool BookInRentExists(int bookId)
        {
            return _context.Rental.Any(r => r.Id_Book == bookId);
        }
        public bool RentExists(int id)
        {
            return _context.Rental.Any(r => r.id_Rent == id);
        }
        public bool ReaderInRent(int id)
        {
            return _context.Rental.Any(r => r.Id_Reader == id);
        }
    }
}