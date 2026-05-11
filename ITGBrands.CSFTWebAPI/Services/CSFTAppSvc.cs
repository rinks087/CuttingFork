using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Serialization;
using CSFTWebAPI.Models;
using static CSFTWebAPI.Controllers.CSFTAppSvcController;

namespace CSFTWebAPI.Services
{
    public class DataService
    {
        private readonly string _connectionString;
        private readonly ILogger<DataService> _logger;
        private readonly string _getDataFromTablesProc;
        private readonly string _savedumpmessage;
        private readonly string _getdumpcount;

        // Constructor to inject dependencies
        public DataService(ILogger<DataService> logger,IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _getDataFromTablesProc = configuration["StoredProcedures:GetDataFromTables"];
            _savedumpmessage = configuration["StoredProcedures:SaveDumpMessage"];
            _getdumpcount = configuration["StoredProcedures:GetDumpCount"];
            _logger = logger;
          
           
        }

        /// <summary>
        /// Fetches data from the database using the provided InputModel and stored procedure.
        /// </summary>
        /// <param name="inputModel">The model containing input data for the stored procedure.</param>
        /// <returns>Returns a DataSet containing the fetched data.</returns>
        //public async Task<DataSet> GetDataFromTablesAsync(InputModel inputModel)
        //{
        //    var dataSet = new DataSet();

        //    try
        //    {
        //        await using (var connection = new SqlConnection(_connectionString))
        //        {
        //            using (var command = new SqlCommand(_getDataFromTablesProc, connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;

        //                // Serialize the InputModel to XML
        //                var xmlSerializer = new XmlSerializer(typeof(InputModel));
        //                using (var stringWriter = new StringWriter())
        //                {
        //                    using (var writer = new XmlTextWriter(stringWriter))
        //                    {
        //                        xmlSerializer.Serialize(writer, inputModel);
        //                        var xmlInput = stringWriter.ToString();
        //                        command.Parameters.Add(new SqlParameter("@InputData", SqlDbType.Xml) { Value = xmlInput });
        //                    }
        //                }

        //                using (var adapter = new SqlDataAdapter(command))
        //                {
        //                    await connection.OpenAsync();
        //                    adapter.Fill(dataSet);
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        _logger.LogError(ex, "SQL error occurred while fetching data.");
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An unexpected error occurred while fetching data.");
        //        throw;
        //    }

        //    return dataSet;
        //}


        public async Task<(List<TagLayout> TagLayouts, List<TagDetail> TagDetails, List<BlendCode> BlendCodes, List<FeederMatrix> FeederMatrices, List<Location> Locations, List<FeederPurge> PurgeFeeder)> GetDataFromTablesAsync()
        {
            var tagLayouts = new List<TagLayout>();
            var tagDetails = new List<TagDetail>();
            var blendCodes = new List<BlendCode>();
            var feederMatrices = new List<FeederMatrix>();
            var locations = new List<Location>();
            var feederpurge = new List<FeederPurge>();

            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand(_getDataFromTablesProc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        //// Serialize the InputModel to XML
                        //var xmlInput = SerializeInputModelToXml(inputModel);
                        //command.Parameters.Add(new SqlParameter("@InputData", SqlDbType.Xml) { Value = xmlInput });

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            // Process TagLayouts
                            while (await reader.ReadAsync())
                            {
                                tagLayouts.Add(new TagLayout
                                {
                                    TagLayoutId = reader["Tag_Layout"].ToString(),
                                    TagType = reader["Tag_Type"].ToString(),
                                    Length = Convert.ToInt32(reader["Length"])
                                });
                            }

                            // Move to next result set
                            await reader.NextResultAsync();

                            // Process TagDetails
                            while (await reader.ReadAsync())
                            {
                                tagDetails.Add(new TagDetail
                                {
                                    TagLayout = reader["Tag_Layout"].ToString(),
                                    TagType = reader["Tag_Type"].ToString(),
                                    StartPosition = Convert.ToInt32(reader["Start_Position"]),
                                    FieldName = reader["Field_Name"].ToString(),
                                    FieldType = Convert.ToInt32(reader["Field_Type"]),
                                    FieldLength = Convert.ToInt32(reader["Field_Length"])
                                });
                            }

                            // Move to next result set
                            await reader.NextResultAsync();

                            // Process BlendCodes
                            while (await reader.ReadAsync())
                            {
                                blendCodes.Add(new BlendCode
                                {
                                    Code = Convert.ToInt32(reader["Code"]),
                                    CodeType = reader["Code_Type"].ToString(),
                                    BlendDescription = reader["BlendDescription"].ToString()
                                });
                            }

                            // Move to next result set
                            await reader.NextResultAsync();

                            // Process FeederMatrices
                            while (await reader.ReadAsync())
                            {
                                feederMatrices.Add(new FeederMatrix
                                {
                                    FeederCode = reader["Feeder_Code"].ToString(),
                                    SiemensLocn = reader["Siemens_Locn"].ToString(),
                                    SugLocDescription = reader["SugLocDescription"].ToString(),
                                    BlendCode = Convert.ToInt32(reader["Blend_Code"])
                                });
                            }

                            // Move to next result set
                            await reader.NextResultAsync();

                            // Process Locations
                            while (await reader.ReadAsync())
                            {
                                locations.Add(new Location
                                {
                                    SiemensLocn = reader["Siemens_Locn"].ToString(),
                                    ProductIdLocn = reader["Product_ID_Locn"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    LocationType = reader["Location_Type"].ToString()
                                });
                            }

                            // Move to next result set Feeder purge
                            await reader.NextResultAsync();

                            // Process FeederPurge
                            while (await reader.ReadAsync())
                            {
                                feederpurge.Add(new FeederPurge
                                {
                                    BlendCode = reader["BC"].ToString(),
                                    Description = reader["Descp"].ToString(),
                                    ItemCode = reader["IC"].ToString(),
                                    FeederlocationId = reader["Feeder_Location_ID"].ToString(),
                                    subTobacco_type = reader["sub_tobacco_type"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions
                // _logger.LogError(ex, "SQL error occurred while fetching data.");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // _logger.LogError(ex, "An unexpected error occurred while fetching data.");
                throw;
            }

            return (tagLayouts, tagDetails, blendCodes, feederMatrices, locations,feederpurge);
        }

        private string SerializeInputModelToXml(InputModel inputModel)
        {
            var xmlSerializer = new XmlSerializer(typeof(InputModel));
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    xmlSerializer.Serialize(writer, inputModel);
                    return stringWriter.ToString();
                }
            }
        }

        /// <summary>
        /// Saves a dump message to the database by calling a stored procedure.
        /// </summary>
        /// <param name="msg">The message object to be serialized and saved.</param>
        /// <param name="endpoint">The API endpoint related to the message.</param>
        /// <param name="returnCode">The return code from the API.</param>
        /// <param name="returnDescription">The description of the return code.</param>
        public async Task SaveDumpMessageAsync(Msg msg, string endpoint, string returnCode, string returnDescription)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(_savedumpmessage, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Serialize the Msg object to XML
                        var serializer = new XmlSerializer(typeof(Msg));
                        using (var stringWriter = new StringWriter())
                        {
                            serializer.Serialize(stringWriter, msg);
                            command.Parameters.Add(new SqlParameter("@XmlData", SqlDbType.Xml) { Value = stringWriter.ToString() });
                            command.Parameters.Add(new SqlParameter("@MIIAPI", SqlDbType.VarChar, 100) { Value = endpoint });
                            command.Parameters.Add(new SqlParameter("@ReturnCode", SqlDbType.NVarChar) { Value = returnCode });
                            command.Parameters.Add(new SqlParameter("@ReturnResponse", SqlDbType.NVarChar) { Value = returnDescription });
                        }

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while saving the dump message.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while saving the dump message.");
                throw;
            }
        }

        /// <summary>
        /// Retrieves the record count for a specific date using a stored procedure.
        /// </summary>
        /// <param name="recordDate">The date for which the record count is needed.</param>
        /// <returns>Returns the record count as an integer.</returns>
        public async Task<int> GetRecordCountByDateAsync(DateTime recordDate)
        {
            try
            {
                await using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(_getdumpcount, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@RecordDate", recordDate);

                        await connection.OpenAsync();

                        var result = await command.ExecuteScalarAsync();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "SQL error occurred while retrieving record count.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving record count.");
                throw;
            }
        }
    }
}
