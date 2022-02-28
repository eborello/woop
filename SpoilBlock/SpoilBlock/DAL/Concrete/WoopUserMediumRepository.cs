﻿using SpoilBlock.Models;
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
        IEnumerable<Medium> IWoopUserMediumRepository.GetListOfShowsForUser(int id)
        {
            
            return GetAll().Where(u => u.UserId == id).Select(u => u.Media).ToList();
        }
    }
}