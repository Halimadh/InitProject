using DataLayer.Context;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceLayer
{
    public class ServiceUser : IDisposable
    {
        private readonly DataLayerContext _context;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;


        public ServiceUser() { }
        public ServiceUser(DataLayerContext context, IJwtAuthenticationService jwtAuthenticationService) 
        {  
            _context = context;
            _jwtAuthenticationService=jwtAuthenticationService;
        }


        public User Authenticate(string username,string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
                return null;
            return user;
        }
        public dynamic GetAll()
        {
            return _context.Users.ToList();
        }
        public dynamic GetUserById(string id)
        {
          var user= _context.Users.FindAsync(id);
            return user;
            
        }
        public void AddUser(User requeste) 
        {
            var _user = new User();

            string EncryptedPassword = _jwtAuthenticationService.Encrypt(requeste.Password);

            _user.Name = requeste.Name;
            _user.Username = requeste.Username;
            _user.Password = EncryptedPassword;
            _context.Users.Add(_user);
            _context.SaveChanges();
          
        }
       
        public dynamic delete(int id)
        {
            var user = _context.Users.Find(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                return _context.SaveChanges();

            }
            return null;
        }
        

      
        private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;


        void IDisposable.Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    component.Dispose();
                }


                // Note disposing has been done.
                disposed = true;
            }
        }


    }
}
