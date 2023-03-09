using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using test.api.helpers;
using test.api.models;
using System.IO;
using System.Net;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections.Generic;

namespace test.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        Class1 myClass = new Class1();

        [HttpGet]
        [ActionName("GetAllContacts")]
        public ActionResult<string> GetAllContacts()
        {
            
            string path = @"C:\Users\acer\Desktop\denemee\dnm.txt";
            
            string vlue=System.IO.File.ReadAllText(path, Encoding.UTF8);
            var list = JsonConvert.DeserializeObject<List<Contact>>(vlue);
           
            return Ok(list);
        }
        
        [HttpGet]
        [ActionName("GetContact")]
        public ActionResult<string> GetContact([FromQuery] int id)
        {
            string path = @"C:\Users\acer\Desktop\denemee\dnm.txt";

            string vlue = System.IO.File.ReadAllText(path, Encoding.UTF8);
            var list = JsonConvert.DeserializeObject<List<Contact>>(vlue);
            return Ok(list.Where(p=> p.Id==id).FirstOrDefault());
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult<string> Create(string name, DateTime dt)
        {
            string path = @"C:\Users\acer\Desktop\denemee\dnm.txt";
            string vlue = System.IO.File.ReadAllText(path, Encoding.UTF8);
            var list = JsonConvert.DeserializeObject<List<Contact>>(vlue);
            int nextId = list.Count > 0 ? list.Max(c => c.Id) + 1 : 1;
            Contact ct = new Contact();
            ct.Id = nextId;
            ct.Name = name;
            ct.BirthDate = dt;
            list.Add(ct);
            list = list.OrderBy(c => c.Id).ToList();
            vlue = JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText(path, vlue, Encoding.UTF8);
            return Ok(list);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public ActionResult<string> Delete(int id)
        {
            Contact ct = new Contact();
            ct.Id = id;
            string path = @"C:\Users\acer\Desktop\denemee\dnm.txt";
            string vlue = System.IO.File.ReadAllText(path, Encoding.UTF8);
            var list = JsonConvert.DeserializeObject<List<Contact>>(vlue);
            var aa = list.Where(x => x.Id == ct.Id).First();
            list.Remove(aa);
            vlue = JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText(path, vlue, Encoding.UTF8);
            return Ok(list);
        }

        [HttpPut]
        [ActionName("Update")]
        public ActionResult<string> Update(int id,string name, DateTime dt)
        {

            string path = @"C:\Users\acer\Desktop\denemee\dnm.txt";
            string vlue = System.IO.File.ReadAllText(path, Encoding.UTF8);
            var list = JsonConvert.DeserializeObject<List<Contact>>(vlue);

            var ss = list.FirstOrDefault(x => x.Id == id);
            if(ss!= null)
            {
            ss.Name = name;
            ss.BirthDate = dt;
            }

            vlue = JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText(path, vlue, Encoding.UTF8);
            return Ok(list);
        }

    }

}
