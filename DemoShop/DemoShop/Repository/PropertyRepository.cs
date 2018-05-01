using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoShop.Models;
using System.Data.SqlClient;
using System.Data;
using DemoShop.DataAccess;
using DemoShop.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DemoShop.Repository
{
    public interface IPropertyRepository
    {
        PropertyIndex GetPropertySearch(int pageSize, int pageIndex);
        PropertyIndex GetPropertySearch2(int pageSize, int pageIndex);

    }

    public class PropertyRepository : BaseCmdExec, IPropertyRepository
    {
        public PropertyRepository(string strConnName) : base("Data Source=.;Initial Catalog=dinhgianhadat.vn;Integrated Security=True;MultipleActiveResultSets=True;")
        {

        }
        public PropertyIndex GetPropertySearch2(int pageSize, int pageIndex)
        {
            PropertyOptions options = new PropertyOptions();

            object[] param = new object[] { options.DomainGroupId , pageSize , pageIndex,
                options.AdsTypeId, options.ProvinceId, options.DistrictId, options.WardId,options.StreetId , null, null, null, null, null, null, null, null};

            var rowMapper = base.GenerateRowMapper<PropertyIndex, List<PropertyItem>>(
                           "ListProperties", base.GetPropertyInfo<PropertyIndex>(x => x.ListProperties));

            return base.ExecStoredProc<PropertyIndex>("SP_F_Property_dgnd_Search_2", param, rowMapper).SingleOrDefault();
        }
        public PropertyIndex GetPropertySearch(int pageSize, int pageIndex)
        {
            DateTime start = DateTime.Now;
            using (SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=dinhgianhadat.vn;Integrated Security=True;MultipleActiveResultSets=True;"))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SP_F_Property_dgnd_Search", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //var priceFirst = GetListPropertyPriceStatic().FirstOrDefault(r => r.Id == options.PriceId && r.AdsType == options.AdsTypeId);
                //options.MinPriceProposed = priceFirst != null ? priceFirst.FromValue : options.MinPriceProposed;
                //options.MaxPriceProposed = priceFirst != null ? priceFirst.ToValue : options.MaxPriceProposed;

                //var areaFirst = GetListPropertyAreaStatic().FirstOrDefault(r => r.Id == options.AreaId);
                //options.MinAreaTotal = areaFirst != null ? areaFirst.FromValue : options.MinAreaTotal;
                //options.MaxAreaTotal = areaFirst != null ? areaFirst.ToValue : options.MaxAreaTotal;
                //options.WardId = options.WardId == 0 ? -1 : options.WardId;

                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@pageIndex", pageIndex);

                //cmd.Parameters.AddWithValue("@domainGroupId", options.DomainGroupId);
                //cmd.Parameters.AddWithValue("@adsTypeId", options.AdsTypeId);
                //cmd.Parameters.AddWithValue("@provinceId", options.ProvinceId);

                //cmd.Parameters.AddWithValue("@districtId", options.DistrictId);
                //cmd.Parameters.AddWithValue("@wardId", options.WardId);
                //cmd.Parameters.AddWithValue("@streetId", options.StreetId);

                //cmd.Parameters.AddWithValue("@apartmentId", options.ApartmentId);
                //cmd.Parameters.AddWithValue("@directionId", options.DirectionId);
                //cmd.Parameters.AddWithValue("@locationId", options.LocationId);

                //cmd.Parameters.AddWithValue("@MinAreaTotal", options.MinAreaTotal);
                //cmd.Parameters.AddWithValue("@MaxAreaTotal", options.MaxAreaTotal);

                //cmd.Parameters.AddWithValue("@MinPriceProposed", options.MinPriceProposed);
                //cmd.Parameters.AddWithValue("@MaxPriceProposed", options.MaxPriceProposed);
                //NOTE: @typeCssClassesLand has been hard code in DB

                //added 27/03/2016
                var reader = cmd.ExecuteReader();

                var model = new PropertyIndex();
                model.ListProperties = new List<PropertyItem>();

                reader.Read();
                int total = (int)reader["TotalCount"];
                model.TotalCount = total;

                // gets the second
                reader.NextResult();
                while (reader.Read())
                {
                    var item = new PropertyItem
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Content = reader["Content"].ToString(),
                        Title = reader["Title"].ToString(),
                        Note = reader["Note"].ToString(),
                        Floors = ConvertUtility.ToDouble(reader["Floors"].ToString()),
                        Bedrooms = ConvertUtility.ToInt32(reader["Bedrooms"].ToString()),
                        Bathrooms = ConvertUtility.ToInt32(reader["Bathrooms"].ToString()),
                        AddressNumber = reader["AddressNumber"].ToString(),
                        AddressCorner = reader["AddressCorner"].ToString(),
                        CreatedDate = ConvertUtility.ToDateTime(reader["CreatedDate"].ToString(), default(DateTime)),
                        LastUpdatedDate = ConvertUtility.ToDateTime(reader["LastUpdatedDate"].ToString(), default(DateTime)),
                        AdsExpirationDate = ConvertUtility.ToDateTime(reader["AdsExpirationDate"].ToString(), default(DateTime)),
                        AlleyWidth = !string.IsNullOrEmpty(reader["AlleyWidth"].ToString()) ? double.Parse(reader["AlleyWidth"].ToString()) : default(double),
                        PriceProposed = ConvertUtility.ToDouble(reader["PriceProposed"].ToString()),
                        PriceProposedInVND = ConvertUtility.ToDouble(reader["PriceProposedInVND"].ToString()),//--
                        PriceEstimatedInVND = ConvertUtility.ToDouble(reader["PriceEstimatedInVND"].ToString()),//--
                        AreaUsable = ConvertUtility.ToDouble(reader["AreaUsable"].ToString()),
                        AreaTotalWidth = ConvertUtility.ToDouble(reader["AreaTotalWidth"].ToString()),
                        AreaTotalLength = ConvertUtility.ToDouble(reader["AreaTotalLength"].ToString()),
                        AreaTotal = ConvertUtility.ToDouble(reader["AreaTotal"].ToString()),
                        ApartmentId = ConvertUtility.ToInt32(reader["ApartmentId"].ToString()),
                        AdsType = new PropertyBase
                        {
                            ShortName = reader["AdsTypeShortName"].ToString(),
                            CssClass = reader["AdsTypeCssClass"].ToString(),
                            Id = int.Parse(reader["AdsTypeId"].ToString())
                        },

                        Location = new PropertyBase
                        {
                            Id = ConvertUtility.ToInt32(reader["LocationId"].ToString()),
                            CssClass = reader["LocationCssClass"].ToString(),
                            Name = reader["LocationName"].ToString(),
                            ShortName = reader["LocationShortName"].ToString()
                        },
                        PaymentMethod = new PropertyBase
                        {
                            Id = ConvertUtility.ToInt32(reader["PaymentMethodId"].ToString()),
                            Name = reader["PaymentMethodName"].ToString(),
                            CssClass = reader["PaymentMethodCssClass"].ToString(),
                            ShortName = reader["PaymentMethodShortName"].ToString()
                        },


                        ImageDefaultAvatar = reader["ImageDefaultAvatar"].ToString()
                    };

                    model.ListProperties.Add(item);
                }
                reader.Close();

                return model;
            }
        }

    }
}