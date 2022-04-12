using SpoilBlock.Models;
using System.Collections.Generic;


namespace SpoilBlock.DAL.Abstract
{
    public interface IWoopUserMediumRepository: IRepository<WoopuserMedium>
    {

        IEnumerable<int> GetBlockageLevel(int id);
        IEnumerable<Medium> GetListOfShowsForUser(int? id);
    }
}
