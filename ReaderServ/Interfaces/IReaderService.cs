using Microsoft.AspNetCore.Mvc;
using ReaderServ.Model;
using ReaderServ.Requests;
using ReaderServ.Services;
using static ReaderServ.Services.ReaderService; // Import for PaginationParams and ReaderSearchFilter

namespace ReaderServ.Interfaces
{
    public interface IReaderService
    {
        public List<Reader> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize);
        public Task AddNewReader(createReader reader);
        public Reader GetReaderById(int id);
        public Task UpdateReaderById(int id, createReader reader);
        public Task DeleteReaderById(int id);
        //public List<RentHistory> GetReadersRentals(int id);
        public bool ReaderExists(string login);
        public List<Reader> GetAll();

    }
}