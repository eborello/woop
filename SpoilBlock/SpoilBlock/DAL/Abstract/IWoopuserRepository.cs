using SpoilBlock.Models;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace SpoilBlock.DAL.Abstract
{
    public interface IWoopuserRepository: IRepository<Woopuser>
    {
        Woopuser? GetWoopUserByIdentityId(string identityID);
        bool Exists(Woopuser user);

        Task ListShowsAsync(Woopuser user, int mediaID, int blockageLevel);

        IEnumerable<Medium> GetListOfShows(IEnumerable<Medium> mediaList, Woopuser user);

    }
}
