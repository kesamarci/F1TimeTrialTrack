using F1TimeTrialTrack.Data;
using F1TimeTrialTrack.Entities.Dtos.TTs;
using F1TimeTrialTrack.Entities.Entity_Models;
using F1TimeTrialTrack.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1TimeTrialTrack.Logic.Logic
{
    public class TTsLogic
    {
        Repository<TTs> repo;
        DtoProvider dtoProvider;
        public TTsLogic(Repository<TTs> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }
        public void AddTTs(TTsCCreateUpdateDto dto)
        {
            TTs ts=dtoProvider.Mapper.Map<TTs>(dto);
            if (repo.GetAll().FirstOrDefault(x=>x.Id==ts.Id)==null)
            {
                repo.Create(ts);
            }
            else
            {
                throw new ArgumentException("Te már csak Updatelni tudsz, egy fiók csak egy TT idővel rendelkezhet");
            }
            
        }
        public TTsViewDto GetTTs(string id)
        {
            var ts = repo.FindById(id);
            return dtoProvider.Mapper.Map<TTsViewDto>(ts);
        }
        public void DeleteTTs(string id)
        {
            repo.DeleteById(id);
        }
    }
}
