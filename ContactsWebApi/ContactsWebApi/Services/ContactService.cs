using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsWebApi.Models;
using ContactsWebApi.Repositories;

namespace ContactsWebApi.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public List<Contact> GetContacts()
        {
            return _contactRepository.Get();
        }

        public Contact GetContactById(int id)
        {
            return _contactRepository.Get(id);
        }

        public Contact CreateContact(Contact contact)
        {
            return _contactRepository.Create(contact);
        }

        public Contact UpdateContact(int id, Contact contact)
        {
            var savedContact = _contactRepository.Get(id);
            if (savedContact == null)
            {
                throw new Exception("Contact not found");
            }
            else
            {
                return _contactRepository.Update(contact);
            }
        }

        public void DeleteContact(int id)
        {
            _contactRepository.Delete(id);
        }
    }
}
