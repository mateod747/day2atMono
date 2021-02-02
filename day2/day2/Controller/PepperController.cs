using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace day2.Controller
{
    public class Pepper
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Pepper(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class PepperController : ApiController
    {
        public static List<Pepper> peppers = new List<Pepper>()
        {
            new Pepper(5, "california")
        };

        [HttpPost]
        public string SavePepper(Pepper pepper)
        {
            Pepper pepper_new = new Pepper(pepper.Id, pepper.Name);
            peppers.Add(pepper_new);
            return pepper_new.Name;
        }
                
        [Route("show/{id}")]
        [HttpGet]
        public string ShowPepper(int id)
        {
            bool found = false;
            string name = "";
            foreach(Pepper pepper in peppers)
            {
                if(pepper.Id == id)
                {
                    found = true;
                    name = pepper.Name;
                }
            }
            if(!found)
            {
                return "This pepper doesn't exist!";
            } else
            {
                return "Pepper name: " + name;
            }
            
        }

        [Route("update/{id}/{name}")]
        [HttpPut]
        public HttpResponseMessage UpdatePepper(int id, string newName)
        {
            bool found = false;
            foreach (Pepper pepper in peppers)
            {
                if (pepper.Id == id)
                {
                    found = true;
                    pepper.Name = newName;
                }
            }

            if(found)
            {
                return Request.CreateResponse(HttpStatusCode.OK, 305);
            } else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, 405);
            }
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeletePepper(int id)
        {
            bool found = false;
            foreach (Pepper pepper in peppers)
            {
                if (pepper.Id == id)
                {
                    found = true;
                    peppers.RemoveAt(peppers.IndexOf(pepper));
                }
            }

            if (found)
            {
                return Request.CreateResponse(HttpStatusCode.OK, 305);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, 405);
            }
        }
    }
}
