using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Shopping.Portal.Data;
using Dotnet.Shopping.Portal.Models;
using Dotnet.Shopping.Portal.Models.User;

namespace Dotnet.Shopping.Portal.Services.User
{
    public interface IBillingAddressService
    {
        IQueryable<ApplicationUser> GetUsers();

        IQueryable<BillingAddress> GetBillingAddresses();
        /// <summary>
        /// Get billing address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Billing address entity</returns>
        BillingAddress GetBillingAddressByEmail(string email);
        BillingAddress GetBillingAddressById(Guid id);

        /// <summary>
        /// Insert billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        void InsertBillingAddress(BillingAddress billingAddress);

        /// <summary>
        /// Update billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        void UpdateBillingAddress(BillingAddress billingAddress);
    }
    public class BillingAddressService : IBillingAddressService
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructor

        public BillingAddressService(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods


        public BillingAddress GetBillingAddressByEmail(string email)
        {
            return _context.BillingAddresses.Where(b => b.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Get billing address by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Billing address entity</returns>
        public BillingAddress GetBillingAddressById(Guid id)
        {
            return _context.BillingAddresses.Find(id);
        }

        public IQueryable<BillingAddress> GetBillingAddresses()
        {
            return _context.BillingAddresses;
        }

        public IQueryable<ApplicationUser> GetUsers()
        {
            return _context.Users;
        }

        /// <summary>
        /// Insert billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        public void InsertBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _context.BillingAddresses.Add(billingAddress);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update billing address
        /// </summary>
        /// <param name="billingAddress">Billing address entity</param>
        public void UpdateBillingAddress(BillingAddress billingAddress)
        {
            if (billingAddress == null)
                throw new ArgumentNullException("billingAddress");

            _context.BillingAddresses.Update(billingAddress);
            _context.SaveChanges();
        }

        #endregion
    }
}
