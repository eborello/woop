using SpoilBlock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpoilBlock.DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace SpoilBlock.DAL.Concrete
{
    public class WoopUserMediumRepository : Repository<WoopuserMedium>, IWoopUserMediumRepository
    {
        public WoopUserMediumRepository(WOOPDbContext ctx) : base(ctx) 
        { }

        public IEnumerable<int> GetBlockageLevel(int id)
        {
            return GetAll().Where(x => x.UserId == id).Select(x => x.BlockageLevel).ToList();   
        }
        public  IEnumerable<Medium> GetListOfShowsForUser(int? id)
        {
            
            var accounts = GetAll().Select(a => a.UserId).ToList();
            try
            {
                if (id != null)
                {
                    if (accounts.Contains((int)id))
                    {
                        var mediaList = new List<Medium>();
                        mediaList = GetAll().Include(a => a.Media).Where(a => a.UserId == id).Select(a => a.Media).ToList();
                        
                        if (mediaList != null)
                        {
                            return mediaList.OrderBy(u => u.Imdbid);
                        }
                        throw new Exception();
                    }
                    return Enumerable.Empty<Medium>();
                }
                throw new ArgumentNullException();

            }

            catch (Exception err)
            {
                return Enumerable.Empty<Medium>();
            }

        }
    }
}
