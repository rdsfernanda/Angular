using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
            //PALESTRANTES 

          Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos);
         Task<Palestrante[]> GetPalestrantesByAsync(bool includeEventos);
         Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteid,bool includeEventos);
    }
}