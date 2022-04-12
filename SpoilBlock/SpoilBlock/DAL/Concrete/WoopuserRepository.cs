using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpoilBlock.Models;
using SpoilBlock.DAL.Abstract;
using SpoilBlock.Models;

namespace SpoilBlock.DAL.Concrete
{
    public class WoopuserRepository : Repository<Woopuser>, IWoopuserRepository
    {

        public WoopuserRepository(WOOPDbContext ctx) : base(ctx)
        { }
        public bool Exists(Woopuser user)
        {
            return _dbSet.Any(x => x.AspnetIdentityId == user.AspnetIdentityId && x.Username == user.Username);
        }

        public  Woopuser? GetWoopUserByIdentityId(string identityID)
        {
            return _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefault();
        }

        public async Task<Woopuser>? GetWoopUserByIdentityIdAsync(string identityID)
        {
            return await _dbSet.Where(u => u.AspnetIdentityId == identityID).FirstOrDefaultAsync();
        }


        public virtual IEnumerable<Medium> GetListOfShows(IEnumerable<Medium> mediaList, Woopuser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();

            }
            Woopuser? foundUser = _dbSet.Include(x => x.WoopuserMedia).Where(x => x.Id == user.Id).FirstOrDefault();

            List<Medium> output = new List<Medium>();

            if (foundUser == null || mediaList == null)
            {
                return output;
            }

            foreach (Medium media in mediaList)
            {
                var temp = foundUser.WoopuserMedia.Where(um => um.MediaId == media.Id).Select(um => um.Media).Single();
                output.Add(temp);
            }

            return output;

        }

        public virtual async Task ListShowsAsync(Woopuser user, int mediaID, int blockageLevel)
        {
            WoopuserMedium coreUser = new WoopuserMedium
            {

                User = user,
                MediaId = mediaID,
                BlockageLevel = blockageLevel
            };

            _context.Add(coreUser);
            await _context.SaveChangesAsync();
            return;
        }

        
    }
}
