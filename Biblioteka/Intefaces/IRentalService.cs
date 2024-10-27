using Biblioteka.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteka.Interfaces
{
    public interface IRentalService
    {
        public Task RentBookById(int id, int readerId, int rentalTime);
        public List<Rental> GetReadersRentals(int id);
        public Task ReturnRent(int rentId);
        public List<Rental> GetCurrentRentals();

        public List<Rental> GetBookRentals(int id);
        public bool BookInRentExists(int bookId);
        public bool RentExists(int id);
        public bool ReaderInRent(int id);
    }
}