using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace day2.Controller
{
    public class PepperController : ApiController
    {
        static string con = ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString;
        static SqlConnection conn = new SqlConnection(con);
                
        [HttpPost]
        public HttpResponseMessage SavePepperOrShop(int id, string name, int pepperOrShop)
        {
            SqlCommand insert;

            if (pepperOrShop == 0)
            {
                insert = new SqlCommand("insert into Peppers values(@id, @name);", conn);
            }
            else
            {
                insert = new SqlCommand("insert into PepperShops values(@id, @name);", conn);
            }

            insert.Parameters.AddWithValue("@id", id);
            insert.Parameters.AddWithValue("@name", name);

            conn.Open();

            try
            {
                insert.ExecuteNonQuery();
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, 200);
            }
            catch (Exception)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, 400);
            }

            //string insert;
            //string what;

            //if (pepperOrShop == 0)
            //{
            //    what = "Peppers";
            //    insert = "select * from Peppers;";
            //}
            //else
            //{
            //    what = "PepperShops";
            //    insert = "select * from PepperShops;";
            //}

            //conn.Open();

            //try
            //{
            //    SqlDataAdapter dataAdapter= new SqlDataAdapter(insert, conn);
            //    DataSet dataSet = new DataSet();
            //    dataAdapter.Fill(dataSet);

            //    var newRow = dataSet.Tables[what].NewRow();
            //    if(what.Equals("Peppers"))
            //    {
            //        newRow["PeppersID"] = id;
            //        newRow["PeppersName"] = name;
            //    } else
            //    {
            //        newRow["ShopID"] = id;
            //        newRow["ShopName"] = name;
            //    }
            //    dataSet.Tables[what].Rows.Add(newRow);

            //    new SqlCommandBuilder(dataAdapter);
            //    dataAdapter.Update(dataSet);
            //    conn.Close();

            //    return Request.CreateResponse(HttpStatusCode.OK, 200);
            //}
            //catch (Exception)
            //{
            //    conn.Close();
            //    return Request.CreateResponse(HttpStatusCode.BadRequest, 400);
            //}
        }
                
        [Route("show/{id}")]
        [HttpGet]
        public HttpResponseMessage ShowPepperOrShop(int id, int pepperOrShop)
        {
            SqlCommand show;
            if (pepperOrShop == 0)
            {
                show = new SqlCommand("select * from Peppers where PepperID=@id;", conn);
            } else
            {
                show = new SqlCommand("select * from PepperShops where ShopID=@id;", conn);
            }
            
            show.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader reader = show.ExecuteReader();
                    
            try
            {
                string name = "";

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        name = reader.GetString(1);
                    }
                }

                reader.Close();
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, name);
            } catch (Exception)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, 400);
            }

        }

        [Route("update/{id}/{name}")]
        [HttpPut]
        public HttpResponseMessage UpdatePepperOrShop(int id, int pepperOrShop, string newName)
        {
            SqlCommand update;

            if (pepperOrShop == 0)
            {
                update = new SqlCommand("update Peppers set PepperName = @name where PepperID = @id;", conn);
            }
            else
            {
                update = new SqlCommand("update PepperShops set ShopName = @name where ShopID = @id;", conn);
            }

            update.Parameters.AddWithValue("@id", id);
            update.Parameters.AddWithValue("@name", newName);

            conn.Open();

            try
            {
                update.ExecuteNonQuery();
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, 200);
            }
            catch (Exception)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, 400);
            }
        }

        [Route("delete/{id}/{option}")]
        [HttpDelete]
        public HttpResponseMessage DeletePepperOrShop([FromUri]int id, [FromUri]int pepperOrShop)
        {
            SqlCommand delete;
            if(pepperOrShop == 0)
            {
                delete = new SqlCommand("delete from Peppers where PepperID=@id;", conn);
            } else
            {
                delete = new SqlCommand("delete from Peppers where ShopID=@id;", conn);
            }
            delete.Parameters.AddWithValue("@id", id);
            conn.Open();

            try
            {
                delete.ExecuteNonQuery();
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, 200);
            }
            catch (Exception)
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, 400);
            }                                           
        }
    }
}
