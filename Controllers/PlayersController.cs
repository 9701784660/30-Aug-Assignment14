using Assignment_14.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment_14.Controllers
{
    public class PlayersController : Controller
    {
        string conString = ConfigurationManager.ConnectionStrings["PlayerConStr"].ConnectionString;
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader srdr;
        private object player;

        // GET: Players
        public ActionResult Index()
        {
            List<Player> players = new List<Player>();

            try
            {

                con = new SqlConnection(conString);
                cmd = new SqlCommand("Select * from Players");
                cmd.Connection = con;
                con.Open();
                srdr = cmd.ExecuteReader();
                while (srdr.Read())
                {
                    players.Add(
                        new Player
                        {
                            PlayerId = (int)(srdr["PlayerId"]),
                            FirstName = (string)srdr["FirstName"],
                            LastName = (string)srdr["LastName"],
                            JerseyNumber = (int)srdr["JerseyNumber"],
                            Position = (string)(srdr["Position"]),
                            Team = (string)(srdr["Team"])
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally { con.Close(); }

            return View(players);
        }

        // GET: Players/Details/5
        public ActionResult Details(int id)
        {
            Player player = new Player();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players", con);
                con.Open();
                srdr = cmd.ExecuteReader();

                while (srdr.Read())
                {
                    player = (new Player
                    {
                        PlayerId = (int)srdr["PlayerId"],
                        FirstName = (string)srdr["FirstName"],
                        LastName = (string)srdr["LastName"],
                        JerseyNumber = (int)srdr["JerseyNumber"],
                        Position = (string)srdr["Position"],
                        Team = (string)srdr["Team"]
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);

        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("insert into Players values(@id,@fname,@lname,@jnum,@pos,@team)");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", player.PlayerId);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jnum", player.JerseyNumber);
                cmd.Parameters.AddWithValue("@pos", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);

                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally { con.Close(); }
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            Player player = new Player();
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("select * from Players where PlayerId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                srdr = cmd.ExecuteReader();

                while (srdr.Read())
                {
                    player = (new Player
                    {
                        PlayerId = (int)srdr["PlayerId"],
                        FirstName = (string)srdr["FirstName"],
                        LastName = (string)srdr["LastName"],
                        JerseyNumber = (int)srdr["JerseyNumber"],
                        Position = (string)srdr["Position"],
                        Team = (string)srdr["Team"]
                    });
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
            }
            return View(player);
        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("update Players set PlayerId=@id, FirstName=@fname,LastName=@lname,JerseyNumber=@jnum,Position=@pos,Team=@team where PlayerId=@id");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", player.PlayerId);
                cmd.Parameters.AddWithValue("@fname", player.FirstName);
                cmd.Parameters.AddWithValue("@lname", player.LastName);
                cmd.Parameters.AddWithValue("@jnum", player.JerseyNumber);
                cmd.Parameters.AddWithValue("@pos", player.Position);
                cmd.Parameters.AddWithValue("@team", player.Team);
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally { con.Close(); }
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }


        // POST: Players/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Player player)
        {
            try
            {
                con = new SqlConnection(conString);
                cmd = new SqlCommand("delete from Players where PlayerId=@id");
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@id", player.PlayerId);

                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Error");
            }
            finally { con.Close(); }
        }
    }
}
