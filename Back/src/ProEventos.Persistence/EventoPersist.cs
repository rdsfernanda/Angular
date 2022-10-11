using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _context;
        public EventoPersist(ProEventosContext context)
        {
            _context = context;
           // _context.ChangeTracker.QueryTrackingBehavior=QueryTrackingBehavior.NoTracking;


        }
      
        public async Task<Evento[]> GetEventosByAsync(bool includePalestrantes =false)// = false significa que é opcional
        {
           IQueryable<Evento> query =_context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais); // usando include para colocar lotes e redes sociais dos eventos 

                if(includePalestrantes){ // se for true entra nessa linha de código 
                    query=query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante); 
                }

                query = query.AsNoTracking().OrderBy(e=> e.Id); // ordena por ID 
                return await query.ToArrayAsync(); // retorna um array    
        }

        public async Task<Evento> GetAllEventosByIdAsync(int eventoId, bool includePalestrantes =false)
        {
             IQueryable<Evento> query =_context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais); // usando include para colocar lotes e redes sociais dos eventos 

                if(includePalestrantes){ // se for true entra nessa linha de código 
                    query=query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante); 
                }

            query = query.AsNoTracking().OrderBy(e=> e.Id)
                         .Where(e => e.Id==eventoId); // filtra pelo id do wvento
            return await query.FirstOrDefaultAsync();
        }
      

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query =_context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais); // usando include para colocar lotes e redes sociais dos eventos 

                if(includePalestrantes){ // se for true entra nessa linha de código 
                    query=query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante); 
                }

            query = query.AsNoTracking().OrderBy(e=> e.Id)
                         .Where(e => e.Tema.ToLower().Contains(tema.ToLower())); /// ordena por tema (contains verifica se contém o tema) 
            return await query.ToArrayAsync(); // retorna um array     
        }

       

       

       

        
      
    }
}