using Microsoft.AspNetCore.Mvc.Rendering;
using model_asp.net_core.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace model_asp.net_core.Controllers.Admin.Document
{
    public class DocumentController
    {
        protected SqlConnection con = null;
        protected SqlCommand cmd = null;
        protected SqlDataReader read = null;
        protected DocumentModel documentModel = null;
        protected SelectListItem item = null;
        protected List<SelectListItem> list, select = null;

        public List<SelectListItem> GetDocuments(SqlConnection connection)
        {
            list = new List<SelectListItem>();
            try
            {
                string sql_select = "SELECT * FROM documents";
                con = connection;
                connection.Close();
                cmd = new SqlCommand(sql_select, con);
                con.Open();

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    documentModel = new DocumentModel();
                    documentModel.Id = int.Parse(read["id"].ToString());
                    documentModel.Name = read["document"].ToString();
                    item = new SelectListItem();
                    item.Text = documentModel.Name;
                    item.Value = documentModel.Name;
                    list.Add(item);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>() { new SelectListItem(ex.Message, "error") };
            }
            return list;
        }

        public List<SelectListItem> UpdateDocument(SqlConnection connection, string document)
        {
            con = connection;
            list = GetDocuments(con);
            select = new List<SelectListItem>();
            for (int i=0;i<list.Count; i++)
            {
                item = new SelectListItem();
                item.Text = list[i].Text;
                item.Value = list[i].Text;

                if (list[i].Text.Equals(document))
                {
                    item.Selected = true;
                }
                select.Add(item);
            }
            return select; 
        }
    }
}
