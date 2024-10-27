using Biblioteka.Model;
using Biblioteka.Requests;
using Biblioteka.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Biblioteka.Services.ReaderService;

namespace Biblioteka.Interfaces
{
    public interface IReaderService
    {
        public List<Reader> GetAllReaders([FromQuery] int? page, [FromQuery] int? pageSize);
        public Task AddNewReader(createReader reader);
        public Reader GetReaderById(int id);
        public Task UpdateReaderById(int id, createReader reader);
        public Task DeleteReaderById(int id);
        public List<Rental> GetReadersRentals(int id);
        public bool ReaderExists(string login);
        public List<Reader> GetAll();

    }
}