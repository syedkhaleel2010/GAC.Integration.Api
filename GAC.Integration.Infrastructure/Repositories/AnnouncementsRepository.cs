using AutoMapper;
using MG.DynamicQuery;
using MG.Marine.Ticketing.Domain.Entities;
using MG.Marine.Ticketing.Domain.Helpers;
using MG.Marine.Ticketing.Domain.Repository;
using MG.Marine.Ticketing.SQL.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MG.Marine.Ticketing.SQL.Infrastructure.Repositories
{
    public class AnnouncementsRepository : HybridRepositoryBase<ANNOUNCEMENTS>, IAnnouncementsRepository
    {
        private readonly ServiceDbContext _dbContext;
        public AnnouncementsRepository(ServiceDbContext dbContext, IDbConnection dbConnection) 
            : base(dbContext, dbConnection)
        {
            _dbContext = dbContext;
        }
        public async Task<IPagedData<ANNOUNCEMENTS>> GetAllAnnouncements(DataDescriptor descriptor,string Status="0")
        {
            descriptor = CommonFormatters.DateFormatterInDataDescriptor(descriptor);
            if (descriptor.Filter.Filters.Count() > 0)
            {
                if (Status == "1")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now, x.ValidTo) > 0)).OrderByDescending(x => x.CreatedDate).ApplyDescriptorWithPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else if (Status == "2")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now,x.ValidTo) < 0)).OrderByDescending(x => x.CreatedDate).ApplyDescriptorWithPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else if (Status == "3")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now,x.ValidFrom) > 0)).OrderByDescending(x => x.CreatedDate).ApplyDescriptorWithPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else
                {
                    var pagedData = await _dbContext.ANNOUNCEMENTS.OrderByDescending(x => x.CreatedDate).ApplyDescriptorWithPaginationAsync(descriptor);
                    return await Task.FromResult(pagedData);
                }
            }
            else
            {
                if (Status == "1")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now, x.ValidTo) >= 0)).OrderByDescending(x => x.CreatedDate).ApplyPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else if (Status == "2")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now,x.ValidTo) < 0)).OrderByDescending(x => x.CreatedDate).ApplyPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else if (Status == "3")
                {
                    var data = await _dbContext.ANNOUNCEMENTS.Where(x => (EF.Functions.DateDiffMinute(DateTime.Now, x.ValidFrom) > 0)).OrderByDescending(x => x.CreatedDate).ApplyPaginationAsync(descriptor);
                    return await Task.FromResult(data);
                }
                else
                {
                    var pagedData = await _dbContext.ANNOUNCEMENTS.OrderByDescending(x => x.CreatedDate).ApplyPaginationAsync(descriptor);
                    return await Task.FromResult(pagedData);
                }
            }
        }
        public async Task<ANNOUNCEMENTS> GetAnnouncementById(string Id)
        {
            var Announcemnt =  _dbContext.ANNOUNCEMENTS.Where(x => x.Id == Id).FirstOrDefault();
            return await Task.FromResult(Announcemnt);
        }
    }
}