﻿using System;
using System.Collections.Generic;
using System.Linq;
using DDDEastAnglia.Areas.Admin.Models;
using DDDEastAnglia.DataAccess;
using DDDEastAnglia.DataAccess.SimpleData.Models;

namespace DDDEastAnglia.Controllers
{
    public class SponsorModelQuery
    {
        private readonly ISponsorRepository _sponsorRepository;

        public SponsorModelQuery(ISponsorRepository sponsorRepository)
        {
            if (sponsorRepository == null)
            {
                throw new ArgumentNullException("sponsorRepository");
            }
            _sponsorRepository = sponsorRepository;
        }

        public IEnumerable<SponsorModel> Get()
        {
            var sponsors =
                _sponsorRepository
                    .GetAllSponsors()
                    .Where(x => x.ShowPublicly)
                    .OrderBySponsorSorter();

            return sponsors.Select(ToViewModel);
        }

        private SponsorModel ToViewModel(Sponsor sponsor)
        {
            return new SponsorModel
            {
                Name = sponsor.Name, 
                SponsorId = sponsor.SponsorId,
                Url = sponsor.Url
            };
        }
    }
    
}