﻿using F1TimeTrialTrack.Data;
using F1TimeTrialTrack.Entities.Dtos.Tracks;
using F1TimeTrialTrack.Entities.Entity_Models;
using F1TimeTrialTrack.Logic.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1TimeTrialTrack.Logic.Logic
{
    public class TracksLogic
    {
        Repository<Tracks> repo;
        DtoProvider dtoProvider;
        public TracksLogic(Repository<Tracks> repo, DtoProvider dtoProvider)
        {
            this.repo = repo;
            this.dtoProvider = dtoProvider;
        }
        public void AddTrack(TracksCreateUpdateDto track)
        {
            Tracks t=dtoProvider.Mapper.Map<Tracks>(track);
            if (repo.GetAll().FirstOrDefault(x=>x.Name==t.Name)==null)
            {
                repo.Create(t);
            }
            else
            {
                    throw new ArgumentException("Ilyen nevű pálya már létezik az adatbázisban, válassz másikat!");
            }
        }
        public IEnumerable<TrackShortViewDto> GetAllTracks()
        {
            return repo.GetAll()
                .Select(x=>dtoProvider.Mapper.Map<TrackShortViewDto>(x));
        }
        public void DeleteTrack(string id)
        {
            repo.DeleteById(id);
        }
        public void UpdateTrack(string id, TracksCreateUpdateDto track)
        {
            var model = repo.FindById(id);
            dtoProvider.Mapper.Map(track, model);
            repo.Update(model);
        }
        public TrackViewDto GetTrack(string id)
        {
            var model = repo.FindById(id);
            return dtoProvider.Mapper.Map<TrackViewDto>(model);
        }
        public string Upload(IFormFile file)
        {
            //extension check
            List<string> valide = new List<string>() { ".jpg", ".png", ".gif", ".jpeg" };
            var extension = Path.GetExtension(file.FileName);
            if (!valide.Contains(extension))
            {
                return $"Fájlformátum nem megfelelő!!({string.Join(',', valide)})";
            }

            //size check
            if (file.Length > 10 * 1024 * 1024)
            {
                return "Fájl mérete túl nagy!(nagyobb mint 10Mb)";
            }
            string fileName = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            using FileStream fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }

    }
}
