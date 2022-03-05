using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _context;
        public PalestrantePersist(ProEventosContext context)
        {
            _context = context;


        }
       
        public async Task<Palestrante> GetAllPalestrantesByIdAsync(int palestranteId, bool includeEventos)
        {
              IQueryable<Palestrante> query =_context.Palestrantes              
                .Include(p => p.RedesSociais); // usando include para colocar redes sociais 

                if(includeEventos){ // se for true entra nessa linha de código 
                    query=query
                        .Include(p => p.PalestranteEventos)
                        .ThenInclude(pe => pe.Evento); 
                }
            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Id ==palestranteId);
                        
            return await query.FirstOrDefaultAsync(); // retorna um array     
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos=false)
        {
            IQueryable<Palestrante> query =_context.Palestrantes              
                .Include(p => p.RedesSociais); // usando include para colocar redes sociais 

                if(includeEventos){ // se for true entra nessa linha de código 
                    query=query
                        .Include(p => p.PalestranteEventos)
                        .ThenInclude(pe => pe.Evento); 
                }
            query = query.OrderBy(p => p.Id)
                         .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));
                        
            return await query.ToArrayAsync(); // retorna um array     
        }

       

        public async Task<Palestrante[]> GetPalestrantesByAsync(bool includeEventos =false) // busca por todos os palestrantes
        {        
        
            IQueryable<Palestrante> query =_context.Palestrantes              
                .Include(p => p.RedesSociais); // usando include para colocar redes sociais 

                if(includeEventos){ // se for true entra nessa linha de código 
                    query=query
                        .Include(p => p.PalestranteEventos)
                        .ThenInclude(pe => pe.Evento); 
                }
            query = query.OrderBy(p=> p.Id);
                        
            return await query.ToArrayAsync(); // retorna um array     
        }

      
    }
}