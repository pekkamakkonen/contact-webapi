using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsWebApi.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public List<Contact> Get()
        {
            return _context.Contacts.AsNoTracking().ToList();
        }

        public Contact Get(int id)
        {
            return _context.Contacts.AsNoTracking().FirstOrDefault(c => c.id == id);
        }

        public Contact Create(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return contact;
        }

        public Contact Update(Contact contact)
        {
            _context.Contacts.Update(contact);
            _context.SaveChanges();
            return contact;
        }

        public void Delete(int id)
        {
            var contact = Get(id);
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return;
        }
    }
}
