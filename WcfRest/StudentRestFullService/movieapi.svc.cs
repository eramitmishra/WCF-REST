using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.OleDb;

namespace MovieRestFullService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "movieapi" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select movieapi.svc or movieapi.svc.cs at the Solution Explorer and start debugging.
    public class movieapi : Imovieapi
    {
        public void DoWork()
        {
        }
        public List<Movie> getMovieList()
        {
            try
            {
                   //string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\github\WCF-REST\WcfRest\StudentRestFullService\App_Data\moviedatabase.accdb";


                SqlConnection conn = new  SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\github\WCF-REST\WcfRest\StudentRestFullService\App_Data\dbStudent.mdf;Integrated Security=True");
                //OleDbConnection conn = new OleDbConnection(connectionString);
                conn.Open();
                //OleDbCommand com = new OleDbCommand("Select * from Movie", conn);
                SqlCommand com = new SqlCommand("Select * from Movie", conn);
                //OleDbDataReader sdr = com.ExecuteReader();
                SqlDataReader sdr = com.ExecuteReader();
                List<Movie> lsmovie = new List<Movie>();
                while (sdr.HasRows)
                {
                    Movie obmiv = new Movie();

                    // get the results of each column
                    obmiv.ID =  int.Parse(sdr["ID"] as string);
                    obmiv.Title =  sdr["Title"] as string;
                    obmiv.Description =  sdr["Description"] as string;
                    obmiv.Image =  sdr["Image"] as string;
                    obmiv.GenreId =  Convert.ToInt32(sdr["GenreId"] as string);
                    obmiv.Director =  sdr["Director"] as string;
                    obmiv.Writer =  sdr["Writer"] as string;
                    obmiv.Producer =  sdr["Producer"] as string;
                    obmiv.ReleaseDate =  Convert.ToDateTime(sdr["ReleaseDate"] as string);
                    obmiv.Rating =  (byte)sdr["Rating"];
                    obmiv.TrailerURI = sdr["TrailerURI"] as string;
                    lsmovie.Add(obmiv);
                }


                conn.Close();




                //using (dbMoviesEntities objMoviesDB = new dbMoviesEntities())
                //{
                //    //objMoviesDB.Connection.Open();
                //    // the rest
                // return objMoviesDB.Movies.Select(a => a).ToList();
                //}

                return lsmovie;
                

            }
            catch (Exception ex)
            {
                //return ex.ToString();
                throw;
            }

        }


    }
}
