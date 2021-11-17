using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL
{
    public class DealsDAL
    {
        readonly DLContext db = new DLContext();

        public int GetDealsCount()
        {
            try
            {
                return db.Deals.ToList().Count();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Decimal> GetDealNumbers()
        {
            try
            {

                return from r in db.Deals
                select r.DealNumber;
            }
            catch
            {
                throw;
            }
        }


        //To Add new deal
        public int AddDeal(Deal deal)
        {
            try
            {
                var obj = db.Deals.SingleOrDefault(d => d.DealNumber == deal.DealNumber);

                if (obj == null)
                {
                    db.Deals.Add(deal);
                    db.SaveChanges();
                }

                return 1;
            }
            catch
            {
                throw;
            }
        }
    }

}
