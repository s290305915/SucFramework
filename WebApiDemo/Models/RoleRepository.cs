using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiTest.Models
{
    public class RoleRepository : IRoleRepository
    {
        private List<Role> Roles = new List<Role>();
        private int _nextId = 1;
        public RoleRepository()
        {
            Add(new Role { id = 1, Name = "Tomato soup", Remark = "Groceries", Type = 1, Level = 1 });
            Add(new Role { id = 2, Name = "Yo-yo", Remark = "Toys", Type = 7, Level = 1 });
            Add(new Role { id = 3, Name = "Hammer", Remark = "Hardware", Type = 6, Level = 1 });
        }
        public Role Add(Role item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.id = _nextId++;
            Roles.Add(item);
            return item;
        }

        public Role Get(int id)
        {
            return Roles.Find(p => p.id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return Roles;
        }

        public void Remove(int id)
        {
            Roles.RemoveAll(p => p.id == id);
        }

        public bool Update(Role item)
        {
            if(item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = Roles.FindIndex(p => p.id == item.id);
            if(index == -1)
            {
                return false;
            }
            Roles.RemoveAt(index);
            Roles.Add(item);
            return true;
        }
    }
}