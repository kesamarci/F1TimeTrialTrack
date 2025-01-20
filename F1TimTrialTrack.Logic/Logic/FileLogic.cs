using F1TimeTrialTrack.Data;
using F1TimeTrialTrack.Entities.Entity_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F1TimeTrialTrack.Logic.Logic
{
    public class FileLogic
    {
        private readonly F1Context _context;
        private readonly string _storagePath;

        public FileLogic(F1Context context)
        {
            _context = context;
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<UploadedFile> UploadFileAsync(IFormFile file, string trackName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is invalid.");

            var filePath = Path.Combine("UploadedFiles", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            if (!File.Exists(filePath))
                throw new IOException("File saving failed.");


            var uploadedFile = new UploadedFile
            {
                FileName = file.FileName,
                FilePath = filePath,
                TrackName = trackName,
            };

            _context.UploadedFiles.Add(uploadedFile);
            await _context.SaveChangesAsync();

            return uploadedFile;
        }

        public IEnumerable<UploadedFile> GetFilesByTrack(string trackName)
        {
            return _context.UploadedFiles.Where(f => f.TrackName == trackName).ToList();
        }
    }
}

