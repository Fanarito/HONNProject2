using System;
using System.Linq;
using VideotapeGalore.Repositories;
using VideotapeGalore.Services.Exceptions;
using VideotapeGalore.Tests.DataFakers;
using Xunit;

namespace VideotapeGalore.Tests.Services
{
    public class CrudServiceTests
    {
        private readonly ApplicationContext _context;

        public CrudServiceTests()
        {
            _context = BogusContext.SetupDb();
        }

        [Fact]
        public async void Create_ShouldCreate()
        {
            var service = new CrudServiceImpl(_context);
            service.Create(BogusDataGenerators.GetTape());
            var tapes = await service.GetAll();
            Assert.Equal(11, tapes.Count());
        }

        [Fact]
        public async void Update_ShouldUpdate()
        {
            var service = new CrudServiceImpl(_context);
            service.Create(BogusDataGenerators.GetTape());

            var tape = await service.GetSingle(1);
            tape.Title = "Updated title";
            service.Update(tape);
            tape = await service.GetSingle(1);
            Assert.Equal("Updated title", tape.Title);
        }

        [Fact]
        public async void Remove_ShouldRemove()
        {
            var service = new CrudServiceImpl(_context);
            var tapes = await service.GetAll();
            Assert.Equal(10, tapes.Count());
            service.Delete(await service.GetSingle(1));
            tapes = await service.GetAll();
            Assert.Equal(9, tapes.Count());
        }

        [Fact]
        public async void GetSingle_ShouldThrowNotFound()
        {
            var service = new CrudServiceImpl(_context);
            await Assert.ThrowsAsync<NotFoundException>(() => service.GetSingle(int.MaxValue));
        }

        [Fact]
        public async void GetAll_ShouldGetAll()
        {
            var service = new CrudServiceImpl(_context);
            var tapes = await service.GetAll();
            Assert.Equal(10, tapes.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}