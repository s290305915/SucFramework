using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        /// <summary>
        /// 创建实例工厂
        /// </summary>
        static readonly IRoleRepository IRoleRepository = new RoleRepository();

        public string AllList()
        {
            string json = "[";
            List<string> kvs = new List<string>();
            string kv = "";
            GetAllRoless().ToList().ForEach((x) => {
                Type t = x.GetType();
                PropertyInfo[] pros = t.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                pros.ToList().ForEach(y => {
                    kvs.Add(string.Format("{\"{0}\",\"{1}\"}", y.Name, y.GetValue(y.Name)));
                });
            });
            kv = string.Join(",", kvs);
            json += kv + "]";
            return json;
        }

        //GET:  /api/products  
        public IEnumerable<Role> GetAllRoless()
        {
            return IRoleRepository.GetAll();
        }

        //GET: /api/products/id  
        public Role GetProduct(int id)
        {
            Role item = IRoleRepository.Get(id);
            if(item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        //GET: /api/products?name=name  
        public IEnumerable<Role> GetRolesByCategory(string name)
        {
            return IRoleRepository.GetAll().Where(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }


        //POST: /api/products  
        public HttpResponseMessage PostRole(Role item)
        {
            item = IRoleRepository.Add(item);

            var response = Request.CreateResponse<Role>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new
            {
                id = item.id
            });
            response.Headers.Location = new Uri(uri);

            return response;
        }

        //PUT: /api/products/id  
        public void PutRole(int id, Role role)
        {
            role.id = id;
            if(!IRoleRepository.Update(role))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        //Delete: /api/products/id  
        public void DeleteRole(int id)
        {
            Role item = IRoleRepository.Get(id);
            if(item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            IRoleRepository.Remove(id);
        }
    }
}
