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

        public DocumentController(SqlConnection connection)
        {
            con = connection;
        }
        public List<SelectListItem> GetDocuments()
        {
            list = new List<SelectListItem>();
            try
            {
                string sql_select = "SELECT * FROM documents";
                cmd = new SqlCommand(sql_select, con);
                con.Open();

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    documentModel = new DocumentModel();
                    documentModel.Id = int.Parse(read["id"].ToString());
                    documentModel.Document = read["document"].ToString();
                    item = new SelectListItem();
                    item.Text = documentModel.Document;
                    item.Value = documentModel.Document;
                    list.Add(item);
                }
                read.Close();
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>() { new SelectListItem(ex.Message, "error") };
            }
        }

        public List<SelectListItem> UpdateDocument(String document)
        {
            list = GetDocuments();
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
