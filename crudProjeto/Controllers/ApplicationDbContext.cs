
namespace crudProjeto.Controllers
{
    internal class ApplicationDbContext
    {
        public object Clientes { get; internal set; }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}