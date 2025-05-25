using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Interfaces
{
    public interface IHasher
    {
        public string Hash(string password);
        public bool Verify(string passwordToVerify, string hashedPassword);
    }
}
