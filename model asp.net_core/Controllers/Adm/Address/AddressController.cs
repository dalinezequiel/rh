using Microsoft.AspNetCore.Mvc.Rendering;
using model_asp.net_core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace model_asp.net_core.Controllers.Admin.Address
{
    public class AddressController
    {
        protected SqlConnection con = null;
        protected SqlCommand cmd = null;
        protected SqlDataReader read = null;
        protected ProvinceModel provinceModel = null;
        protected DistrictModel districtModel = null;
        protected SelectListItem item, province, district = null;
        protected List<SelectListItem> listProvince, listDistrict = null;

        /////////////////////////////////////////////PROVINCE///////////////////////////////////////////////
        public List<SelectListItem> GetProvinces(SqlConnection connection)
        {
            listProvince = new List<SelectListItem>();
            try
            {
                string sql_select = "SELECT * FROM provinces";
                con = connection;
                connection.Close();
                cmd = new SqlCommand(sql_select, con);
                con.Open();

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    provinceModel = new ProvinceModel();
                    provinceModel.Id = int.Parse(read["id"].ToString());
                    provinceModel.Name = read["province"].ToString();
                    item = new SelectListItem();
                    item.Text = provinceModel.Name;
                    item.Value = provinceModel.Id.ToString();
                    listProvince.Add(item);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>() { new SelectListItem(ex.Message, "error") };
            }
            return listProvince;
        }
        public List<SelectListItem> UpdateProvince(SqlConnection connection, string provinc)
        {
            listProvince = GetProvinces(connection);
            //provinceModel = new ProvinceModel();
            province = new SelectListItem();

            foreach (SelectListItem item in listProvince)
            {
                if (item.Text.Equals(provinc))
                {
                    province.Selected = true;
                }
                province.Text = item.Text;
                province.Value = item.Value;
                //provinceModel.Selected = province;
            }
            return new List<SelectListItem>() { province };
        }

        /////////////////////////////////////////////DISTRICT///////////////////////////////////////////////
        public List<SelectListItem> GetDistricts(SqlConnection connection)
        {
            listDistrict = new List<SelectListItem>();
            try
            {
                string sql_select = "SELECT * FROM districts";
                con = connection;
                connection.Close();
                cmd = new SqlCommand(sql_select, con);
                con.Open();

                read = cmd.ExecuteReader();
                while (read.Read())
                {
                    districtModel = new DistrictModel();
                    districtModel.Id = int.Parse(read["id"].ToString());
                    districtModel.Name = read["district"].ToString();
                    item = new SelectListItem();
                    item.Text = districtModel.Name;
                    item.Value = districtModel.Name;
                    listDistrict.Add(item);
                }
                read.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>() { new SelectListItem(ex.Message, "error") };
            }
            return listDistrict;
        }
        public List<SelectListItem> UpdateDistrict(SqlConnection connection, string distric)
        {
            listProvince = GetProvinces(connection);
            province = new SelectListItem();

            foreach (SelectListItem item in listDistrict)
            {
                if (item.Text.Equals(distric))
                {
                    district.Selected = true;
                }
                district.Text = item.Text;
                district.Value = item.Value;
            }
            return new List<SelectListItem>() { district };
        }
    }
}
