using Microsoft.EntityFrameworkCore;
using SkillsMatrix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillsMatrix.Api.Models
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext appDbContext;

        public AddressRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Address> AddAddress(Address address)
        {
            var result = await appDbContext.Addresses.AddAsync(address);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Address> DeleteAddress(int addressId)
        {
            var result = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);
            if (result != null)
            {
                appDbContext.Addresses.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Address> GetAddress(int addressId)
        {
            return await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == addressId);
        }

        public async Task<IEnumerable<Address>> GetAddresses()
        {
            return await appDbContext.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> Search(string code, string addressline)
        {
            IQueryable<Address> query = appDbContext.Addresses;

            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(e => e.PostalCode.Contains(code) || e.PhysicalCode.Contains(code));
            }

            if (!string.IsNullOrEmpty(addressline))
            {
                query = query.Where(e => e.PhysicalAddressLine1.Contains(addressline) || e.PostalAddressLine1.Contains(addressline));
            }

            return await query.ToListAsync();
        }

        public async Task<Address> UpdateAddress(Address address)
        {
            var result = await appDbContext.Addresses.FirstOrDefaultAsync(e => e.Id == address.Id);
            if (result != null)
            {
                result.PhysicalAddressLine1 = address.PhysicalAddressLine1;
                //...

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
