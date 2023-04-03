﻿using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;

namespace WorkshopGroup.Repository
{
    public class ClubRepository : IClubRepository
  {
    private readonly ApplicationDbContext _context;
    public ClubRepository(ApplicationDbContext context){
      _context = context;
    }
    public bool Add(Club club)
    {
      _context.Add(club);
      return Save();
    }
    public bool Delete(Club club)
    {
      _context.Remove(club);
      return Save();
    }
    public async Task<IEnumerable<Club>> GetAll()
    {
       return await _context.Clubs.ToListAsync();
    }
    public async Task<Club> GetByIdAsync(int id)
    {
      //orig// return await _context.Clubs.FirstOrDefaultAsync(i => i.Id == id);
      // When using NAVIGATION properties, one-one one-many; must .include() to bring it in.
      return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Club> GetByIdAsyncNoTracking(int id)
    {
      return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Club>> GetClubByCity(string city)
    {
      return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync();
    }
    public bool Save()
    {
      var saved = _context.SaveChanges();
      return saved > 0 ? true : false;
    }
    public bool Save(Club club)
    {
      throw new NotImplementedException();
    }


    public bool Update(Club club)
    {
      _context.Update(club);
      return Save();
    }
  }
}
